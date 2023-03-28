using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreY : MonoBehaviour
{

    private Transform player;
    public Text score;
    public Text score2;
    private float highest;
    private float number = 0;
    private float highScore;

    // Use this for initialization
    void Start()
    {
        highest = -5000f;
        GameObject myObject = GameObject.Find("Player");
        player = myObject.transform;
        highScore = PlayerPrefs.GetFloat("HighScore");
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            number = PlayerPrefs.GetFloat("CurrentScore");
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (highest < player.position.y + 50.5f)
        {

            highest = (player.position.y+ 50.5f);
            number = number + highest;
            PlayerPrefs.SetFloat("CurrentScore", number);
            
            if (number > highScore)
            {
                highScore = number;
                PlayerPrefs.SetFloat("HighScore", highScore);
            }

        }


        score.text = number.ToString("0");
        score2.text = number.ToString("0");
    }
}
