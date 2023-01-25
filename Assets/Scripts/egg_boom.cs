using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class egg_boom : MonoBehaviour
{
    public GameObject Boom;
    public GameObject Fire;
    public AudioClip Boom_Audio;
    private List<GameObject> boomList = new List<GameObject>();

    private void Booms(Vector3 Position)
    {
        List<GameObject> list = GameManage.instance.Movebles;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] != null)
            {
                if (Vector3.Distance(Position, list[i].transform.position) < 2)
                {
                    boomList.Add(list[i]);
                }
            }
        }
        for (int i = 0; i < boomList.Count; i++)
        {
            if (boomList[i] != null)
            {
                float Dis = Vector3.Distance(Position, boomList[i].transform.position);
                Rigidbody2D srg = boomList[i].GetComponent<Rigidbody2D>();
                if (Dis < 0.6F && boomList[i].GetComponent<Collison_Moveble>() != null)
                {
                    boomList[i].GetComponent<Collison_Moveble>().Die();
                }
                else if (Dis < 1.5F && boomList[i].GetComponent<Collison_Moveble>() != null)
                {
                    boomList[i].GetComponent<Collison_Moveble>().Ht(15 * (3F - Dis));
                }
                float speed = 2F / Mathf.Pow(Dis, 1.35F) + 0.75F / Mathf.Pow(srg.mass, 1.25F);
                Vector2 Direction = (new Vector2(srg.position.x, srg.position.y) - new Vector2(Position.x, Position.y)).normalized * speed;
                srg.velocity += Direction;
            }
        }
    }
    protected void AudioPlay(AudioClip clips)
    {
        AudioSource.PlayClipAtPoint(clips, new Vector3(0, 0, 0));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 vector3 = transform.position;
        Destroy(gameObject);
        AudioPlay(Boom_Audio);
        Instantiate(Boom, transform.position, Quaternion.identity);
        Instantiate(Fire, transform.position, Quaternion.identity);
        Booms(vector3);   
    }
}
