using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyBridge : MonoBehaviour {

    private BoxCollider2D boxCollider;
    private Animator animator;
    public GameObject part;
    public GameObject part1;
    public GameObject part2;
    public GameObject part3;
    public float firstTime;
    public float secondTime;
    public float thirdTime;
    public float fourthTime;
    private bool reachedFirst;
    private bool reachedSecond;
    private bool reachedThird;
    private bool reachedFourth;



    // Use this for initialization
    void Start () {
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        animator = gameObject.GetComponent<Animator>();
        reachedFirst = false;
}
	
	// Update is called once per frame
	void Update () {
        if (reachedFirst)
        {
            StartCoroutine(WaitAndInvoke(firstTime, destroyStart));
            StartCoroutine(WaitAndInvoke(secondTime, destroyFirst));
            StartCoroutine(WaitAndInvoke(thirdTime, destroySecond));
            StartCoroutine(WaitAndInvoke(fourthTime, destroyThird));
            reachedFirst = false;
        }
	}
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            reachedFirst = true;


        }
    }
    private void destroyStart()
    {

        part.GetComponent<DestroyParts>().destroy();
    }
    private void destroyFirst()
    {
        
        part1.GetComponent<DestroyParts>().destroy();
    }
    private void destroySecond()
    {
        part2.GetComponent<DestroyParts>().destroy();
    }
    private void destroyThird()
    {
        part3.GetComponent<DestroyParts>().destroy();
        
    }
    delegate void InvokedFunction();
    IEnumerator WaitAndInvoke(float secondsToWait, InvokedFunction func)
    {
        yield return new WaitForSeconds(secondsToWait);
        func();
    }
}

