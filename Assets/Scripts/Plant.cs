using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private bool grown;
    private BoxCollider2D platformCollider;
    private AudioManager audioManager;
    
    private void Start ()
	{
	    anim.speed = 0.0f;
	    platformCollider = GetComponent<BoxCollider2D>();
	    audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void Grow()
    {
        if (!grown)
        {
            grown = true;
            anim.speed = 0.4f;
            platformCollider.enabled = true;
            audioManager.PlaySound(AudioManager.SoundID.PlantGrown);
        }
    }
}
