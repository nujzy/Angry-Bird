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
    public GameObject boom; //��Ӷ�����С��ı�ըЧ��
    public GameObject PP;   //���������ͼ��С���ƨƨ(�����߹켣)
    [HideInInspector]
    public GameObject PP2;  //pp�Ŀ�¡
    protected float x;      //x������
    protected float y;
    private float size;
    protected List<GameObject> PPs;  //���ڴ洢��ͳһɾ��PP2

    public bool active = false;      //С���Ƿ��ǻ��
    public bool Live = true;         //�ж�С���Ƿ�һ�ػ���(�ɳ�ȥ��������)
    private bool Live2 = true;       //�ж�С���Ƿ���ػ���(��������������)
    private float Stime;             //С����ʧʱ��(����������ʱ������

    [HideInInspector]
    public SpringJoint2D sp;
    [HideInInspector]
    public Rigidbody2D rg;
    protected CircleCollider2D circle;

    //������ر���

    //С����ͼ��ر���
    protected SpriteRenderer sr;
    public Sprite awak; //��ʼʱ
    public Sprite pull; //��ȡʱ
    public Sprite fly;  //����ʱ
    public Sprite hurt; //����ʱ
    public Sprite link; //գ�۶���
    public Sprite open; //���춯��

    //������ر���
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
       
        if (rg.mass<2)
            Invoke("Anime_Jump", a);
    }
    private void Anime_Jump()
    {
        if(Mynum >= GameManage.instance.Num && Live)
        {
            if (Random.Range(0, 2) == 0)
            {
                rg.velocity = new Vector2(rg.velocity.x, rg.velocity.y + 2.5F);
            }
            else
            {
                rg.velocity = new Vector2(rg.velocity.x, rg.velocity.y + 3.75F);
                int a = Random.Range(-1, 2);    //-1,0,1               
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
        AudioPlay(fly_Audio);
        AudioPlay(shot);
        Next();
        Live = false;
        rg.freezeRotation=false;
        sp.enabled = false;
        sr.sprite = fly;
    }
    protected void Xian(Transform targets)//С���ߵĽű�
    {
        if (x ==0 || (targets.position-new Vector3(x,y,0)).magnitude>0.35)
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
            AudioPlay(dst_Audio);
            GameManage.instance.Win();
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)   //С�����������󣬿�ʼ��ʧ�ж�
    {
        if (!Live && Live2)  //��ֹʹ��ǰС�����ײ
        {
            Live2 = false;
            AudioPlay(coll);
            sr.sprite = hurt;
            Invoke("Hide", 2.5F);
        }
        else if (!Live2 && (collision.relativeVelocity.magnitude > 3 || rg.velocity.magnitude != 0))    //ײ�������ʱ���ӳ�
        {
            Stime = 2.5F;
            if (collision.relativeVelocity.magnitude > 6)
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
        if (Mynum >= GameManage.instance.Num && rg.velocity.y == 0)
        {
            rg.velocity = new Vector2(rg.velocity.x, rg.velocity.y + 3 / rg.mass);
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
    private void Update()
    {
        if (put && active && Live)//��갴��
        {
            sr.sprite = pull;
            circle.enabled = false;
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);   //�����һƽ�棬����޷�����
            transform.position += new Vector3(0, 0, -Camera.main.transform.position.z); //��ȥz��ƫ��
            if (Vector3.Distance(transform.position, position_r.position) > maxdis)
            {
                Vector3 pos = (transform.position - position_r.position).normalized;    //�����������(��λ��)
                pos *= maxdis;                                                          //ȷ����������
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