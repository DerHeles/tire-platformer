using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private GameObject patchPrefab;
    [SerializeField] private Transform spawnLocation;
    [SerializeField] private WorldSystem world;

    [SerializeField] private Sprite spikesTriggeredSprite;

    private bool triggered;

    private AudioManager audioManager;
    
	private void Start ()
    {
	    audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
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
                triggered = true;

                var sprite = GetComponent<SpriteRenderer>();
                if (sprite)
                {
                    sprite.sprite = spikesTriggeredSprite;
                    GameObject patch = Instantiate(patchPrefab, spawnLocation.position, Quaternion.identity);
                    patch.GetComponent<Patch>().World = world;
                }
                audioManager.PlaySound(AudioManager.SoundID.Spikes);
            }
        }
    }
}
