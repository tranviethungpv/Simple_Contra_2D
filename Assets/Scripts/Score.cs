using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour {

    private Transform player;
    public Text score;
    public Text score2;
    private float highest = -500f;
    private float number = 0;
    private float highScore;
 
	// Use this for initialization
	void Start () {
        GameObject myObject = GameObject.Find("Player");
        player = myObject.transform;
        highScore = PlayerPrefs.GetFloat("HighScore");
        if(SceneManager.GetActiveScene().buildIndex != 1)
        {
            number = PlayerPrefs.GetFloat("CurrentScore");
        }
    }
	
	// Update is called once per frame
	void Update () {
     
        if(highest < player.position.x)
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
       

        score.text = number.ToString("0");
        score2.text = number.ToString("0");
	}
}
