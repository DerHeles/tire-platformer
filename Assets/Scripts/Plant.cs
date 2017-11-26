using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    private bool m_grown;

    public Sprite grownSprite;
    public Animator anim;

    private BoxCollider2D collider;

    private AudioManager m_audioManager;

    // Use this for initialization
    void Start ()
	{
	    anim.speed = 0.0f;
	    collider = GetComponent<BoxCollider2D>();
	    m_audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Grow()
    {
        m_grown = true;
        //transform.localScale = new Vector3(1, 1, 1);
        anim.speed = 0.4f;
        collider.enabled = true;
        m_audioManager.PlaySound(AudioManager.SoundID.PlantGrown);
    }
}
