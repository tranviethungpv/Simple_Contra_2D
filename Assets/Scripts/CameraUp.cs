using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUp : MonoBehaviour {

    public Transform player;
    private float y;

    void Start () {
        y = -52f;
	}
	
	// Update is called once per frame
	void Update () {
        if (y < player.position.y)
        {
            y = player.position.y;
        }
        transform.position = new Vector3(transform.position.x, y+2, transform.position.z);

    }
}
