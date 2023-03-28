using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossV2Controller : MonoBehaviour {

    public int health;
    private BoxCollider2D boxCollider;
    private Animator animator;
    public GameObject bullet;
    private GameObject finish;
    private Transform firePosition;
    private Transform firePosition2;
    private bool isDead = false;
    private Transform target;
    private float distance;
 


    // Use this for initialization
    void Start()
    {
        health = 8;
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        animator = gameObject.GetComponent<Animator>();
        GameObject myObject = GameObject.Find("Player");
        target = myObject.transform;
       
        firePosition = transform.Find("FirePosition1");
        firePosition2 = transform.Find("FirePosition2");
        InvokeRepeating("Shoot", 0, 1);

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            health--;
           
        }
        if (health == 0)
        {
            GetComponent<AudioSource>().Play();
            boxCollider.isTrigger = true;
            animator.SetBool("Dead", true);
            Invoke("LoadNext", 1f);
            


        }
    }

    private void Shoot()
    {
        distance = Mathf.Abs(target.position.y - transform.position.y);
        if (distance < 10f && health != 0)
        {

            Instantiate(bullet, firePosition.position, Quaternion.identity);
            Instantiate(bullet, firePosition2.position, Quaternion.identity);
        }
    }
    private void LoadNext()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

