using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawn : MonoBehaviour
{
    public float timeWindow = 4.0f;
    private float timeLeft;

    public int minTimesTriggered = 3;

    private int currentTimesTriggered;

    private bool currentlyChecking;
    private bool triggered;

    //private bool

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (triggered)
	        return;
	    if (currentlyChecking)
	    {
	        timeLeft -= Time.deltaTime;
	        if (timeLeft <= 0.0f)
	        {
	            currentlyChecking = false;
	            currentTimesTriggered = 0;
	        }
	    }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered)
            return;

        if (other.gameObject.CompareTag("TV"))
        {
            // TO DO   

            if (currentlyChecking)
            {
                currentTimesTriggered++;
                if (currentTimesTriggered >= minTimesTriggered)
                {
                    // SPAWN GHOST
                    Debug.Log("GHOST");
                    triggered = true;
                }
            }
            else
            {
                currentlyChecking = true;
                currentTimesTriggered = 1;
                timeLeft = timeWindow;
            }
        }
    }
}
