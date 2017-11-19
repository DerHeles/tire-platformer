using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    public Animator anim;
    public Plant plant;

    private bool triggered;

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
	    if (triggered)
	        return;

	    if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.98f)
	    {
	        Debug.Log("Plant Grow");
	        triggered = true;
	        plant.Grow();
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponent<PlayerController>();
            //player.ReceiveDamage();
            //anim.SetTrigger("TouchBottle");
            anim.speed = 0.4f;
            Debug.Log("BOTTLE");

        }
    }
}
