using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBirds : Birds
{
    private GameObject[] Son=new GameObject[2];
    private SpriteRenderer[] S_sr=new SpriteRenderer[2];
    private Rigidbody2D[] S_rb=new Rigidbody2D[2];
    private CircleCollider2D[] S_cc = new CircleCollider2D[2];

    /*private float[] size2;
    private float[] xp;
    private float[] yp;*/
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
    /*private void Xian(Transform targets,int Ct)//小鸟划线的脚本
    {
        xp= new float[Ct];
        yp= new float[Ct];
        size2 = new float[Ct];
        for (int j = 0; j < Ct; j++)
        {
            if (xp[j] == 0 || (targets.position - new Vector3(xp[j], yp[j], 0)).magnitude > 0.35)
            {
                PP2 = Instantiate(PP, targets.position, Quaternion.identity) as GameObject;
                float v3 = (size2[j] % 3 + 2.45F) / 10;
                size2[j]++;
                PP2.transform.localScale = new Vector3(v3, v3, v3);
                xp[j] = PP2.transform.position.x;
                yp[j] = PP2.transform.position.y;
                PPs.Add(PP2);
            }
        }
    }*/
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
        ShowPP();
        Son[0].SetActive(true);
        Son[1].SetActive(true);
        for (int i = 0; i < 2; i++)
        {
            Son[i].SetActive(true);
            S_cc[i].enabled = false;
            Son[i].transform.position = transform.position;
            S_rb[i].velocity = new Vector2(rg.velocity.x, rg.velocity.y + 2.5F * (i*2-1));
        }
        Invoke("Cllon", 0.15F);
    }
    private void OnCollisionEnter2D(Collision2D collision)  //抗性设置
    {
        CommonCollision(collision);
        var tag = collision.collider.tag;
        switch (tag)
        {
            case "glass":   //玻璃
                Li *= 2F;
                break;
            case "wood":    //木头
                Li *= 0.75F;
                break;
            case "iron":    //铁块
                Li *= 0.75F;
                break;
            case "pig":
                Li *= 1.65F;
                break;
        }
    }
    /*private void FixedUpdate()
    {
        if (!Live && !active)
        {
            for (int i = 0; i < 2; i++)
            {
                if (S_cc.tou)
                    Xian(Son[i].transform,2);
            }
        }
    }*/
}
