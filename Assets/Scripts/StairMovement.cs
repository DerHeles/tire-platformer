using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("COLLISION");
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<PlayerController>();
            player.moveForce = 30.0f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("COLLISION");
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<PlayerController>();
            player.moveForce = 10.0f;
        }
    }
}
