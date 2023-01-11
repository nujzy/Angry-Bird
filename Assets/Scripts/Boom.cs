using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public void Stre()
    {
        gameObject.SetActive(true);     //初始化为true
    }
    public void Des()
    {
        gameObject.SetActive(false);    //因为还要用，所以用setactive
    }
}
