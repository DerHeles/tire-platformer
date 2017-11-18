using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patch : MonoBehaviour
{

    [SerializeField] private WorldSystem m_world;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            m_world.QueueSwitch(WorldSystem.WorldSwitch.Friendly);
            //m_world.EnterFriendlyWorld();
            Destroy(gameObject);
        }
    }
}
