
using UnityEngine;

public class GameManager : MonoBehaviour {

    private bool gameEnd = false;
    private Transform player;
    public GameObject completeLevel;

    private void Start()
    {
        GameObject myObject = GameObject.Find("Player");
        player = myObject.transform;
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
        completeLevel.SetActive(true);
    }
}
