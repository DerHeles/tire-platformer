using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawn : MonoBehaviour
{
    public float timeWindow = 4.0f;
    private float timeLeft;

    public int minTimesTriggered = 3;

    private int currentTimesTriggered;

    private bool currentlyChecking;
    private bool triggered;

    public Ghost ghost;
    public SpriteRenderer tv;
    public Sprite brokenTVSprite;
    public GameObject patch;

    public SpriteRenderer friendlyTV;
    public Sprite friendlyTVSprite;
    private AudioManager m_audioManager;

    //private bool

    // Use this for initialization
    void Start () {

        m_audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (triggered)
	        return;
	    if (currentlyChecking)
	    {
	        timeLeft -= Time.deltaTime;
	        if (timeLeft <= 0.0f)
	        {
	            currentlyChecking = false;
	            currentTimesTriggered = 0;
	        }
	    }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered)
            return;

        if (other.gameObject.CompareTag("TV"))
        {
            // TO DO   

            if (currentlyChecking)
            {
                currentTimesTriggered++;
                if (currentTimesTriggered >= minTimesTriggered)
                {
                    // SPAWN GHOST
                    Debug.Log("GHOST");
                    triggered = true;
                    ghost.Fly();
                    tv.sprite = brokenTVSprite;
                    friendlyTV.sprite = friendlyTVSprite;
                    patch.SetActive(true);

                    m_audioManager.PlaySound(AudioManager.SoundID.BrokenTV);
                    m_audioManager.PlaySound(AudioManager.SoundID.FireGhost);
                }
            }
            else
            {
                currentlyChecking = true;
                currentTimesTriggered = 1;
                timeLeft = timeWindow;
            }
        }
    }
}
