using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenGlass : MonoBehaviour {

    [SerializeField] private WorldSystem m_world;

    private bool m_broken;
    public Animator anim;

    // Use this for initialization
    void Start()
    {
        //anim = GetComponent<Animator>();
        anim.speed = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.98f)
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
