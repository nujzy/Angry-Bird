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
        Rigidbody2D srg = Son.GetComponent<Rigidbody2D>();
        srg.velocity += new Vector2(0, -4);
        transform.DetachChildren();
    }
    private void FixedUpdate()
    {
        if(!active && Live2)
            transform.Rotate(Vector3.forward, -30);
    }
}
