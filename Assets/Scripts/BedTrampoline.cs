using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedTrampoline : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.gameObject.GetComponent<PlayerController>();
        if (player)
        {
            player.jumpForce = 480.0f;
            player.airMoveForce = 2.0f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var player = other.gameObject.GetComponent<PlayerController>();
        if (player)
        {
            player.jumpForce = 320.0f;
            player.airMoveForce = 5.0f;
        }
    }
}
