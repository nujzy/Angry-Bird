using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBirds : Birds
{
    public Sprite Sped;
    public override void Show()
    {
        base.Show();
        ShowPP();
        sr.sprite = Sped;
        rg.velocity *= 2.15F;
    }
}
