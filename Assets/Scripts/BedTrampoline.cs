using UnityEngine;

public class BedTrampoline : MonoBehaviour {
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.gameObject.GetComponent<PlayerController>();
        if (player)
        {
            player.JumpForce = 480.0f;
            player.AirMoveForce = 2.0f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var player = other.gameObject.GetComponent<PlayerController>();
        if (player)
        {
            player.JumpForce = 320.0f;
            player.AirMoveForce = 5.0f;
        }
    }
}
