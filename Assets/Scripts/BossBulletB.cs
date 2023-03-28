using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletB : MonoBehaviour {
    private Vector2 speed;
    private Rigidbody2D rigidBody;
    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.AddForce(new Vector2(Random.Range(-300, 300), Random.Range(300, 400)));

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
}
