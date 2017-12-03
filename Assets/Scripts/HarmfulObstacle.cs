using UnityEngine;

public class HarmfulObstacle : MonoBehaviour {

    [SerializeField] private WorldSystem world;
    [SerializeField] private bool isPuddle;

    private AudioManager audioManager;

    private void Start () {

        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<PlayerController>();
            player.ReceiveDamage();

            world.QueueSwitch(WorldSystem.WorldSwitch.Evil);
            
            if (isPuddle)
                audioManager.PlaySound(AudioManager.SoundID.Puddle);
            else
                Destroy(gameObject);
        }
    }
}
