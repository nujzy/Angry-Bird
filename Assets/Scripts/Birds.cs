using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Birds : MonoBehaviour
{
    public float maxdis = 3;
    private bool put = false;

    public Transform position_r;
    public Transform position_l;

    public LineRenderer po_r;
    public LineRenderer po_l;

    public GameObject Pad;
    public GameObject boom; //添加动画，小鸟的爆炸效果
    public GameObject PP;   //用于添加贴图，小鸟的屁屁(抛物线轨迹)
    [HideInInspector]
    public GameObject PP2;  //pp的克隆
    protected float x;    //x的坐标
    protected float y;
    private float size;
    protected List<GameObject> PPs;   //用于存储，统一删除PP2

    public bool active = false;      //小鸟是否是活动的
    public bool Live = true;         //判定小鸟是否一重活着(飞出去就算死了)
    private bool Live2 = true;      //判定小鸟是否二重或者(碰到东西算死了)
    private float Stime;            //小鸟消失时间(碰到东西后时间加快

    [HideInInspector]   //隐藏公有变量在inspector面板里的可视化
    public SpringJoint2D sp;
    [HideInInspector]
    public Rigidbody2D rg;

    //小鸟贴图相关变量
    protected SpriteRenderer sr;
    public Sprite awak;//开始时
    public Sprite pull;//拉取时
    public Sprite fly;//飞行时
    public Sprite hurt;

    private void Awake()//游戏初始化(优先Start())
    {
        PPs = new List<GameObject> ();
        sp = GetComponent<SpringJoint2D>(); //GetComponent 返回插件类型
        rg = GetComponent<Rigidbody2D>();   
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = awak;       
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
        if(Live)
        {
            Next();
            Live = false;
        }
        rg.freezeRotation=false;
        sp.enabled = false;
        sr.sprite = fly;
    }
    private void Xian()//小鸟划线的脚本
    {
        if(x==0 || (transform.position-new Vector3(x,y,0)).magnitude>0.35)
        {
            PP2 = Instantiate(PP) as GameObject;
            PP2.transform.position = transform.position;
            float v3 = (size % 3 + 2F) / 10 ;
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

    private void Itzs()
    {
        GameManage.instance.Itz();
    }
    private void Next() // 下一个小鸟的飞出
    {
        Invoke("Itzs", 1F);
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
            GameManage.instance.Win();
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)   //小鸟碰到东西后，开始消失判定
    {
        if (!Live && Live2)  //防止使用前小鸟的碰撞
        {
            Live2 = false;
            sr.sprite = hurt;
            Invoke("Hide", 2.5F);
        }
        else if (!Live2 && collision.relativeVelocity.magnitude > 4)    //撞击后存在时间延长
        {
            Stime = 2.5F;
            if (collision.relativeVelocity.magnitude > 6)
                Instantiate(boom, transform.position, Quaternion.identity);
        }
    }
    private void OnMouseDown()
    {
        put = true;
        rg.isKinematic = true;
    }
    private void OnMouseUp()
    {
        put = false;
        rg.isKinematic = false;
        //划线禁用
        po_l.enabled = false;
        po_r.enabled = false;
        if (active)
        {
            Invoke("Fly", 0.1F);
            Pad.transform.position = new Vector3(-5.39F, -0.51F, 0);
            Line(Pad);
            rg.constraints = ~RigidbodyConstraints2D.FreezePosition;
        }
    }

    public virtual void Show()
    {
        active = false;
    }
    private void Update()
    {
        if (put && active && Live)//鼠标按下
        {
            sr.sprite = pull;
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);//造成鸟相机一平面，相机无法照射
            transform.position += new Vector3(0, 0, -Camera.main.transform.position.z);//减去z轴偏移
            //transform.position += new Vector3(0, 0, 10);//直接向前移就ok
            if (Vector3.Distance(transform.position, position_r.position) > maxdis)
            {
                Vector3 pos = (transform.position - position_r.position).normalized;//获得向量方向(单位化)
                pos *= maxdis;                                                      //确定向量长度
                transform.position = pos + position_r.position;
            }
        }
        if (Live)
        {
            Line(gameObject);
        }
        if (!Live && Live2)
        {
            Xian();
            if (active && Input.GetMouseButtonDown(0))
            {
                Show();
            }
        }
    }
}
