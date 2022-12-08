using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    public float maxs = 10;
    public float mins = 5;
    public bool Ispig = false; //判定对象是猪还是物体

    //变化图片的变量
    public Sprite hurt;
    public Sprite died;
    private SpriteRenderer sr;

    //外部引用的对象
    public GameObject boom;
    public GameObject Score;
    private void OnTriggerEnter2D(Collider2D collision)//出发检测 一个rg 只有自己反馈
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
    private void OnCollisionEnter2D(Collision2D collision)//碰撞检测 两个rg 都会反馈
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
