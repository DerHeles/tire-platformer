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
            keyImage.sprite = keySprite;
            if(otherKey)
                Destroy(otherKey.gameObject);
            Destroy(gameObject);
        }
    }
}
