using UnityEngine;

public class GarageDoor : MonoBehaviour
{
    [SerializeField] private BrokenGlass glass;
    [SerializeField] private bool showBubble = true;

    private GameObject parent;
    private AudioManager audioManager;
    
    private void Start ()
	{
	    parent = transform.parent.gameObject;
	    audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponent<PlayerController>();
            if (player)
            {
                if (player.HasKey())
                {
                    player.UseKey();
                    audioManager.PlaySound(AudioManager.SoundID.DoorOpened);
                    parent.SetActive(false);
                }
                else
                {
                    if (Mathf.Abs(player.GetComponent<Rigidbody2D>().velocity.x) > 5.0f)
                    {
                        glass.Break();
                        audioManager.PlaySound(AudioManager.SoundID.DoorRammed);
                    }
                    if (showBubble)
                    {
                        showBubble = false;
                        player.ShowKeyBubble();
                    }
                }
            }
        }
    }
}
