using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageDoor : MonoBehaviour
{
    private GameObject parent;
    public BrokenGlass glass;

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
            if (player)
            {
                if (player.HasKey())
                {
                    player.UseKey();
                    // Open Door
                    //gameObject.SetActive(false);
                    parent.SetActive(false);
                }
                else
                {
                    // NO KEY
                    //Debug.Log(player.GetComponent<Rigidbody2D>().velocity.x);
                    if (Mathf.Abs(player.GetComponent<Rigidbody2D>().velocity.x) > 5.0f)
                    {
                        glass.Break();
                    }
                }
            }
        }
    }
}
