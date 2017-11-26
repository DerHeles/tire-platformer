using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf : MonoBehaviour
{

    public Rigidbody2D arm;

    private bool triggered = false;
    private AudioManager m_audioManager;

    // Use this for initialization
    void Start ()
    {
        m_audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

    }
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.O))
	    {
	        arm.isKinematic = false;
	        arm.AddForce(Vector2.left * 6f, ForceMode2D.Impulse);
	        //vase.AddForce(Vector2.left * 2f, ForceMode2D.Impulse);
	        //vase.AddForceAtPosition(Vector2.left * 2f, vase.position + new Vector2(0.1f, 0.8f), ForceMode2D.Impulse);
	        Debug.Log("DROP");
	        triggered = true;
        }
	}

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (triggered)
            return;

        if (other.gameObject.CompareTag("Player"))
        {
            // Drop arm
            arm.isKinematic = false;
            arm.AddForce(Vector2.left * 4f, ForceMode2D.Impulse);
            //vase.AddForce(Vector2.left * 2f, ForceMode2D.Impulse);
            //vase.AddForceAtPosition(Vector2.left * 2f, vase.position + new Vector2(0.1f, 0.3f), ForceMode2D.Impulse);
            Debug.Log("DROP");
            triggered = true;

            m_audioManager.PlaySound(AudioManager.SoundID.SkeletonArm);
        }
    }
}