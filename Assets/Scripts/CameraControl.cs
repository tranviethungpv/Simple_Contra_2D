using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public Transform player;
  
   
    private float x;
   
	
	void Start () {
		x = 11.630f;
        
	}
	
	// Update is called once per frame
	void Update () {
        if (x < player.position.x)
        {
            x = player.position.x;
        }
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
        
     
    }
}
