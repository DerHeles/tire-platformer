using UnityEngine;

public class BrokenGlass : MonoBehaviour {

    [SerializeField] private WorldSystem world;

    private bool broken;
    private bool soundPlayed;
    private Animator animator;
    private AudioManager audioManager;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed = 0.0f;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void Update()
    {
        if (!soundPlayed && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.82f)
        {
            audioManager.PlaySound(AudioManager.SoundID.BrokenGlass);
            soundPlayed = true;
        }
        if (!broken && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.98f)
        {
            broken = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (broken)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                var player = collision.gameObject.GetComponent<PlayerController>();
                player.ReceiveDamage();
                world.QueueSwitch(WorldSystem.WorldSwitch.Evil);
                
                gameObject.SetActive(false);
            }
        }
    }

    public void Break()
    {
        animator.speed = 0.5f;
    }
}
