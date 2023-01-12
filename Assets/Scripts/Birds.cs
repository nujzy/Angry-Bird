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
    public GameObject boom; //��Ӷ�����С��ı�ըЧ��
    public GameObject PP;   //���������ͼ��С���ƨƨ(�����߹켣)
    [HideInInspector]
    public GameObject PP2;  //pp�Ŀ�¡
    protected float x;    //x������
    protected float y;
    private float size;
    protected List<GameObject> PPs;   //���ڴ洢��ͳһɾ��PP2

    public bool active = false;      //С���Ƿ��ǻ��
    public bool Live = true;         //�ж�С���Ƿ�һ�ػ���(�ɳ�ȥ��������)
    private bool Live2 = true;      //�ж�С���Ƿ���ػ���(��������������)
    private float Stime;            //С����ʧʱ��(����������ʱ��ӿ�

    [HideInInspector]   //���ع��б�����inspector�����Ŀ��ӻ�
    public SpringJoint2D sp;
    [HideInInspector]
    public Rigidbody2D rg;

    //С����ͼ��ر���
    protected SpriteRenderer sr;
    public Sprite awak;//��ʼʱ
    public Sprite pull;//��ȡʱ
    public Sprite fly;//����ʱ
    public Sprite hurt;

    private void Awake()//��Ϸ��ʼ��(����Start())
    {
        PPs = new List<GameObject> ();
        sp = GetComponent<SpringJoint2D>(); //GetComponent ���ز������
        rg = GetComponent<Rigidbody2D>();   
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = awak;       
    }
    private void Line(GameObject obj) //  ��������
    {
        Vector3 Dis = (obj.transform.position - position_r.position).normalized;
        Dis *=0.2F;
        Vector3 Q= obj.transform.position+Dis;
        float angel = Mathf.Atan2(Dis.y, Dis.x)*Mathf.Rad2Deg;  //mathf.rad2deg ����ת�Ƕ�
        Pad.transform.rotation = Quaternion.AngleAxis(angel, position_r.position);  //Χ���ҵ�����ת angel��
        Pad.transform.position = Q;
        po_l.enabled = true;
        po_r.enabled = true;
        po_r.SetPosition(0, position_r.position);
        po_r.SetPosition(1, Pad.transform.position);
        po_l.SetPosition(0, position_l.position);
        po_l.SetPosition(1, Pad.transform.position);


    }
    private void Fly()  //С��ķ���
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
    private void Xian()//С���ߵĽű�
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
    public void Dst()   //ƨƨ��ɾ���ű�
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
    private void Next() // ��һ��С��ķɳ�
    {
        Invoke("Itzs", 1F);
    }
    public void Hide() //С�񵥶������ؽű�
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
    public void OnCollisionEnter2D(Collision2D collision)   //С�����������󣬿�ʼ��ʧ�ж�
    {
        if (!Live && Live2)  //��ֹʹ��ǰС�����ײ
        {
            Live2 = false;
            sr.sprite = hurt;
            Invoke("Hide", 2.5F);
        }
        else if (!Live2 && collision.relativeVelocity.magnitude > 4)    //ײ�������ʱ���ӳ�
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
        //���߽���
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
        if (put && active && Live)//��갴��
        {
            sr.sprite = pull;
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);//��������һƽ�棬����޷�����
            transform.position += new Vector3(0, 0, -Camera.main.transform.position.z);//��ȥz��ƫ��
            //transform.position += new Vector3(0, 0, 10);//ֱ����ǰ�ƾ�ok
            if (Vector3.Distance(transform.position, position_r.position) > maxdis)
            {
                Vector3 pos = (transform.position - position_r.position).normalized;//�����������(��λ��)
                pos *= maxdis;                                                      //ȷ����������
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
