using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour {
    public Vector2 speed;
    Rigidbody2D rigidBody;
    public float spinningSpeed;
    public float speedx;
    public bool isSpinning;
    PlayerManager playerManager = new PlayerManager();
	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = speed;
        if (isSpinning)
        {
           
                rigidBody.AddRelativeForce(Vector2.up * speedx, ForceMode2D.Impulse);
                rigidBody.angularVelocity = spinningSpeed;
          
        }
	}

    // Update is called once per frame
    void Update()
    {
        rigidBody.velocity = speed;
        if (isSpinning)
        {
           
                rigidBody.AddRelativeForce(Vector2.up * speedx, ForceMode2D.Impulse);
                rigidBody.angularVelocity = spinningSpeed;
            


        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Water" ||
            collision.gameObject.tag == "DeathLayer")
        {
            Destroy(gameObject, 0);
        }
        
    }
}
