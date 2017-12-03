using UnityEngine;

public class Key : MonoBehaviour
{ 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var player = other.GetComponent<PlayerController>();
            if (player)
            {
                player.PickupKey();
            }
            Destroy(gameObject);
        }
    }
}
