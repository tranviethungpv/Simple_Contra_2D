using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxPowerUpB : MonoBehaviour {

    private Vector2 speed;
    private Rigidbody2D rigidBody;
    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.AddForce(new Vector2(Random.Range(500, 80), Random.Range(500, 600)));
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "DeathLayer")
        {
            Destroy(gameObject, 0);
        }
    }
}
