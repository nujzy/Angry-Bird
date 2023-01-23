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

    private new void OnCollisionEnter2D(Collision2D collision)  //¿¹ÐÔÉèÖÃ
    {
        base.OnCollisionEnter2D(collision);
        var tag = collision.collider.tag;
        switch (tag)
        {
            case "glass":   //²£Á§

                break;
            case "wood":    //Ä¾Í·
                
                break;
            case "iron":    //Ìú¿é
                Li *= 2F;
                break;
            case "pig":
                Li *= 1.75F;
                break;
        }
    }
}
