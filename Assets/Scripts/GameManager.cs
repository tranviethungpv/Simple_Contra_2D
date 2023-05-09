using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool gameEnd = false;
    private Transform player;
    private AudioSource audioSource;

    public Button soundButton;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;

    private void Start()
    {
        // Find the player object and get its transform component
        GameObject myObject = GameObject.Find("Player");
        player = myObject.transform;

        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();

        // Set up the sound button
        if (soundButton != null)
        {
            soundButton.onClick.AddListener(ToggleSound);
            UpdateSoundButton();
        }

        // Start playing the background music
        audioSource.Play();
    }

    public void GameOver()
    {
        if (!gameEnd)
        {
            gameEnd = true;
        }
    }

    public void Complete()
    {
        SceneManager.LoadScene(2);
    }

    private void ToggleSound()
    {
        // Toggle the sound on/off state
        bool soundOn = !audioSource.isPlaying;

        // Toggle the background music playback
        if (soundOn)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }

        // Update the sound button sprite to reflect the current sound on/off state
        UpdateSoundButton();
    }

    private void UpdateSoundButton()
    {
        // Update the sound button sprite to reflect the current sound on/off state
        if (soundButton != null)
        {
            bool soundOn = audioSource.isPlaying;
            soundButton.image.sprite = soundOn ? soundOnSprite : soundOffSprite;
        }
    }
}
