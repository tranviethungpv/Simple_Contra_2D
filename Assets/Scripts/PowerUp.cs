using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

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

    // Hàm Start được gọi khi script được kích hoạt
    void Start()
    {
        animator = GetComponent<Animator>();
        GameObject myObject = GameObject.Find("Player");
        target = myObject.transform;
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        // Khởi tạo rigidBody của power-up và đóng băng nó
        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        // Nếu power-up không di chuyển, đặt box collider của nó là trigger để bắt đầu va chạm
        if (!isMoving)
        {
            boxCollider.isTrigger = true;
        }
    }

    // Hàm Update được gọi mỗi khung hình
    void Update()
    {
        // Tính toán khoảng cách giữa power-up và player
        distance = Mathf.Abs(transform.position.x - target.position.x);

        if (distance <= 10f)
        {
            // Nếu khoảng cách nhỏ hơn 10 thì bật cờ startMoving để bắt đầu di chuyển power-up
            startMoving = true;
        }
        if (distance <=10f && !isMoving)
        {
            // Nếu khoảng cách nhỏ hơn 10 và power-up không di chuyển, bật animation tương ứng với loại power-up
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
            // Đặt box collider của power-up thành không phải trigger để bắt đầu va chạm
            boxCollider.isTrigger = false;
        }
        if (isSpinning && startMoving && isMoving)
        {
            // Nếu power-up đang xoay và đã bắt đầu di chuyển, thì bỏ đóng băng rigidBody và di chuyển power-up
            rigidBody.constraints = RigidbodyConstraints2D.None; //Bỏ đóng băng
            rigidBody.angularVelocity = spinningSpeed; //thiết lập tốc độ xoay
            rigidBody.velocity = speed; //thiết lập tốc độ di chuyển
            rigidBody.AddRelativeForce(Vector2.up * speedx, ForceMode2D.Impulse);  //tác động lực bằng phương thức AddRelativeForce để power-up bay đi
        }
    }

    // Hàm được gọi khi power-up va chạm với đối tượng khác
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            // Nếu power-up va chạm với đạn, tạo một power-up mới tương ứng với loại power-up
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
            // Hủy power-up hiện tại
            Destroy(gameObject, 0);
        }
    }
}