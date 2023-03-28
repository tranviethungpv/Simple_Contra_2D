using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    Rigidbody2D rigidBody;
    private bool isSpinning = true;
    public float spinningSpeed;
    public int powerUpType;
    public float speedx;
    public Vector2 speed;
    private BoxCollider2D boxCollider;
    private GameObject player;
    private Transform target;
    private float distance;
    private bool startMoving = false;
    public bool powerUpB = false;
    public bool powerUpL = false;
    public bool powerUpM = false;
    public bool isMoving;

    public GameObject powerUpBPref;
    public GameObject powerUpMPref;
    public GameObject powerUpLPref;

    private Animator animator;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        GameObject myObject = GameObject.Find("Player");
        target = myObject.transform;
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        if (!isMoving)
        {
            boxCollider.isTrigger = true;
           
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        distance = Mathf.Abs(transform.position.x - target.position.x);
        
        if (distance <= 10f)
        {
            
            startMoving = true;
        }
        if(distance <=10f && !isMoving)
        {
            if (powerUpB)
            {
                animator.SetBool("animationB", true);
                animator.SetBool("animationL", false);
                animator.SetBool("animationM", false);
            }
            if (powerUpL)
            {
                animator.SetBool("animationL", true);
                animator.SetBool("animationB", false);
                animator.SetBool("animationM", false);
            }
            if (powerUpM)
            {
                animator.SetBool("animationM", true);
                animator.SetBool("animationL", false);
                animator.SetBool("animationB", false);
            }
            
            boxCollider.isTrigger = false;

        }
        if (isSpinning && startMoving && isMoving)
        {
            rigidBody.constraints = RigidbodyConstraints2D.None;
            rigidBody.angularVelocity = spinningSpeed;
            rigidBody.velocity = speed;
            rigidBody.AddRelativeForce(Vector2.up * speedx, ForceMode2D.Impulse);
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
           
            if (powerUpB)
            {
                Instantiate(powerUpBPref, transform.position, Quaternion.identity);
            }
            if (powerUpL)
            {
                Instantiate(powerUpLPref, transform.position, Quaternion.identity);
            }
            if (powerUpM)
            {
                Instantiate(powerUpMPref, transform.position, Quaternion.identity);
            }
            Destroy(gameObject, 0);
        }
       
    }
}
