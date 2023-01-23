using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBirds : Birds
{
    public Sprite Used;
    private GameObject Son;

    private void Start()
    {
        Son = transform.GetChild(0).gameObject;
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
                
                break;
            case "iron":    //����
                Li *= 2F;
                break;
            case "pig":
                Li *= 1.75F;
                break;
        }
    }
}
