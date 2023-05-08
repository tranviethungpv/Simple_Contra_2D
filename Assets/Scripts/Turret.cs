using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {
    public int health;
    private Transform target;
    public GameObject enemyBullet;
    private float distanceX;
    private float distance;
    private float distanceY;
    private Transform firePosition;
    private bool temp = false;
    private bool facingRight = false;
    private bool isDead = false;
    private CircleCollider2D circleCollider;
    private Animator animator;
    public GameObject particles;
    public float bulletSpeed;
    // Use this for initialization
    void Start () {
        GameObject myObject = GameObject.Find("Player");
        target = myObject.transform;
        firePosition = transform.Find("EnemyFirePoint");
        circleCollider = gameObject.GetComponent<CircleCollider2D>();
        animator = gameObject.GetComponent<Animator>();
        if (!isDead)
        {
            InvokeRepeating("Shoot", 0, 1);
        }
    }
	
	// Update is called once per frame
	void Update () {
        distanceX = Mathf.Abs(transform.position.x - target.position.x);
        distanceY = Mathf.Abs(transform.position.y - target.position.y);
        distance = distanceY + distanceX;
        if (distance < 20f)
        {
            temp = true;
        }
        else temp = false;
       
    }

    //Hàm xử lí việc bắn đạn
    private void Shoot()
    {

        if (temp && !isDead)
        {
            // Tính toán vector hướng từ turret đến player
            Vector3 direction = target.position - transform.position;
            // Tạo enemyBullet và thiết lập velocity theo vector hướng này
            GameObject bullet = Instantiate(enemyBullet, firePosition.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * bulletSpeed;
        }

    }

    //Xử lí va chạm
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            health--;
        }
         if (health <= 0)
        {
            isDead = true;
            GetComponent<AudioSource>().Play();
            Instantiate(particles, transform.position, transform.rotation);
            Destroy(gameObject, 0);
        }
        else
        {
            isDead = false;
        }
    }
}
