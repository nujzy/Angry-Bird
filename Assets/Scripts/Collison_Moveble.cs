using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collison_Moveble : MonoBehaviour
{
    public float maxs = 10;
    public float mins = 5;
    public bool Ispig = false; //�ж���������������

    public Sprite hurt;
    private SpriteRenderer sr;

    public GameObject boom;
    public GameObject Score;
    public void Die()
    {
        if(Ispig)
        {
            GameManage.instance.pigs.Remove(this);
        }
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);
        if (Ispig)
        {
            GameObject dd = Instantiate(Score, transform.position, Quaternion.identity);
            Destroy(dd, 1.5f);
        }
    }
    public void Ht(float Lis)
    {
        
        if (Ispig && GameManage.instance.bird[GameManage.instance.Num - 1].Li == 1F)
        {
            Lis *= 1.45F;
        }
        if (Lis > maxs)
        {
            Die();
        }
        else if (Lis < maxs && Lis > mins)
        {
            sr.sprite = hurt;
            maxs /= 2;
            mins /= 2;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)//��ײ��� ����rg ���ᷴ��
    {
        float Li = collision.relativeVelocity.magnitude * GameManage.instance.bird[GameManage.instance.Num - 1].Li;
        Ht(Li);
    }
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        GameManage.instance.Movebles.Add(gameObject);
    }
}
