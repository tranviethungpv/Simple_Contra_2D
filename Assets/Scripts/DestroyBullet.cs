using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    public float delay;
    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, delay);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "GROUND")
        {
            Destroy(gameObject, 0);
        }

    }
    // Update is called once per frame
}
