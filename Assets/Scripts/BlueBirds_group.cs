using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBirds_group : MonoBehaviour
{
    private GameObject Fa;
    private bool Tc;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Tc = true;
    }
}
    
