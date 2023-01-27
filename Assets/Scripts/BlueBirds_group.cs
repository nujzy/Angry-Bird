using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBirds_group : MonoBehaviour
{
    private GameObject Fa;
    private BlueBirds Bb;
    private SpriteRenderer sp;
    private Rigidbody2D rb;
    public Sprite hurt;
    private GameObject Boom;
    public List<AudioClip> Cl;
    public List<AudioClip> dst;
    private float Stime;
    private float x;
    private float y;
    [HideInInspector]
    public float size;
    private GameObject PP;
    private GameObject PP2;
    private bool Live2=true;

    private void AudioPlay(List<AudioClip> clips)
    {
        int a = Random.Range(0, clips.Count);
        AudioSource.PlayClipAtPoint(clips[a], new Vector3(0, 0, 0));
    }
    private void Xian(Transform targets)//小鸟划线的脚本
    {
        if (x == 0 || (targets.position - new Vector3(x, y, 0)).magnitude > 0.4)
        {
            PP2 = Instantiate(PP, targets.position, Quaternion.identity) as GameObject;
            float v3 = (size % 3 + 2.45F) / 10;
            size++;
            PP2.transform.localScale = new Vector3(v3, v3, v3);
            x = PP2.transform.position.x;
            y = PP2.transform.position.y;
            Bb.PPs.Add(PP2);
        }
    }
    private void Hide() //小鸟单独的隐藏脚本
    {
        if (Stime != 0)
        {
            Invoke("Hide", Stime);
            Stime = 0;
        }
        else
        {
            Instantiate(Boom, transform.position, Quaternion.identity);
            AudioPlay(dst);
            Destroy(gameObject);
            GameManage.instance.Win();
        }
    }
    private void Awake()
    {
        Fa = transform.parent.gameObject;
        Bb = Fa.GetComponent<BlueBirds>();
        Boom = Bb.boom;
        PP = Bb.PP;
        sp=GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Live2 = false;
        AudioPlay(Cl);
        sp.sprite = hurt;
        Invoke("Hide", 2.5F);
        if ((collision.relativeVelocity.magnitude > 3 || rb.velocity.magnitude != 0))    //撞击后存在时间延长
        {
            Stime = 2.5F;
            if (collision.relativeVelocity.magnitude > 4)
            {
                Instantiate(Boom, transform.position, Quaternion.identity);
                AudioPlay(Cl);
            }
        }
    }
    private void Update()
    {
        if(Live2)
        {
            Xian(transform);
        }
    }
}
    
