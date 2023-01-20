using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    public float maxs = 10;
    public float mins = 5;
    public bool Ispig = false; //�ж���������������

    public Sprite hurt;
    private SpriteRenderer sr;

    public GameObject boom;
    public GameObject Score;
    private void Die()
    {
        if(Ispig)
        {
            GameManage.instance.pigs.Remove(this);
        }
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);
        GameObject dd=Instantiate(Score, transform.position, Quaternion.identity);
        Destroy(dd, 1.5f);
    }
    private void OnCollisionEnter2D(Collision2D collision)//��ײ��� ����rg ���ᷴ��
    {
        float Lis = collision.relativeVelocity.magnitude * GameManage.instance.bird[GameManage.instance.Num - 1].Li;
        if (Ispig && GameManage.instance.bird[GameManage.instance.Num - 1].Li == 1F)
        {
            Lis *= 1.45F;
        }
        if (Lis>maxs)
        {
            Die();
        }
        else if(Lis < maxs && Lis>mins )
        {
            sr.sprite = hurt;
            maxs = maxs / 2;
            mins = mins / 2;
        }
    }
    private void Awake()
    {
        sr= GetComponent<SpriteRenderer>();
    }
}
