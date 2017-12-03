using UnityEngine;

public class WorldSystem : MonoBehaviour
{
    public enum WorldSwitch
    {
        None, Evil, Friendly
    }

    private WorldSwitch queuedSwitch;

    [SerializeField] private Transform wordEvil;
    [SerializeField] private Transform worldFriendly;

    private Vector3 worldOffset;
    private bool inEvilWorld;

    [SerializeField] private Rigidbody2D player;
    [SerializeField] private CameraFollow mainCamera;

    private AudioManager audioManager;

    private void Start ()
    {
        worldOffset = worldFriendly.position - wordEvil.position;

        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void Update()
    {
        if (queuedSwitch == WorldSwitch.Evil)
        {
            EnterEvilWorld();
            queuedSwitch = WorldSwitch.None;
        }
        else if (queuedSwitch == WorldSwitch.Friendly)
        {
            EnterFriendlyWorld();
            queuedSwitch = WorldSwitch.None;
        }

        #if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (inEvilWorld)
                EnterFriendlyWorld();
            else
                EnterEvilWorld();
        }
        #endif
    }

    public void EnterFriendlyWorld()
    {
        player.transform.position += worldOffset;
        mainCamera.transform.position += worldOffset;

        inEvilWorld = false;
        player.GetComponent<Animator>().SetBool("evil", false);

        audioManager.PlaySound(AudioManager.SoundID.PickupPatch);
        audioManager.PlayMusic(AudioManager.MusicID.Friendly);

    }

    public void EnterEvilWorld()
    {
        player.transform.position -= worldOffset;
        mainCamera.transform.position -= worldOffset;

        inEvilWorld = true;
        player.GetComponent<Animator>().SetBool("evil", true);

        audioManager.PlaySound(AudioManager.SoundID.LosePatch);
        // Don't play evil music when dead (instead the end music will be played)
        if (!player.GetComponent<PlayerController>().GameFinished())
            audioManager.PlayMusic(AudioManager.MusicID.Evil);
    }

    // Why queued? 'cause of physical jump?
    public void QueueSwitch(WorldSwitch worldSwitch)
    {
        queuedSwitch = worldSwitch;
    }
}
