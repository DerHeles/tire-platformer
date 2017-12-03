using UnityEngine;

public class GhostSpawn : MonoBehaviour
{
    [SerializeField] private float timeWindow = 4.0f;
    private float timeLeft;

    [SerializeField] private int minTimesTriggered = 3;
    private int currentTimesTriggered;

    private bool currentlyChecking;
    private bool triggered;

    [SerializeField] private Ghost ghost;
    [SerializeField] private GameObject patch;
    [SerializeField] private SpriteRenderer tv;
    [SerializeField] private Sprite tvBrokenSprite;
    [SerializeField] private SpriteRenderer tvFriendly;
    [SerializeField] private Sprite tvFriendlySprite;

    private AudioManager audioManager;

    private void Start () {

        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }
	
    private void Update ()
	{
	    if (triggered)
	        return;

	    if (currentlyChecking)
	    {
	        timeLeft -= Time.deltaTime;
	        if (timeLeft <= 0.0f)
	        {
	            currentlyChecking = false;
	            currentTimesTriggered = 0;
	        }
	    }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered)
            return;

        if (other.gameObject.CompareTag("TV"))
        {
            if (currentlyChecking)
            {
                currentTimesTriggered++;
                if (currentTimesTriggered >= minTimesTriggered)
                {
                    triggered = true;
                    ghost.Fly();
                    patch.SetActive(true);

                    tv.sprite = tvBrokenSprite;
                    tvFriendly.sprite = tvFriendlySprite;

                    audioManager.PlaySound(AudioManager.SoundID.BrokenTV);
                    audioManager.PlaySound(AudioManager.SoundID.FireGhost);
                }
            }
            else
            {
                currentlyChecking = true;
                currentTimesTriggered = 1;
                timeLeft = timeWindow;
            }
        }
    }
}
