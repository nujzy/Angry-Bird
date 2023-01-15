using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    public List<Birds> bird;
    public List<Pig> pigs;

    [HideInInspector]
    public int Num; //��ǰ��С����Ŀ
    public static GameManage instance;
    public Vector3 p_org;    //��ʼ��λ��

    private void Awake()//ָ���Լ�,������������
    {
        instance = this;
        if (bird.Count > 0)
            p_org = bird[Num].transform.position;
    }
    private void Start()
    {
        Itz();
        for(int i = 0; i < bird.Count; i++)
        {
            bird[i].Mynum = i;
            bird[i].Change();
            bird[i].Jump();
        }
    }
    public void Win()   //ֻ��Ӧʤ����ʧ��
    {
        if (pigs.Count > 0 && bird.Count == 0)   //ʧ��
        {
            print("��ӮӮӮ���������");
        }
        else if(pigs.Count<=0)  //ʤ��
        {
            print("��ӮӮӮ�����Ӯ����");
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
    }
}
