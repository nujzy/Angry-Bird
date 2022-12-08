using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    public List<Birds> bird;
    public List<Pig> pigs;

    public static GameManage instance;
    private Vector3 p_org;    //��ʼ��λ��
    private void Awake()//ָ���Լ�,������������
    {
        instance = this;
        if (bird.Count > 0)
            p_org = bird[0].transform.position;
    }
    private void Start()
    {
        Itz();
    }
    public void Nextbird()  //��Ƶ��ʹ��Nextbird��������������Ϸ�߼����ʲ���public��Itz��Win 
    {
       /* if (pigs.Count>0)
        {
            if(bird.Count>0)//����
            {
                Itz();
            }
            else//ʧ��
            {

            }
        }
        else
        {

        }*/
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
