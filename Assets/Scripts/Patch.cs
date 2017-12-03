using UnityEngine;

public class Patch : MonoBehaviour
{
    public WorldSystem World
    {
        get { return world; }
        set { world = value; }
    }
    [SerializeField] private WorldSystem world;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var player = other.GetComponent<PlayerController>();
            if (player)
                player.PickupPatch();

            world.QueueSwitch(WorldSystem.WorldSwitch.Friendly);
            Destroy(gameObject);
        }
    }
}
