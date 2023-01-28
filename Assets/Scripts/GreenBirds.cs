using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBirds : Birds
{
    public Sprite Rote;
    public AudioClip swish;
    private Vector3 Past;

    private int GetSign(float a)
    {
        if(a < 0)
        {
            return -1;
        }
        else if(a > 0)
        {
            return -1;
        }
        return 0;
    }
    private void Play_Swich()
    {
        if (!active && Live2)
        {
            AudioPlay(swish);
            Invoke("Play_Swich", 0.3F);
        }
    }
    private void Rotes()
    {
        Vector3 Pos = -(transform.position - Past).normalized;
        Pos.y *= -1;
        float speed;
        speed = rg.velocity.magnitude;
        if(Live2)
            rg.velocity = Pos * speed;
        // rg.velocity += new Vector2(-rg.velocity.x * 2, rg.velocity.y+(Past.y-transform.position.y)*5);
    }
    public override void Show()
    {
        base.Show();
        sr.sprite = Rote;
        ShowPP();
        Past = transform.position;
        Play_Swich();
        rg.velocity *= 1.25F;
        Invoke("Rotes", 0.1F);
    }
    private void FixedUpdate()
    {
        if (Live2 && !Live)
        {
            float a = 1;
            if (!active)
                a = 2.5F;
            transform.Rotate(Vector3.forward, -10 * GetSign(rg.velocity.x)*a);
        }
    }
}
