using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBirds : Birds
{
    private int Prefer_Num=0;
    private bool Isbom;
    public Sprite[] Prefer = new Sprite[3];
    private List<Rigidbody2D> Moves = new List<Rigidbody2D>();
    private void Booms()
    {
        if (!Isbom)
        {
            Isbom = true;
            List<Rigidbody2D> list = GameManage.instance.Movebles;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] != null)
                {
                    if (Vector3.Distance(transform.position, list[i].transform.position) < 3.5)
                    {
                        Moves.Add(list[i]);
                    }
                }
            }
            for (int i = 0; i < Moves.Count; i++)
            {
                if (Moves[i] != null)
                {
                    float speed = 9F / Vector3.Distance(transform.position, Moves[i].transform.position);
                    Vector2 Direction = (new Vector2(Moves[i].position.x, Moves[i].position.y) - new Vector2(transform.position.x, transform.position.y)).normalized * speed;
                    Moves[i].velocity += Direction;
                }
            }
            if(!Live2)
            {
                AudioPlay(yell);
            }
            Stime = 0;
            Hide();
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
            Invoke("TimeOut", 0.7F - Prefer_Num / 10);
        }
    }
    public override void Show()
    {
        base.Show();
        Booms();
    }
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if(!Live && Live2)
        {
            Live2 = false;
            AudioPlay(coll);
            TimeOut();
        }
        var tag = collision.collider.tag;
        switch (tag)
        {
            case "glass":   //²£Á§
                Li *= 1F;
                break;
            case "wood":    //Ä¾Í·
                Li *= 1F;
                break;
            case "iron":    //Ìú¿é
                Li *= 2F;
                break;
            case "pig":
                Li *= 1.65F;
                break;
        }
    }
    private void Update()
    {
        base.Update();
        if(Input.GetMouseButtonDown(0) && !Live2 )
        {
            Booms();
        }
    }
}
