using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParts : MonoBehaviour {
    private BoxCollider2D boxCollider;
    private Animator animator;
    public GameObject particles;
    bool destroyed = false;

    // Use this for initialization
    void Start () {
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        animator = gameObject.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void destroy()
    {
        if (!destroyed)
        {
            destroyed = true;
            Instantiate(particles, transform.position, transform.rotation);
            animator.SetBool("Destroyed", true);
            boxCollider.isTrigger = true;
            GetComponent<AudioSource>().Play();
        }
    }
}
