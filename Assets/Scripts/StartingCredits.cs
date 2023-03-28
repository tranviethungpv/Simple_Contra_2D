
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StartingCredits : MonoBehaviour
{
    private float score;

    public Text highScore;
    public void Start()
    {
        
        score = PlayerPrefs.GetFloat("HighScore");
        highScore.text = "High score: " + score.ToString("0");

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
