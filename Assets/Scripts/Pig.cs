using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    public float maxs = 10;
    public float mins = 5;
    public bool Ispig = false; //�ж���������������

    //�仯ͼƬ�ı���
    public Sprite hurt;
    public Sprite died;
    private SpriteRenderer sr;

    //�ⲿ���õĶ���
    public GameObject boom;
    public GameObject Score;
    private void OnTriggerEnter2D(Collider2D collision)//������� һ��rg ֻ���Լ�����
    {
    }
    private void Die()
    {
        if(Ispig)
        {
            GameManage.instance.pigs.Remove(this);
        }
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);
        GameObject dd=Instantiate(Score, transform.position+new Vector3(0,0.5f,0), Quaternion.identity);
        Destroy(dd, 1.5f);
    }
    private void OnCollisionEnter2D(Collision2D collision)//��ײ��� ����rg ���ᷴ��
    {
        if (collision.relativeVelocity.magnitude>maxs)
        {
            Die();
        }
        else if(collision.relativeVelocity.magnitude>mins && collision.relativeVelocity.magnitude < maxs)
        {
            sr.sprite = hurt;
        }
    }
    private void Awake()
    {
        sr= GetComponent<SpriteRenderer>();
    }
}
