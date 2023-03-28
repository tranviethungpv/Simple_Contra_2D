
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finishing : MonoBehaviour {

    public GameManager gameManager;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameManager.Complete();
            Invoke("NewLevel", 3f);
        }
    }
    private void NewLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
