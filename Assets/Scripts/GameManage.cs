using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    public List<Birds> bird;
    public List<Collison_Moveble> pigs;
    public List<GameObject> Movebles = new List<GameObject>();

    [HideInInspector]
    public int Num; //当前的小鸟数目
    public static GameManage instance;
    public Vector3 p_org;    //初始的位置

    public List<AudioClip> win_Audio;
    public List<AudioClip> lose_Audio;
    public List<AudioClip> next_Audio;
    public List<AudioClip> start_Audio;
    public List<AudioClip> open_Audio;

    private bool wins = false;
    private void Awake()//指定自己,供其它类引用
    {
        instance = this;
        if (bird.Count > 0)
            p_org = bird[Num].transform.position;

    }
    private void Start()
    {
        AudioPlay(start_Audio);
        Itz();
        for (int i = 0; i < bird.Count; i++)
        {
            Movebles.Add(bird[i].gameObject);
            bird[i].Mynum = i;
            bird[i].Change();
            bird[i].Jump();
        }
    }
    protected void AudioPlay(List<AudioClip> clips)
    {
        int a = Random.Range(0, clips.Count);
        AudioSource.PlayClipAtPoint(clips[a], new Vector3(0, 0, 0));
    }
    public void Win()   //只响应胜利和失败
    {
        if (pigs.Count > 0 && bird.Count == Num - 1)   //失败
        {
            AudioPlay(lose_Audio);
        }
        else if (pigs.Count <= 0 && !wins)  //胜利
        {
            wins = true;
            AudioPlay(win_Audio);
        }
    }
    public void Bezer()
    {
        if(Num<bird.Count)
            StartCoroutine(Move());
    }
    private IEnumerator Move()
    {
        Vector3 start = bird[Num].transform.position;
        Vector3 target = p_org;
        Vector3 mid = new Vector3((start.x + target.x) / 2, target.y + 1.5F, 0);
        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            Vector3 s1 = Vector3.Lerp(start, mid, i);
            Vector3 s2 = Vector3.Lerp(mid, target, i);
            Vector3 s3 = Vector3.Lerp(s1, s2, i);
            float q1 = Mathf.Lerp(0, 360, i);
            yield return StartCoroutine(MovetoPoint(s3,i,q1));
        }
    }
    private IEnumerator MovetoPoint(Vector3 s3,float i,float q1)
    {
        bird[Num].rg.bodyType = RigidbodyType2D.Static;
        while (Vector3.Distance(bird[Num].transform.position, s3) > 0.01F)
        {
            bird[Num].transform.position = Vector3.MoveTowards(bird[Num].transform.position,s3,Time.deltaTime*15);
            bird[Num].transform.localRotation = Quaternion.Euler(0, 0,-q1);
            yield return null;
        }
        if (i + Time.deltaTime >= 1-Time.deltaTime)
        {
            Itz();
            StopAllCoroutines();
            yield return null;
        }
    }
    public void Itz()//小鸟初始化
    {
        for (int i = Num; i < bird.Count; i++)
        {
            if (i == Num)
            {
                bird[i].rg.bodyType = RigidbodyType2D.Dynamic;
                bird[i].rg.constraints = RigidbodyConstraints2D.FreezeAll;
                bird[i].active = true;
                bird[i].enabled = true;
                bird[i].sp.enabled = true;
                bird[i].transform.position = p_org;  
                bird[i].transform.rotation = Quaternion.identity;
                if (Num >= 2)
                {
                    bird[i - 2].Dst();
                }
            }
            else
            {
                bird[i].enabled = false;
                bird[i].sp.enabled = false;
            }
        }
        Num++;
        if (bird.Count > Num - 1)
        {
            AudioPlay(next_Audio);
        }
    }
}