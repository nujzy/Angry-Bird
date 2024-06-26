using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBirds : Birds
{
    private int Prefer_Num=0;
    private bool Isbom;
    public Sprite[] Prefer = new Sprite[3];
    private List<GameObject> Moves = new List<GameObject>();
    public GameObject Fire;

    private void Booms()
    {
        if (!Isbom)
        {
            Isbom = true;
            List<GameObject> list = GameManage.instance.Movebles;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] != null)
                {
                    if (Vector3.Distance(transform.position, list[i].transform.position) < 5)
                    {
                        Moves.Add(list[i]);
                    }
                }
            }
            for (int i = 0; i < Moves.Count; i++)
            {
                if (Moves[i] != null)
                {
                    float Dis = Vector3.Distance(transform.position, Moves[i].transform.position);
                    Rigidbody2D srg = Moves[i].GetComponent<Rigidbody2D>();
                    if (Dis < 2F && Moves[i].GetComponent<Collison_Moveble>() != null)
                    {
                        Moves[i].GetComponent<Collison_Moveble>().Ht(12 * (3F - Dis));
                    }
                    float speed = 10.5F / Mathf.Pow(Dis,1.35F)+0.75F / Mathf.Pow(srg.mass,1.25F);                  
                    if (Moves[i].tag == "pig")
                    {
                        speed += 1.5F;
                    }
                    Vector2 Direction = (new Vector2(srg.position.x, srg.position.y) - new Vector2(transform.position.x, transform.position.y)).normalized * speed;
                    if(Moves[i] != gameObject)
                        Moves[i].GetComponent<Rigidbody2D>().velocity += Direction;
                }
            }
            if(!Live2)
            {
                AudioPlay(yell);
            }
            Stime = 0;
            Hide();
            Instantiate(Fire, transform.position, Quaternion.identity);
        }
    }
    private void TimeOut()
    {
        sr.sprite = Prefer[Prefer_Num];
        if (Prefer_Num == 2)
        {
            Invoke("Booms", 0.5F);
        }
        else
        { 
            Prefer_Num++;
            Invoke("TimeOut", 0.25F + Prefer_Num / 10);
        }
    }
    public override void Show()
    {
        base.Show();
        Booms();
    }
    private new void OnCollisionEnter2D(Collision2D collision) 
    {
        if(!Live && Live2)
        {
            Live2 = false;
            AudioPlay(coll);
            TimeOut();
        }
    }
    private new void Update()
    {
        base.Update();
        if(Input.GetMouseButtonDown(0) && !Live2 )
        {
            Booms();
        }
    }
}