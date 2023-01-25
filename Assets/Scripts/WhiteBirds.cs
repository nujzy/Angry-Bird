using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBirds : Birds
{
    public Sprite Used;
    private GameObject Son;
    private bool Isrote;

    private void Start()
    {
        Son = transform.GetChild(0).gameObject;
    }
    public override void Show()
    {
        base.Show();
        ShowPP();
        hurt = Used;
        circle.radius = 0.23F;
        sr.sprite = Used;
        Son.SetActive(true);
        Son.transform.position = transform.position + new Vector3(0, -0.9F, 0);
        rg.velocity += new Vector2(0, 8);
        rg.freezeRotation = false;
        rg.angularVelocity += 8848;
        Rigidbody2D srg = Son.GetComponent<Rigidbody2D>();
        srg.velocity += new Vector2(0, -4);
        Isrote = true;
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
