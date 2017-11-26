using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenGlass : MonoBehaviour {

    [SerializeField] private WorldSystem m_world;

    private bool m_broken;
    private bool m_soundPlayed;
    public Animator anim;
    private AudioManager m_audioManager;

    // Use this for initialization
    void Start()
    {
        //anim = GetComponent<Animator>();
        anim.speed = 0.0f;
        m_audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_soundPlayed && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.82f)
        {
            m_audioManager.PlaySound(AudioManager.SoundID.BrokenGlass);
            m_soundPlayed = true;
        }
        if (!m_broken && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.98f)
        {
            Debug.Log("Glass actually broken");
            m_broken = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_broken)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                var player = collision.gameObject.GetComponent<PlayerController>();
                player.ReceiveDamage();
                m_world.QueueSwitch(WorldSystem.WorldSwitch.Evil);

                Debug.Log("GLASS");
                gameObject.SetActive(false);
                //Destroy(gameObject);
            }
        }
    }

    public void Break()
    {
        Debug.Log("BREAK");
        //m_broken = true;
        anim.speed = 0.5f;
    }
}
