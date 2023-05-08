using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    private Transform target;
    public GameObject enemyBullet;
    private float distanceX;
    private float distance;
    private float distanceY;
    private Transform firePosition;
    private bool temp = false;
    private bool facingRight = false;
    private bool isDead = false;
    private float bulletSpeed = 0.1f;

    // Use this for initialization
    void Start()
    {
        GameObject myObject = GameObject.Find("Player");
        target = myObject.transform;
        firePosition = transform.Find("EnemyFirePoint");
        if (!isDead)
        {
            //Lặp lại hàm bắn đạn
            InvokeRepeating("Shoot", 0, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        distanceX = Mathf.Abs(transform.position.x - target.position.x);
        distanceY = Mathf.Abs(transform.position.y - target.position.y);
        distance = distanceY + distanceX;
        if (distance < 20f)
        {
            temp = true;
        }
        else temp = false;
        Flip();
    }
    //Hàm để bắn
    private void Shoot()
    {
        if (temp && !isDead)
        {
            // Tính toán vector hướng từ enemy đến player
            Vector3 direction = target.position - transform.position;
            // Tạo enemyBullet và thiết lập velocity theo vector hướng này
            GameObject bullet = Instantiate(enemyBullet, firePosition.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * bulletSpeed;
        }
    }

    //Hàm đổi hướng, quay về phía Player
    private void Flip()
    {
        float distance;
        distance = target.position.x - transform.position.x;
        if (distance > 0 && !facingRight || distance < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Nếu dính đạn -> chết
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(gameObject, 1f);
            isDead = true;
        }
    }
}
