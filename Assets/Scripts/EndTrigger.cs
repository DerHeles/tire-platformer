using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    private bool triggered;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (triggered)
            return;

        if (other.collider.gameObject.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponent<PlayerController>();
            if (player)
            {
                player.FinishGame();
                triggered = true;
            }
        }
    }
}
