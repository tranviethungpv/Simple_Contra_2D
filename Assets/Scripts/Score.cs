using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{

    private Transform player;       // Transform của Player
    public Text score;              // Text để hiển thị điểm số
    private float highest = -500f;  // Vị trí xa nhất mà Player đã đi được
    private float number = 0;       // Điểm số hiện tại của người chơi
    private float highScore;        // Điểm số cao nhất

    // Hàm được gọi khi script được kích hoạt
    void Start()
    {
        // Tìm đối tượng Player bằng tên "Player" và lưu trữ transform của nó vào biến player
        GameObject myObject = GameObject.Find("Player");
        player = myObject.transform;

        // Lấy điểm số cao nhất được lưu trữ trong PlayerPrefs vào biến highScore
        highScore = PlayerPrefs.GetFloat("HighScore");

        // Nếu không ở trong cảnh chơi mới, lấy điểm số hiện tại của người chơi từ PlayerPrefs vào biến number
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            number = PlayerPrefs.GetFloat("CurrentScore");
        }
    }

    // Hàm được gọi mỗi khung hình
    void Update()
    {
        // Nếu vị trí hiện tại của Player xa hơn vị trí xa nhất mà Player đã đi được (highest),
        // cập nhật highest thành vị trí hiện tại và tăng number lên giá trị của highest
        if (highest < player.position.x)
        {
            highest = player.position.x;
            number = number + highest;
            PlayerPrefs.SetFloat("CurrentScore", number);
            PlayerPrefs.SetFloat("HighScore", highScore);
            if (number > highScore)
            {
                highScore = number;
            }
        }

        // Hiển thị điểm số hiện tại của người chơi trên Text score
        score.text = number.ToString("0");
    }
}