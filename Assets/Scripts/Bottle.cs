using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    public Animator anim;

	// Use this for initialization
	void Start ()
	{
	    anim.speed = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.F))
	    {
            anim.SetTrigger("TouchBottle");
	        anim.speed = 1.0f;
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponent<PlayerController>();
            //player.ReceiveDamage();

            Debug.Log("BOTTLE");
        }
    }
}
