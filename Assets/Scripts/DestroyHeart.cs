using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyHeart : MonoBehaviour {

    public int health;
    public GameObject player;
    private int playerHealth;
    
	
	void Start () {
       
        int number = player.GetComponent<PlayerManager>().health;
     
	}
	
	// Update is called once per frame
	void Update () {
        playerHealth = player.GetComponent<PlayerManager>().health;
        DestroyImage();
        
    }
    public void DestroyImage()
    {
      if(health == playerHealth)
        {
            Destroy(gameObject, 0f);
        }
    }
}
