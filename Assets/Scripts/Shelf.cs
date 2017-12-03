using UnityEngine;

public class Shelf : MonoBehaviour
{
    [SerializeField] private Rigidbody2D arm;

    private bool triggered;
    private AudioManager audioManager;
    
    private void Start ()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (triggered)
            return;

        if (other.gameObject.CompareTag("Player"))
        {
            // Drop arm
            arm.isKinematic = false;
            arm.AddForce(Vector2.left * 4f, ForceMode2D.Impulse);
            triggered = true;

            audioManager.PlaySound(AudioManager.SoundID.SkeletonArm);
        }
    }
}