using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    public List<Birds> bird;
    public List<Pig> pigs;

    public static GameManage instance;
    private Vector3 p_org;    //初始的位置
    private void Awake()//指定自己,供其它类引用
    {
        instance = this;
        if (bird.Count > 0)
            p_org = bird[0].transform.position;
    }
    private void Start()
    {
        Itz();
    }
    public void Nextbird()  //视频里使用Nextbird方法并不符合游戏逻辑，故拆解成public的Itz和Win 
    {
       /* if (pigs.Count>0)
        {
            if(bird.Count>0)//继续
            {
                Itz();
            }
            else//失败
            {

            }
        }
        else
        {

        }*/
    }
    public void Win()   //只响应胜利和失败
    {
        if (pigs.Count > 0 && bird.Count == 0)   //失败
        {
            print("你赢赢赢，最后输光光");
        }
        else if(pigs.Count<=0)  //胜利
        {
            print("你赢赢赢，最后赢麻了");
        }
    }
    public void Itz()//小鸟初始化
    {   
        for(int i=0; i<bird.Count; i++)
        {
            if(i == 0)
            {
                bird[i].active = true;
                bird[i].transform.position = p_org;
                bird[i].enabled = true;
                bird[i].sp.enabled = true;
            }
            else
            {
                bird[i].enabled = false;
                bird[i].sp.enabled = false;
            }
        }
    }
}
