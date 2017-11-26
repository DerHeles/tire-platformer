using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarmfulObstacle : MonoBehaviour {

    [SerializeField] private WorldSystem m_world;

    public bool isPuddle;
    private AudioManager m_audioManager;

    // Use this for initialization
    void Start () {

        m_audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
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
            m_world.QueueSwitch(WorldSystem.WorldSwitch.Evil);

            Debug.Log("SCHADEN");
            if (isPuddle)
            {
                m_audioManager.PlaySound(AudioManager.SoundID.Puddle);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
