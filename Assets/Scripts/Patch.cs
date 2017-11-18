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
            var player = other.GetComponent<PlayerController>();
            if (player)
            {
                player.PickupPatch();
            }
            world.QueueSwitch(WorldSystem.WorldSwitch.Friendly);
            //m_world.EnterFriendlyWorld();
            Destroy(gameObject);
        }
    }
}
