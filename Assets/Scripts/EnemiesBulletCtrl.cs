using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesBulletCtrl : MonoBehaviour {

    private Transform player;
    Rigidbody2D rigidBody;
    private float xSpeed = 10f;
    private float y;
    private float x;
    private bool xDistance;
    private bool yDistance;




    void Start() {
        GameObject myObject = GameObject.Find("Player");
        player = myObject.transform;

        rigidBody = GetComponent<Rigidbody2D>();
        y = (player.position.y - transform.position.y);
        x = (player.position.x - transform.position.x);

        if (x > 0)
        {
            xDistance = true;
        }
        else xDistance = false;
        if (y > 0)
        {
            yDistance = true;
        }
        else
        yDistance = false;

       

        if (!xDistance)
        {
           // x = -x;
        }
        if (!yDistance)
        {
          //  y = -y;
        }
       
        rigidBody.velocity = new Vector2(x, y).normalized * 7;
       
   
    }
	
	// Update is called once per frame
	void Update () {
      
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject, 0);
        }

    }

}
