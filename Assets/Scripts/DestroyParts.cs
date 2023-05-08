using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParts : MonoBehaviour
{
    private BoxCollider2D boxCollider;    
    private Animator animator;           
    public GameObject particles;         
    bool destroyed = false;  // Biến boolean để kiểm tra xem đối tượng đã được phá hủy chưa.

    // Use this for initialization
    void Start()
    {
        boxCollider = gameObject.GetComponent<BoxCollider2D>();   
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Hàm này được gọi khi muốn phá hủy đối tượng và tạo ra hiệu ứng phá hủy.
    public void destroy()
    {
        if (!destroyed)     // Kiểm tra xem đối tượng đã được phá hủy chưa.
        {
            destroyed = true;   // Nếu chưa, đặt destroyed thành true để đánh dấu là đối tượng đã bị phá hủy.
            Instantiate(particles, transform.position, transform.rotation);   // Tạo ra một đối tượng particles tại vị trí và hướng của đối tượng được gắn kịch bản.
            animator.SetBool("Destroyed", true);   // Đặt thuộc tính Destroyed của Animator thành true, kích hoạt các trạng thái phá hủy được định nghĩa trong animator controller.
            boxCollider.isTrigger = true;   // loại bỏ va chạm vật lý với đối tượng.
            GetComponent<AudioSource>().Play();
        }
    }
}