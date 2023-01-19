using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    public List<Birds> bird;
    public List<Pig> pigs;

    [HideInInspector]
    public int Num; //当前的小鸟数目
    public static GameManage instance;
    public Vector3 p_org;    //初始的位置

    public List<AudioClip> win_Audio;
    public List <AudioClip> lose_Audio;

    private bool wins=false;
    private void Awake()//指定自己,供其它类引用
    {
        instance = this;
        if (bird.Count > 0)
            p_org = bird[Num].transform.position;
        
    }
    private void Start()
    {
        Itz();
        for (int i = 0; i < bird.Count; i++)
        {

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
        if (pigs.Count > 0 && bird.Count == 0)   //失败
        {
            print("你赢赢赢，最后输光光");
            AudioPlay(lose_Audio);
        }
        else if(pigs.Count<=0 && !wins)  //胜利
        {
            wins = true;
            print("你赢赢赢，最后赢麻了");
            AudioPlay(win_Audio);
        }
    }
    public void Itz()//小鸟初始化
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
    }
}
