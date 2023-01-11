using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Birds : MonoBehaviour
{
    public float maxdis = 3;
    private bool put=false;

    public Transform position_r;
    public Transform position_l;

    public LineRenderer po_r;
    public LineRenderer po_l;

    public GameObject boom; //��Ӷ�����С��ı�ըЧ��
    public GameObject PP;   //���������ͼ��С���ƨƨ(�����߹켣)
    [HideInInspector]
    public GameObject PP2;  //pp�Ŀ�¡

    public bool active=false;      //С���Ƿ��ǻ��
    public bool Live=true;         //�ж�С���Ƿ�һ�ػ���(�ɳ�ȥ��������)
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
        sp= GetComponent<SpringJoint2D>(); //GetComponent ���ز������
        rg=GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = awak;
    }
    private void Line() //  ��������
    {
        po_l.enabled = true;
        po_r.enabled = true;
        po_r.SetPosition(0, position_r.position);
        po_r.SetPosition(1, transform.position);
        po_l.SetPosition(0, position_l.position);
        po_l.SetPosition(1, transform.position);
    }
    private void Fly()  //С��ķ���
    {
        rg.freezeRotation = false;
        sp.enabled = false;
        sr.sprite = fly;
        Next();
    }
    private void Xian()//С���ߵĽű�
    {
        PP2 = Instantiate(PP);       
        PP2.transform.localScale = new Vector3(0.3F, 0.3F, 0.3F);
        PP2.transform.position = transform.position;
    }
    public void DesX()
    {
        Destroy(PP2);
        print("Live1;"+Live);
        print("Live2:"+Live2);
    }
    private void Itzs()
    {
        GameManage.instance.Itz();
    }
    private void Next() // ��һ��С��ķɳ�
    {
        GameManage.instance.bird.Remove(this);
        Invoke("Itzs", 1F);
    }
    public void Hide() //С�񵥶������ؽű�
    {
        if (Stime !=0)
        {   
            Invoke("Hide", Stime);
            Stime = 0;
        }
        else
        {
            Destroy(gameObject);
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
            Invoke("Hide", 1.5F);
        }
        else if (!Live2 && collision.relativeVelocity.magnitude>4)    //ײ�������ʱ���ӳ�
        {
            Stime = 1.5F;
            if (collision.relativeVelocity.magnitude>1)
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
            this.Live = false;
        }
    }

    public virtual void Show()
    {
        active = false;
    }
    private void Update()
    {
        if (put && active)//��갴��
        {
            sr.sprite = pull;
            transform.position=Camera.main.ScreenToWorldPoint(Input.mousePosition);//��������һƽ�棬����޷�����
            transform.position += new Vector3(0,0,-Camera.main.transform.position.z);//��ȥz��ƫ��
            //transform.position += new Vector3(0, 0, 10);//ֱ����ǰ�ƾ�ok
            if (Vector3.Distance(transform.position, position_r.position)>maxdis)
            {
                Vector3 pos = (transform.position - position_r.position).normalized;//�����������(��λ��)
                pos *= maxdis;                                                      //ȷ����������
                transform.position = pos + position_r.position;
            }
            Line();
        }
        if(!Live && Live2)  //�ɳ�ȥ�󣬴���ǰ
        {
            Xian();
            if(active && Input.GetMouseButtonDown(0))
            {
                Show();
            }
        }
    }
}
