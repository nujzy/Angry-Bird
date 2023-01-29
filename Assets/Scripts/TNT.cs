using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT : Collison_Moveble
{
    private List<GameObject> Moves = new List<GameObject>();
    public GameObject Fire;
    private bool ISboom;
    public AudioClip boom_Audio;

    private new void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.relativeVelocity.magnitude>mins)
        {
            Booms();
        }
    }
    private void OnDestroy()
    {
        Booms();
    }
    private void Booms()
    {
        if (!ISboom)
        {
            ISboom = true;
            List<GameObject> list = GameManage.instance.Movebles;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] != null)
                {
                    if (Vector3.Distance(transform.position, list[i].transform.position) < 3.75F)
                    {
                        Moves.Add(list[i]);
                    }
                }
            }
            for (int i = 0; i < Moves.Count; i++)
            {
                if (Moves[i] != null)
                {
                    float Dis = Vector3.Distance(transform.position, Moves[i].transform.position);
                    Rigidbody2D srg = Moves[i].GetComponent<Rigidbody2D>();
                    if (Dis < 2F && Moves[i].GetComponent<Collison_Moveble>() != null)
                    {
                        Moves[i].GetComponent<Collison_Moveble>().Ht(12 * (3F - Dis));
                    }
                    float speed = 10.5F / Mathf.Pow(Dis, 1.35F) + 0.75F / Mathf.Pow(srg.mass, 1.25F);
                    if (Moves[i].tag is "pig")
                    {
                        speed += 1.5F;
                    }
                    Vector2 Direction = (new Vector2(srg.position.x, srg.position.y) - new Vector2(transform.position.x, transform.position.y)).normalized * speed;
                    if (Moves[i] != gameObject)
                        Moves[i].GetComponent<Rigidbody2D>().velocity += Direction;
                }
            }
            AudioSource.PlayClipAtPoint(boom_Audio, new Vector3(0, 0, 0));
            Instantiate(boom, transform.position, Quaternion.identity);
            Instantiate(Fire, transform.position, Quaternion.identity);
        }
    }
}
