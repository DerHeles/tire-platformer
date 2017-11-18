using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageDoor : MonoBehaviour
{
    private GameObject parent;

	// Use this for initialization
	void Start ()
	{
	    parent = transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponent<PlayerController>();
            if (player && player.HasKey())
            {
                player.UseKey();
                // Open Door
                //gameObject.SetActive(false);
                parent.SetActive(false);
            }
        }
    }
}
