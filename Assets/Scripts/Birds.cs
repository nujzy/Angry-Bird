using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Birds : MonoBehaviour
{
    private float maxdis = 1.3F;
    private bool put = false;
    public int Mynum;

    public Transform position_r;
    public Transform position_l;

    public LineRenderer po_r;
    public LineRenderer po_l;

    public GameObject Pad;
    public GameObject boom; //添加动画，小鸟的爆炸效果
    public GameObject PP;   //用于添加贴图，小鸟的屁屁(抛物线轨迹)
    [HideInInspector]
    public GameObject PP2;  //pp的克隆
    protected float x;      //x的坐标
    protected float y;
    protected float size;
    public List<GameObject> PPs;  //用于存储，统一删除PP2

    public bool active = false;      //小鸟是否是活动的
    public bool Live = true;         //判定小鸟是否一重活着(飞出去就算死了)
    protected bool Live2 = true;       //判定小鸟是否二重或者(碰到东西算死了)
    protected float Stime;             //小鸟消失时间(碰到东西后时间重置

    [HideInInspector]
    public SpringJoint2D sp;
    [HideInInspector]
    public Rigidbody2D rg;   
    protected CircleCollider2D circle;

    //动画相关变量

    //小鸟贴图相关变量
    protected SpriteRenderer sr;
    public Sprite awak; //开始时
    public Sprite pull; //拉取时
    public Sprite fly;  //飞行时
    public Sprite hurt; //受伤时
    public Sprite link; //眨眼动画
    public Sprite open; //张嘴动画

    //声音相关变量
    public List<AudioClip> coll;
    public List<AudioClip> yell;
    public List<AudioClip> shot;
    public AudioClip selects;
    public AudioClip fly_Audio;
    public AudioClip pull_Audio;
    public AudioClip dst_Audio;

    private void Awake()
    {
        PPs = new List<GameObject> ();
        sp = GetComponent<SpringJoint2D>();
        rg = GetComponent<Rigidbody2D>();   
        sr = GetComponent<SpriteRenderer>();
        circle = GetComponent<CircleCollider2D>();
        sr.sprite = awak;   
    }
    public void Change()
    {
        if(Live)
            sr.sprite = awak;
        float a = Random.Range(0.5F, 4);
        Invoke("Anime_Change",a);
    }
    private void Anime_Change()
    {
        if(Mynum>=GameManage.instance.Num-1 && Live)
        {
            if(Random.Range(0,2) == 0)
            {
                sr.sprite = open;
            }
            else
            {
                sr.sprite = link;
            }
            Invoke("Change", 0.2F);
        }
    }
    public void Jump()
    {
        float a = Random.Range(1.5F, 4F);
        if (rg.mass<2.1)
            Invoke("Anime_Jump", a);
    }
    private void Anime_Jump()
    {
        if(Mynum >= GameManage.instance.Num && GameManage.instance.bird[Mynum-1].Live)
        {
            if (rg.velocity.y == 0)
            {
                if (Random.Range(0, 2) == 0)
                {
                    rg.velocity = new Vector2(rg.velocity.x, rg.velocity.y + 3.5F-rg.mass);
                }
                else
                {
                    rg.velocity = new Vector2(rg.velocity.x, rg.velocity.y + 3.75F);
                    if (rg.mass < 1.7)
                    {
                        rg.rotation = 0;
                        int a = Random.Range(-1, 2);
                        rg.angularVelocity += 1305 * a;   //Angular Drag=3.3   Angular Rotate:Rotate = 10:3.026(精确两位)
                    }
                }
            }
            Invoke("Jump", 1);
        }
    }
    protected void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip,new Vector3 (0,0,0));
    }
    protected void AudioPlay(List<AudioClip> clips)
    {
        int a = Random.Range(0, clips.Count);
        AudioSource.PlayClipAtPoint(clips[a], new Vector3 (0,0,0));
    }
    private void Line(GameObject obj) //  弹弓划线
    {
        Vector3 Dis = (obj.transform.position - position_r.position).normalized;
        Dis *=0.2F;
        Vector3 Q= obj.transform.position+Dis;
        float angel = Mathf.Atan2(Dis.y, Dis.x)*Mathf.Rad2Deg;  //mathf.rad2deg 弧度转角度
        Pad.transform.rotation = Quaternion.AngleAxis(angel, position_r.position);  //围绕右弹弓旋转 angel度
        Pad.transform.position = Q;
        po_l.enabled = true;
        po_r.enabled = true;
        po_r.SetPosition(0, position_r.position);
        po_r.SetPosition(1, Pad.transform.position);
        po_l.SetPosition(0, position_l.position);
        po_l.SetPosition(1, Pad.transform.position);
    }
    private void Fly()  //小鸟的飞行
    {
        AudioPlay(fly_Audio);
        AudioPlay(shot);
        GameManage.instance.Bezer();
        rg.freezeRotation = false;
        Live = false;
        sp.enabled = false;
        sr.sprite = fly;
    }
    protected void Xian(Transform targets)//小鸟划线的脚本
    {
        if (x ==0 || (targets.position-new Vector3(x,y,0)).magnitude>0.4)
        {
            PP2 = Instantiate(PP, targets.position, Quaternion.identity) as GameObject;
            float v3 = (size % 3 + 2.45F) / 10 ;
            size ++;
            PP2.transform.localScale = new Vector3(v3, v3, v3);
            x = PP2.transform.position.x;
            y = PP2.transform.position.y;
            PPs.Add(PP2);
        }    
    }
    protected void ShowPP()
    {
        PP2 = Instantiate(PP);
        PP2.layer = 10;
        PP2.transform.localScale = new Vector3(0.8F, 0.8F, 0.8F);
        PP2.transform.position = transform.position;
        PPs.Add(PP2);
        x = PP2.transform.position.x;
        y = PP2.transform.position.y;
    }
    public void Dst()   //屁屁的删除脚本
    {
        for(int i = 0; i < PPs.Count;i++)
        {
            Destroy(PPs[i]);
        }
    }
    public void Hide() //小鸟单独的隐藏脚本
    {
        if (Stime != 0)
        {
            Invoke("Hide", Stime);
            Stime = 0;
        }
        else
        {
            gameObject.SetActive(false);
            Instantiate(boom, transform.position, Quaternion.identity);
            AudioPlay(dst_Audio);
            GameManage.instance.Win();
        }
    }
    protected void OnCollisionEnter2D(Collision2D collision)   //小鸟碰到东西后，开始消失判定
    {
        if (!Live && Live2)  //防止使用前小鸟的碰撞
        {
            Live2 = false;
            AudioPlay(coll);
            sr.sprite = hurt;
            Invoke("Hide", 2.5F);
        }
        else if (!Live2 && (collision.relativeVelocity.magnitude > 3 || rg.velocity.magnitude != 0))    //撞击后存在时间延长
        {
            Stime = 2.5F;
            if (collision.relativeVelocity.magnitude > 4)
            {
                Instantiate(boom, transform.position, Quaternion.identity);
                AudioPlay(coll);
            }
        }
    }
    private void OnMouseDown()
    {
        put = true;
        if (active)
        {
            AudioPlay(selects);
            AudioPlay(pull_Audio);
            rg.isKinematic = true;
        }
    }
    private void OnMouseUp()
    {
        put = false;
        circle.enabled = true;
        rg.isKinematic = false;
        if (Mynum > GameManage.instance.Num-1 && rg.velocity.y == 0)
        {
            rg.velocity += new Vector2(0, 4 / rg.mass);
            AudioPlay(selects);
        }
        if (active)
        {
            if (Vector3.Distance(transform.position, position_r.position) > 0.3)
            {
                Invoke("Fly", 0.1F);
                rg.constraints = ~RigidbodyConstraints2D.FreezePosition;
            }
            else
            {
                transform.position = GameManage.instance.p_org;
                Line(gameObject);
            }
        }
    }
    public virtual void Show()
    {
        active = false;
        AudioPlay(yell);
    }
    protected void Update()
    {
        if (put && active && Live)//鼠标按下
        {
            sr.sprite = pull;
            circle.enabled = false;
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);   //鸟相机一平面，相机无法照射
            transform.position += new Vector3(0, 0, -Camera.main.transform.position.z); //减去z轴偏移
            if (Vector3.Distance(transform.position, position_r.position) > maxdis)
            {
                Vector3 pos = (transform.position - position_r.position).normalized;    //获得向量方向(单位化)
                pos *= maxdis;                                                          //确定向量长度
                transform.position = pos + position_r.position;
            }
        }
        if (Live)
        {
            Line(gameObject);
        }
        else
        {
            Pad.transform.position = GameManage.instance.p_org;
            Line(Pad);
        }
        if (!Live && Live2)
        {
            Xian(transform);
            if (active && Input.GetMouseButtonDown(0))
            {
                Show();
            }
        }
    }
}