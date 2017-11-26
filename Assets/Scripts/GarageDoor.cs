using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageDoor : MonoBehaviour
{
    private GameObject parent;
    public BrokenGlass glass;

    private bool showThoughtBubble = true;
    private AudioManager m_audioManager;

    // Use this for initialization
    void Start ()
	{
	    parent = transform.parent.gameObject;
	    m_audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
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
                    m_audioManager.PlaySound(AudioManager.SoundID.DoorOpened);
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
                        m_audioManager.PlaySound(AudioManager.SoundID.DoorRammed);
                    }
                    if (showThoughtBubble)
                    {
                        showThoughtBubble = false;
                        player.ShowKeyBubble();
                    }
                }
            }
        }
    }
}
