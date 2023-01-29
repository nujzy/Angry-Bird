using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collison_Moveble : MonoBehaviour
{
    public float maxs = 10;
    public float mins = 5;
    private float life;
    public bool Ispig = false; //判定对象是猪还是物体
    public bool Isnormal = false;

    private SpriteRenderer sr;
    public List<Sprite> Hurt = new List<Sprite>();
    public List<Sprite> Link = new List<Sprite>();
    public List<Sprite> Laugh = new List<Sprite>();
    private int Mycount=0;

    public GameObject boom;
    public GameObject Score;

    private void GetSprite()
    {
        float Aray = maxs - mins;
        int Counts = Hurt.Count;
        Mycount = Counts - Mathf.RoundToInt((life - mins) / (Aray / Counts));
        if(Mycount == Counts)
        {
            Mycount -= 1;
        }
        sr.sprite = Hurt[Mycount];  
    }
    public void Ht(float Lis)
    {
        if (Lis > mins)
        {
            life -= Lis;
            if (mins > life)
            {
                Die();
                Destroy(gameObject);
            }
            else if (life >= mins)
            {
                GetSprite();
            }
        }

    }
    private float Gettag(Collision2D collision)
    {
        var Mytag = transform.tag;
        var Thattag = collision.transform.tag;
        float Li = 1F;
        switch (Mytag)
        {
            case "pig":
                Li *= 1.7F;
                break;
            case "glass":
                switch (Thattag)
                {
                    case "red":
                        Li *= 1.3F;
                        break;
                    case "blue":
                        Li *= 1.9F;
                        break;
                    case "green":
                        Li *= 1.6F;
                        break;
                    case "big":
                        Li *= 1.65F;
                        break;
                }
                break;
            case "wood":
                switch (Thattag)
                {
                    case "red":
                        Li *= 1.25F;
                        break;
                    case "blue":
                        Li *= 0.75F;
                        break;
                    case "yellow":
                        Li *= 1.8F;
                        break;
                    case "green":
                        Li *= 1.6F;
                        break;
                    case "big":
                        Li *= 1.65F;
                        break;
                }
                break;
            case "iron":
                switch (Thattag)
                {
                    case "red":
                        Li *= 1.45F;
                        break;
                    case "blue":
                        Li *= 1.0F;
                        break;
                    case "black":
                        Li *= 1.8F;
                        break;
                    case "white":
                        Li *= 1.8F;
                        break;
                    case "big":
                        Li *= 1.7F;
                        break;
                }
                break;
        }
        return Li;
    }
    public void Die()
    {
        if(Ispig)
        {
            GameManage.instance.pigs.Remove(this);
            GameObject dd = Instantiate(Score, transform.position, Quaternion.identity);
            Destroy(dd, 1.5f);
        }
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);         
    }
    protected void OnCollisionEnter2D(Collision2D collision)//碰撞检测 两个rg 都会反馈
    {
        Ht(collision.relativeVelocity.magnitude * Gettag(collision));
    }
    private void Start()
    {
        life = maxs;
        sr = GetComponent<SpriteRenderer>();
        GameManage.instance.Movebles.Add(gameObject);
        GetSprite();
    }
}
