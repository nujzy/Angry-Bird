using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBirds : Birds
{
    private void OnCollisionEnter2D(Collision2D collision)  //��������
    {
        CommonCollision(collision);
        var tag = collision.collider.tag;
        switch (tag)
        {
            case "glass":   //����
                Li *= 1.25F;
                break;
            case "wood":    //ľͷ
                Li *= 1.25F;
                break;
            case "iron":    //����
                Li *= 1.25F;
                break;
            case "pig":
                Li *= 1.75F;
                break;
        }
    }
}
