using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{

    private Animator animator;
    private Rigidbody2D rigidbody2d;
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
    private float fireRate = 10f; //số lần bắn trong 1 giây
    float timeToFire = 0;
    public bool facingRight;
    private bool isDead = false;
    public int health;
    private bool readyToFall;
    private bool onWater;
    private bool onGround;
    private bool jump = true;
    public float gravity;
    private bool isVisible; //biến để xác định sự "bất tử" của player
    private Vector3 previousPosition;


    // Use this for initialization - Khởi tạo các thuộc tính, giá trị
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        //Khởi tạo đối tượng vị trí cho các viên đạn
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
    void Update()
    {
        // Nếu đang ở trạng thái bình thường (không chết)
        if (!isDead)
        {
            Move(speed);
            Flip();
        }

        // Bắt sự kiện phím A
        if (Input.GetKey(KeyCode.A))
        {
            //Đặt speed < 0 (để xác định hướng quay mặt sang trái của nhân vật)
            speed = -runningSpeed;
        }
        //Bắt sự kiện phím D
        else if (Input.GetKey(KeyCode.D))
        {
            //Đặt speed > 0 (để xác định hướng quay mặt sang phải của nhân vật)
            speed = runningSpeed;
        }
        //Các trường hợp còn lại (không phải phím A và D) thì speed = 0 (nhân vật đứng yên)
        else
        {
            speed = 0;
        }

        //Bắt sự kiện phím Space để nhảy
        if (Input.GetKeyDown(KeyCode.Space) && !jump && !Input.GetKey(KeyCode.S) && !isDead
            && rigidbody2d.velocity.y == 0)
        {

            jump = true;
            rigidbody2d.AddForce(new Vector2(rigidbody2d.velocity.x, jumpSpeed));
            //Chuyển animation sang Jump
            animator.SetBool("Ground", false);
        }

        //Bắt sự kiện phím Space + S để tụt xuống
        if (readyToFall && Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.S) && !jump)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - gravity);
            jump = true;
            animator.SetBool("Ground", false);
        }

        //Bắt sự kiện phím W (chĩa súng lên trời)
        if (Input.GetKey("w"))
        {
            animator.SetBool("Up", true);
        }
        //Không phải phím W thì chĩa súng về bình thường
        else
            animator.SetBool("Up", false);

        //Bắt sự kiện phím W (chĩa súng xuống dưới)
        if (Input.GetKey("s"))
        {
            animator.SetBool("Down", true);
        }
        //Không phải phím W thì chĩa súng về bình thường
        else
            animator.SetBool("Down", false);

        //Bắt sự kiện phím Z thì nằm xuống
        if (Input.GetKey("z") && !onWater && onGround)
        {
            animator.SetBool("Crouch", true);
            speed = 0;

        }

        else
            animator.SetBool("Crouch", false);

        //Đặt tốc độ cho animator
        animator.SetFloat("Speed", Mathf.Abs(rigidbody2d.velocity.x));

        //Bắt sự kiện phím K 
        if (Input.GetKey("k") && Time.time > timeToFire && !isDead)
        {
            //Time.time > timeToFire để đảm bảo không bị bắn quá nhanh
            timeToFire = Time.time + 1 / fireRate; //timeToFire = thời gian hiện tại (tính bằng giây) + thời gian bắn 1 viên đạn (1/timeToFire) 
            Fire();
        }

        if (Input.GetKey("k"))
        {
            animator.SetBool("Shooting", true);
            isVisible = true;
        }
        else
            animator.SetBool("Shooting", false);

        //Chuyển animation khi đi trên sông 
        if (onWater)
        {
            animator.SetBool("Water", true);
        }
        else
            animator.SetBool("Water", false);
    }

    // Hàm di chuyển sử dụng Velocity (Vector3) (truyền vào speed)
    private void Move(float speed)
    {
        rigidbody2d.velocity = new Vector3(speed, rigidbody2d.velocity.y, 0);
    }

    //Hàm quay hướng nhân vật theo hướng chạy
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

    //Hàm tạo đạn khi bắn súng
    private void Fire()
    {
        //Bắn đạn đơn (1 viên)
        if (shootingType == 0)
        {
            bool anyKey = false;

            //Bắn đạn đơn theo hướng trên và dưới
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
            //Bắn đạn đơn theo 3 hướng khi quay mặt sang phải
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
            //Bắn đạn đơn theo 3 hướng khi quay mặt sang trái
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

        //Bắn đạn loại F (3 viên)
        //Tương tự bắn đạn đơn, nhưng cùng lúc tạo 3 viên đạn ở 3 vị trí
        if (shootingType == 1)
        {
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

        //Bắn đạn xoay vòng
        if (shootingType == 2)
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

    //Hàm xử lí va chạm
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

        if ((collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "DeathLayer" ||
            collision.gameObject.tag == "EnemiesBullet") && isVisible)
        {
            GetComponent<AudioSource>().Play();
            animator.SetBool("Dead", true);
            boxCollider.isTrigger = true;
            circleCollider.isTrigger = true;
            rigidbody2d.simulated = false;

            //Trạng thái không thể bị chết (isVisible = false)
            isVisible = false;

            isDead = true;
            health++;
            previousPosition = transform.position;

            //Nếu hết mạng thì GameOver
            if (health == 0)
            {

                Invoke("GameOver", 2f);


            }
            //Nếu hi sinh mà vẫn còn mạng thì restart
            if (health !=0)
            {
                Invoke("RestartOne", 0.5f);
                Invoke("Restart", 1f);
                Invoke("MakeVisible", 5f);
            }
        }

        //Nhặt đạn
        if (collision.gameObject.tag == "PowerUpB")
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

    void Restart()
    {
        animator.SetBool("Dead", false);
        boxCollider.isTrigger = false;
        circleCollider.isTrigger = false;
        rigidbody2d.simulated = true;
    }

    private void RestartOne()
    {
        transform.position = previousPosition;
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

    //Đi vào đường hầm qua màn
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Finish")
        {
            GetComponent<PlayerManager>().enabled = false;
        }
    }
}