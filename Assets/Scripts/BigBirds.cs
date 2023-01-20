using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBirds : Birds
{
    private void Start()
    {
        rg.mass = 5.8F;
    }
    private void OnCollisionEnter2D(Collision2D collision)  //¿¹ÐÔÉèÖÃ
    {
        CommonCollision(collision);
        var tag = collision.collider.tag;
        switch (tag)
        {
            case "glass":   //²£Á§
                Li *= 1.75F;
                break;
            case "wood":    //Ä¾Í·
                Li *= 1.75F;
                break;
            case "iron":    //Ìú¿é
                Li *= 1.75F;
                break;
            case "pig":
                Li *= 1.75F;
                break;
        }
    }
}
