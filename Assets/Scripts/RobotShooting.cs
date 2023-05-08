using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotShooting : MonoBehaviour
{
    private GameObject player;
    private Transform playerTransf;
    // Biến để điều chỉnh góc xoay của robot (nếu cần)
    public int rotationOff;

    void Start()
    {
        player = GameObject.Find("Player");
        playerTransf = player.transform;
    }

    void Update()
    {
        // Tính toán vector từ robot đến player
        Vector3 difference = playerTransf.position - transform.position;
        // Chuẩn hóa vector để tính toán góc (chuẩn hoá độ dài vector về 1, hướng giữ nguyên)
        difference.Normalize();
        // Tính toán góc giữa vector từ robot đến player và trục x bằng hàm Atan2 và chuyển nó từ radian về độ
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        // Xoay robot theo góc tính được
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOff);
    }
}