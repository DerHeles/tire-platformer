using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{ 
    [SerializeField] private Key otherKey;
    [SerializeField] private Image keyImage;
    [SerializeField] private Sprite keySprite;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var player = other.GetComponent<PlayerController>();
            if (player)
            {
                player.PickupKey();
            }
            if(otherKey)
                Destroy(otherKey.gameObject);
            Destroy(gameObject);
        }
    }
}
