using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    public float maxs = 10;
    public float mins = 5;
    public bool Ispig = false; //判定对象是猪还是物体

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
    private void OnCollisionEnter2D(Collision2D collision)//碰撞检测 两个rg 都会反馈
    {
        
        float Li = collision.relativeVelocity.magnitude;
        if (Li>maxs)
        {
            Die();
        }
        else if(Li < maxs && Li>mins )
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
