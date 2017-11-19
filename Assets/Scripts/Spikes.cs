using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

public class Spikes : MonoBehaviour
{
    [SerializeField] private Sprite m_spikesTriggeredSprite;

    private bool triggered = false;

    [SerializeField] private GameObject patchPrefab;
    [SerializeField] private Transform spawnLocation;
    [SerializeField] private WorldSystem m_world;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered)
            return;

        if (other.gameObject.CompareTag("Player"))
        {
            var player = other.GetComponent<PlayerController>();
            if (player)
            {
                //player.ReceiveDamage();
                triggered = true;

                var sprite = GetComponent<SpriteRenderer>();
                if (sprite)
                {
                    sprite.sprite = m_spikesTriggeredSprite;
                    GameObject patch = Instantiate(patchPrefab, spawnLocation.position, Quaternion.identity);
                    patch.GetComponent<Patch>().world = m_world;
                }
            }
        }
    }


    private void OnDrawGizmos()
    {
        //Gizmos.DrawIcon(transform.position, "Light Gizmo.tiff", true);
    }
}
