using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockControl : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rigidBody;   
    private Transform target;
    private float distanceY;             // Biến để lưu trữ khoảng cách theo trục Y giữa đối tượng và Player.
    private bool enough = false;         // Biến boolean để kiểm tra xem đối tượng đã đến gần đủ khoảng cách với Player chưa.
    public float offset;                 // Biến để lưu trữ giá trị vị trí lùi xuống của đối tượng khi va chạm với đất.

    // Use this for initialization
    void Start()
    {
        GameObject myObject = GameObject.Find("Player");
        target = myObject.transform;
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;   // Đóng băng tất cả các viên đá
    }

    // Update is called once per frame
    void Update()
    {
        distanceY =Mathf.Abs(transform.position.y - target.position.y);   // Tính khoảng cách theo trục Y giữa viên đá và Player.
        if (distanceY <= 7f)    // Kiểm tra nếu khoảng cách đủ gần với Player.
        {
            enough = true;     // Đặt enough thành true để đánh dấu là đối tượng đã đến gần đủ khoảng cách với Player.
            rigidBody.constraints = RigidbodyConstraints2D.None;   // Dừng đóng băng các viên đá
        }
    }

    // Hàm này được gọi khi đối tượng va chạm với đất.
    private void fallDown()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - offset);   // Đặt vị trí của đối tượng lùi xuống một khoảng offset.
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DeathLayer")   // Kiểm tra nếu đối tượng va chạm với "DeathLayer".
        {
            Destroy(gameObject, 0);   // Phá hủy đối tượng sau 0 giây.
        }
        if (collision.gameObject.tag== "GROUND" && enough)   // Kiểm tra nếu đối tượng va chạm với đất và đã đến gần đủ khoảng cách với Player.
        {
            Invoke("fallDown", 1f);   // Đặt hàm fallDown để được gọi sau 1 giây.
        }
    }
}