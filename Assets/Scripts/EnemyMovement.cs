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
    private Transform groundSensor;
    public bool onGround;
    private float speed = 5;

    public LayerMask ground;
	// Use this for initialization
	void Start () {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        GameObject myObject = GameObject.Find("Player");
        target = myObject.transform;
        if (isMoving)
        {
            groundSensor = transform.Find("GroundSensor");
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (isMoving)
        {
            //Xác định xem có đứng trên mặt đất hay ko
            //Kiểm tra xem có bất kỳ đối tượng nào thuộc lớp ground nằm trong vùng hình tròn bán kính 0.1 tại vị trí của groundSensor hay không.
            onGround = Physics2D.OverlapCircle(groundSensor.position, 0.1f, ground);
        }
       
        //Nếu khoảng cách với Player < 20 thì chạy về phía Player
        distance = Mathf.Abs(transform.position.x - target.position.x);
        if (distance < 20f)
        {
            rigidbody2d.velocity = new Vector3(-speed, rigidbody2d.velocity.y, 0);
            animator.SetFloat("Speed", Mathf.Abs(rigidbody2d.velocity.x));

        }
        if (isDead || !isMoving)
        {
            rigidbody2d.velocity = new Vector3(0, rigidbody2d.velocity.y, 0);
            animator.SetFloat("Speed", Mathf.Abs(rigidbody2d.velocity.x));
        }

    }

    //Xử lí va chạm
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Trúng đạn -> chết
        if(collision.gameObject.tag == "Bullet")
        {   GetComponent<AudioSource>().Play();
            Destroy(gameObject, 2f);
            isDead = true;
            animator.SetBool("Dead", true);
           
        }
        //Đi vào vùng nguy hiểm -> chết
        if (collision.gameObject.tag == "DeathLayer" || collision.gameObject.tag == "Bounds")
        {
            Destroy(gameObject, 0f);
        }
        //Rơi xuống nước -> chết
        if (collision.gameObject.tag == "Water")
        {
            Destroy(gameObject, 0f);
            isDead = true;
            animator.SetBool("Dead", true);
        }
    }
}
