using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {

    private Animator animator;
    private Rigidbody2D rigidbody2d;
    public float resetX;
    public float resetY;
    public GameObject bulletRight;
    public GameObject bulletLeft;
    public GameObject bulletRightUp;
    public GameObject bulletLeftUp;
    public GameObject bulletRightDown;
    public GameObject bulletLeftDown;
    public GameObject bulletUp;
    public GameObject bulletDown;
    public GameObject bulletF;
    public GameObject bulletFLeft;
    public int shootingType;
    private BoxCollider2D boxCollider;
    private CircleCollider2D circleCollider;
    public float runningSpeed;
    private float speed;
    public float jumpSpeed;
    private Transform firePosition;
    private Transform firePositionW;
    private Transform firePositionS;
    private Transform firePositionWd;
    private Transform firePositionF;
    private float fireRate = 3f;
    float timeToFire = 0;
    public bool facingRight;
    private bool isDead = false;
    public int health;
    private bool readyToFall;
    private bool onWater;
    private bool onGround;
    private bool jump = true;
    public float gravity;
    private bool isVisible;
    public bool horizontalLevel;

  
   

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        firePositionW = transform.Find("FirePointOnlyW");
        firePositionS = transform.Find("FirePointS");
        firePositionWd = transform.Find("FirePointW");
        firePositionF = transform.Find("SpinningFirePoint");
        firePosition = transform.Find("FirePoint");
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        circleCollider = gameObject.GetComponent<CircleCollider2D>();
        facingRight = true;
        isVisible = true;
       

    }

    // Update is called once per frame
    void Update() {
        if (!isDead) { 
        Move(speed);
        Flip();
    }
        if (Input.GetKey(KeyCode.A))
        {
            speed = -runningSpeed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            speed = runningSpeed;
        }
        else
        {
            speed = 0;
        }
        

        if (Input.GetKeyDown(KeyCode.Space) && !jump && !Input.GetKey(KeyCode.S) && !isDead
            && rigidbody2d.velocity.y == 0)
        {
            
            jump = true;
            rigidbody2d.AddForce(new Vector2(rigidbody2d.velocity.x, jumpSpeed));

            animator.SetBool("Ground", false);
        }

       

        if (readyToFall && Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.S) && !jump)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - gravity);
            jump = true;
            animator.SetBool("Ground", false);
           
        }



        ///////////////
        if (Input.GetKey("w"))
        {
            animator.SetBool("Up", true);
        }
        else
            animator.SetBool("Up", false);

        if (Input.GetKey("s"))
        {
            animator.SetBool("Down", true);
        }
        else
            animator.SetBool("Down", false);

        if (Input.GetKey("z") && !onWater && onGround)
        {
            animator.SetBool("Crouch", true);
            speed = 0;
          
        }
        else
            animator.SetBool("Crouch", false);
      
        animator.SetFloat("Speed", Mathf.Abs(rigidbody2d.velocity.x));

        if (Input.GetKey("k") && Time.time > timeToFire && !isDead)
        {
            timeToFire = Time.time + 1 / fireRate;
            Fire();
            
        }
        if (Input.GetKey("k")){
            animator.SetBool("Shooting", true);
            isVisible = true;
        }
        else
            animator.SetBool("Shooting", false);

        if (onWater)
        {
            animator.SetBool("Water", true);
        }
        else
            animator.SetBool("Water", false);
    }

    private void Move(float speed)
    {
        rigidbody2d.velocity = new Vector3(speed, rigidbody2d.velocity.y, 0);
    }

    private void Fire()
    {
        if (shootingType == 0)
        {
            bool anyKey = false;


            if (Input.GetKey("w") && !Input.GetKey("d") && !Input.GetKey("a"))
            {
                Instantiate(bulletUp, firePositionW.position, Quaternion.identity);
                anyKey = true;
            }

            else if (Input.GetKey("s") && !Input.GetKey("d") && !Input.GetKey("a"))
            {
                Instantiate(bulletDown, firePositionS.position, Quaternion.identity);
                anyKey = true;
            }
            if (facingRight)
            {


                if (Input.GetKey("w") && Input.GetKey("d"))
                {
                    Instantiate(bulletRightUp, firePositionWd.position, Quaternion.identity);
                    anyKey = true;
                }
                else if (Input.GetKey("s") && Input.GetKey("d"))
                {
                    Instantiate(bulletRightDown, firePositionS.position, Quaternion.identity);
                    anyKey = true;
                }
                else if (!anyKey)
                {
                    Instantiate(bulletRight, firePosition.position, Quaternion.identity);
                }
            }
            else
            {

                if (Input.GetKey("w") && Input.GetKey("a"))
                {
                    Instantiate(bulletLeftUp, firePositionWd.position, Quaternion.identity);
                    anyKey = true;
                }
                else if (Input.GetKey("s") && Input.GetKey("a"))
                {
                    Instantiate(bulletLeftDown, firePositionS.position, Quaternion.identity);
                    anyKey = true;
                }
                else if (!anyKey)
                {
                    Instantiate(bulletLeft, firePosition.position, Quaternion.identity);
                }
            }
        }
        if (shootingType == 1)
        {
          
            ////////////////////////////////////////////////////////
            bool anyKey = false;


            if (Input.GetKey("w") && !Input.GetKey("d") && !Input.GetKey("a"))
            {
                Instantiate(bulletUp, firePositionW.position, Quaternion.identity);
                Instantiate(bulletRightUp, firePositionW.position, Quaternion.identity);
                Instantiate(bulletLeftUp, firePositionW.position, Quaternion.identity);
                anyKey = true;
            }

            else if (Input.GetKey("s") && !Input.GetKey("d") && !Input.GetKey("a"))
            {
                Instantiate(bulletDown, firePositionS.position, Quaternion.identity);
                Instantiate(bulletLeftDown, firePositionS.position, Quaternion.identity);
                Instantiate(bulletRightDown, firePositionS.position, Quaternion.identity);
                anyKey = true;
            }
            if (facingRight)
            {


                if (Input.GetKey("w") && Input.GetKey("d"))
                {
                    Instantiate(bulletRightUp, firePositionWd.position, Quaternion.identity);
                    Instantiate(bulletRight, firePositionWd.position, Quaternion.identity);
                    Instantiate(bulletUp, firePositionW.position, Quaternion.identity);
                    anyKey = true;
                }
                else if (Input.GetKey("s") && Input.GetKey("d"))
                {
                    Instantiate(bulletRightDown, firePositionS.position, Quaternion.identity);
                    Instantiate(bulletRight, firePositionS.position, Quaternion.identity);
                    Instantiate(bulletDown, firePositionS.position, Quaternion.identity);

                    anyKey = true;
                }
                else if (!anyKey)
                {
                    Instantiate(bulletRight, firePosition.position, Quaternion.identity);
                    Instantiate(bulletRightUp, firePosition.position, Quaternion.identity);
                    Instantiate(bulletRightDown, firePosition.position, Quaternion.identity);
                }
            }
            else
            {

                if (Input.GetKey("w") && Input.GetKey("a"))
                {
                    Instantiate(bulletLeftUp, firePositionWd.position, Quaternion.identity);
                    Instantiate(bulletLeft, firePositionWd.position, Quaternion.identity);
                    Instantiate(bulletUp, firePositionW.position, Quaternion.identity);
                    
                    anyKey = true;
                }
                else if (Input.GetKey("s") && Input.GetKey("a"))
                {
                    Instantiate(bulletLeftDown, firePositionS.position, Quaternion.identity);
                    Instantiate(bulletLeft, firePositionS.position, Quaternion.identity);
                    Instantiate(bulletDown, firePositionS.position, Quaternion.identity);
                    anyKey = true;
                }
                else if (!anyKey)
                {
                    Instantiate(bulletLeft, firePosition.position, Quaternion.identity);
                    Instantiate(bulletLeftUp, firePosition.position, Quaternion.identity);
                    Instantiate(bulletLeftDown, firePosition.position, Quaternion.identity);
                }
            }
        }
        if(shootingType == 2)
        {
            if (facingRight)
            {
                Instantiate(bulletF, firePositionF.position, Quaternion.identity);
            }
            else
            {
                Instantiate(bulletFLeft, firePositionF.position, Quaternion.identity);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "GROUND" || collision.gameObject.tag == "Water" ||
            collision.gameObject.tag == "NoFalling") ;
        {
            jump = false;
            animator.SetBool("Ground", true);
        }
        
        if (collision.gameObject.tag == "GROUND" || collision.gameObject.tag == "NoFalling")
        {
            readyToFall = true;
            onGround = true;
            onWater = false;
        }

        if (collision.gameObject.tag == "Water" && readyToFall || collision.gameObject.tag == "NoFalling")
        {
            readyToFall = false;
        }

        if (collision.gameObject.tag == "Water")
        {
            onWater = true;
            onGround = false;
        }

        if((collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "DeathLayer" ||
            collision.gameObject.tag == "EnemiesBullet") && isVisible)
        {
            GetComponent<AudioSource>().Play();
            animator.SetBool("Dead", true);
            boxCollider.isTrigger = true;
            circleCollider.isTrigger = true;
            rigidbody2d.simulated = false;
            isVisible = false;
            isDead = true;
            health--;
        
            if(health == 0)
            {

                Invoke("GameOver", 2f);
                

            }
            if(health !=0)
            {
                Invoke("RestartOne", 1f);
                Invoke("Restart", 2f);
                Invoke("MakeVisible", 5f);
                
            }
        }
        if(collision.gameObject.tag == "PowerUpB")
        {
            shootingType = 0;
        }
        if (collision.gameObject.tag == "PowerUpF")
        {
            shootingType = 1;
        }
        if (collision.gameObject.tag == "PowerUpM")
        {
            shootingType = 2;
        }


    }
  
    
    private void Flip()
    {
        if (speed > 0 && !facingRight || speed < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            
        }
    }
    private void Restart()
    {
        animator.SetBool("Dead", false);
        boxCollider.isTrigger = false;
        circleCollider.isTrigger = false;
        rigidbody2d.simulated = true;

    }
    private void RestartOne()
    {
        if (horizontalLevel)
        {
            transform.position = new Vector3(transform.position.x - resetX, -0.8f, 0);
        }
        else
        {
             transform.position = new Vector3(transform.position.x - resetX, transform.position.y + resetY, 0);
        }
        isDead = false;
        animator.SetBool("Dead", false);
        
    }
    private void MakeVisible()
    {
        isVisible = true;
    }
    private void GameOver()
    {
        SceneManager.LoadScene(3);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Finish")
        {
            GetComponent<PlayerManager>().enabled = false;
        }
    }
}
   


