using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBirds : Birds
{
    public Sprite Speed;
    public override void Show()
    {
        base.Show();
        ShowPP();
        sr.sprite = Speed;
        rg.velocity *= 2.35f;
    }
    private new void OnCollisionEnter2D(Collision2D collision)  //��������
    {
        base.OnCollisionEnter2D(collision);
        var tag = collision.collider.tag;
        switch (tag)
        {
            case "glass":   //����
                
                break;
            case "wood":    //ľͷ
                Li *= 2F;
                break;
            case "iron":    //����

                break;
            case "pig":
                Li *= 1.75F;
                break;
        }
    }
}
