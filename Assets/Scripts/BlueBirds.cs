using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBirds : Birds
{
    private GameObject[] Son=new GameObject[2];
    private SpriteRenderer[] S_sr=new SpriteRenderer[2];
    private Rigidbody2D[] S_rb=new Rigidbody2D[2];
    private CircleCollider2D[] S_cc = new CircleCollider2D[2];

    private void Start()
    {
        for(int i = 0; i < 2; i++)
        {
            Son[i] = transform.GetChild(i).gameObject;
            S_sr[i] = Son[i].GetComponent<SpriteRenderer>();
            S_cc[i] = Son[i].GetComponent<CircleCollider2D>();
            S_rb[i] = Son[i].GetComponent<Rigidbody2D>();
        }
    }
    private void Cllon()
    {
        for (int i = 0; i < 2; i++)
        {
            S_cc[i].enabled = true;
        }
    }
    public override void Show()
    {
        base.Show();
        Son[0].SetActive(true);
        Son[1].SetActive(true);
        for (int i = 0; i < 2; i++)
        {
            Son[i].SetActive(true);
            S_cc[i].enabled = false;
            Son[i].transform.position = transform.position;
            Son[i].GetComponent<BlueBirds_group>().size = size;
            S_rb[i].transform.position += new Vector3(0,0.2F*(i*2-1),0);
            S_rb[i].velocity = new Vector2(rg.velocity.x, rg.velocity.y + 3F * (i*2-1));
            transform.DetachChildren();
        }
        ShowPP();
        Invoke("Cllon", 0.15F);
    }
    private new void OnCollisionEnter2D(Collision2D collision)  //¿¹ÐÔÉèÖÃ
    {
        base.OnCollisionEnter2D (collision);
        var tag = collision.collider.tag;
        switch (tag)
        {
            case "glass":   //²£Á§
                Li *= 2F;
                break;
            case "wood":    //Ä¾Í·
                Li *= 0.75F;
                break;
            case "iron":    //Ìú¿é
                Li *= 0.75F;
                break;
            case "pig":
                Li *= 1.65F;
                break;
        }
    }
}
