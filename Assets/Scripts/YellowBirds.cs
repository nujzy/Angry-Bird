using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBirds : Birds
{
    public Sprite Sped;
    public override void Show()
    {
        base.Show();
        PP2 = Instantiate(PP);
        PP2.transform.localScale = new Vector3(0.8F, 0.8F, 0.8F);
        PP2.transform.position = transform.position;
        PPs.Add(PP2);
        x = PP2.transform.position.x;
        y = PP2.transform.position.y;
        sr.sprite = Sped;
        rg.velocity *= 2.15F;
    }
}
