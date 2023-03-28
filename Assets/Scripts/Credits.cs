
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Credits : MonoBehaviour {
    private float score;
    private float currentScore;
    public Text highScore;
    public Text currentText;

    public void Start()
    {
       
        score = PlayerPrefs.GetFloat("HighScore");
        highScore.text = "High score: " + score.ToString("0");
       
        currentScore = PlayerPrefs.GetFloat("CurrentScore");
        currentText.text = "Your score:" + currentScore.ToString("0");

    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
    
   
}
