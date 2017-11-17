using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarmfulObstacle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<PlayerController>();
            player.ReceiveDamage();

            Debug.Log("SCHADEN");
            Destroy(gameObject);
        }
    }
}
