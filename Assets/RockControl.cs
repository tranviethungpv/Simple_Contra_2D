using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockControl : MonoBehaviour {
    private GameObject player;
    private Rigidbody2D rigidBody;
    private Transform target;
    private float distanceY;
    private bool enough = false;
    public float offset;
    // Use this for initialization
    void Start () {
        GameObject myObject = GameObject.Find("Player");
        target = myObject.transform;
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
    }
	
	// Update is called once per frame
	void Update () {
        distanceY =Mathf.Abs(transform.position.y - target.position.y);
            if(distanceY <= 7f)
        {
            enough = true;
            rigidBody.constraints = RigidbodyConstraints2D.None;
        }
	}
    private void fallDown()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - offset);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "DeathLayer")
        {
            Destroy(gameObject, 0);
        }
        if(collision.gameObject.tag== "GROUND" && enough)
        {
            Invoke("fallDown", 1f);
        }
    }
}
