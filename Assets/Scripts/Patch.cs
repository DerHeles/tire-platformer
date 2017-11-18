using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patch : MonoBehaviour
{
    public WorldSystem world;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            world.QueueSwitch(WorldSystem.WorldSwitch.Friendly);
            //m_world.EnterFriendlyWorld();
            Destroy(gameObject);
        }
    }
}
