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
        rg.velocity *= 2.15F;
    }
}
