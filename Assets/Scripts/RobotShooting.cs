using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotShooting : MonoBehaviour {
    private GameObject player;
    private Transform playerTransf;
    public int rotationOff;
	// Use this for initialization
	void Start () {
        GameObject player = GameObject.Find("Player");
        playerTransf = player.transform;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 difference = playerTransf.position - transform.position;
        difference.Normalize();

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, rotZ+ rotationOff);
		
	}
}
