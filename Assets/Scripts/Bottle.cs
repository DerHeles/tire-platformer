using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    public Animator anim;
    public Plant plant;

    private bool triggered;
    private AudioManager m_audioManager;

    // Use this for initialization
    void Start ()
	{
	    anim.speed = 0.0f;
	    m_audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
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
        if (triggered)
            return;
        if (other.gameObject.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponent<PlayerController>();
            //player.ReceiveDamage();
            //anim.SetTrigger("TouchBottle");
            anim.speed = 0.6f;
            Debug.Log("BOTTLE");
            m_audioManager.PlaySound(AudioManager.SoundID.Bottle);
        }
    }
}
