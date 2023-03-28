using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    private Rigidbody2D rigidbody2d;
    private bool isDead = false;
    private Animator animator;
    public bool isMoving;
    private float distance;
    private Transform target;
    private Transform cliffSensor;
    public bool cliffAhead;
    private Transform groundSensor;
    public bool onGround;
    private float speed = 5;

    private bool reacted;

    public LayerMask ground;
	// Use this for initialization
	void Start () {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        GameObject myObject = GameObject.Find("Player");
        target = myObject.transform;
        if (isMoving)
        {
            cliffSensor = transform.Find("CliffSensor");
            groundSensor = transform.Find("GroundSensor");
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (isMoving)
        {
            onGround = Physics2D.OverlapCircle(groundSensor.position, 0.1f, ground);
            cliffAhead = !Physics2D.OverlapCircle(cliffSensor.position, 0.1f, ground);
        }
       
        distance = Mathf.Abs(transform.position.x - target.position.x);
        if (distance < 20f)
        {
            rigidbody2d.velocity = new Vector3(-speed, rigidbody2d.velocity.y, 0);
            animator.SetFloat("Speed", Mathf.Abs(rigidbody2d.velocity.x));
            if (cliffAhead && onGround && !reacted)
            {
                ReactToCliff(Random.Range(0,3));
                   
            }
            if(!cliffAhead && onGround && reacted)
            {
                reacted = false;
            }
        }
        if (isDead || !isMoving)
        {
            rigidbody2d.velocity = new Vector3(0, rigidbody2d.velocity.y, 0);
            animator.SetFloat("Speed", Mathf.Abs(rigidbody2d.velocity.x));
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {   GetComponent<AudioSource>().Play();
            Destroy(gameObject, 2f);
            isDead = true;
            animator.SetBool("Dead", true);
           
        }
        if (collision.gameObject.tag == "DeathLayer" || collision.gameObject.tag == "Bounds")
        {
            Destroy(gameObject, 0f);
        }
        if (collision.gameObject.tag == "Water")
        {
            Destroy(gameObject, 0f);
            isDead = true;
            animator.SetBool("Dead", true);
            

        }
        if (collision.gameObject.tag == "Bounds")
        {
            Destroy(gameObject, 0f);
        }
    }
    private void ReactToCliff(float type)
    {
        if(type == 0)
        {
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, 4);
            
        }
        if (type > 1.5)
        {
            speed = -speed;
            transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
        }
        reacted = true;
    }
}
