using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartingCredits : MonoBehaviour
{
    public Text highScoreText;
    public Button musicButton;
    public Text soundText;

    private AudioSource audioSource;
    private float highScore;
    private bool musicOn = true;

    private void Start()
    {
        // Get the high score from PlayerPrefs
        highScore = PlayerPrefs.GetFloat("HighScore");

        // Update the high score text
        highScoreText.text = "High Score: " + highScore.ToString("0");

        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();

        // Set up the music button
        if (musicButton != null)
        {
            musicButton.onClick.AddListener(ToggleMusic);
        }

        // Start playing the background music
        audioSource.Play();

        // Update the sound text to reflect the current music on/off state
        UpdateSoundText();
    }

    private void Update()
    {
        // Quit the application if the player presses the escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void ToggleMusic()
    {
        // Toggle the music on/off state
        musicOn = !musicOn;

        // Toggle the background music playback
        if (musicOn)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }

        // Update the sound text to reflect the current music on/off state
        UpdateSoundText();
    }

    private void UpdateSoundText()
    {
        // Update the sound text to indicate whether the sound is on or off
        if (soundText != null)
        {
            soundText.text = musicOn ? "Enabled" : "Disabled";
        }
    }

    public void RestartGame()
    {
        // Load the first scene (assuming it has build index 0)
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
    }
}
