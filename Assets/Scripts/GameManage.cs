using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    public List<Birds> bird;
    public List<Collison_Moveble> pigs;
    public List<GameObject> Movebles = new List<GameObject>();

    [HideInInspector]
    public int Num; //��ǰ��С����Ŀ
    public static GameManage instance;
    public Vector3 p_org;    //��ʼ��λ��

    public List<AudioClip> win_Audio;
    public List <AudioClip> lose_Audio;
    public List<AudioClip> next_Audio;
    public List<AudioClip> start_Audio;

    private bool wins=false;
    private void Awake()//ָ���Լ�,������������
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
    public void Win()   //ֻ��Ӧʤ����ʧ��
    {
        if (pigs.Count > 0 && bird.Count == Num-1)   //ʧ��
        {
            AudioPlay(lose_Audio);
        }
        else if(pigs.Count<=0 && !wins)  //ʤ��
        {
            wins = true;
            AudioPlay(win_Audio);
        }
    }
    public void Itz()//С���ʼ��
    {
        for(int i=Num; i<bird.Count; i++)
        {
            if(i == Num)
            {
                bird[i].rg.constraints = RigidbodyConstraints2D.FreezeAll;
                bird[i].active = true;
                bird[i].enabled = true;
                bird[i].sp.enabled = true;
                bird[i].transform.position = p_org;
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
        Num ++;
        if (bird.Count > Num - 1)
        {
            AudioPlay(next_Audio);
        }
    }
}
