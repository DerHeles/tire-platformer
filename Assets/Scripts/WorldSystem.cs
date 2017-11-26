using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSystem : MonoBehaviour
{
    public enum WorldSwitch
    {
        None, Evil, Friendly
    }

    private WorldSwitch m_queuedSwitch;

    [SerializeField] private Transform worldEvil;
    [SerializeField] private Transform worldFriendly;

    private Vector3 m_worldOffset;
    private bool m_inEvilWorld = false;

    [SerializeField] private Rigidbody2D player;
    [SerializeField] private CameraFollow camera;

    private bool m_switchQueued = false;
    

    private AudioManager m_audioManager;

    // Use this for initialization
    void Start ()
    {
        //m_worldOffset = worldEvil.position - worldFriendly.position;
        m_worldOffset = worldFriendly.position - worldEvil.position;

        m_audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void Update()
    {
        if (m_queuedSwitch == WorldSwitch.Evil)
        {
            EnterEvilWorld();
            m_queuedSwitch = WorldSwitch.None;
        }
        else if (m_queuedSwitch == WorldSwitch.Friendly)
        {
            EnterFriendlyWorld();
            m_queuedSwitch = WorldSwitch.None;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (m_inEvilWorld)
            {
                EnterFriendlyWorld();
            }
            else
            {
                EnterEvilWorld();
            }
        }
    }

    public void EnterFriendlyWorld()
    {
        //player.position += new Vector2(m_worldOffset.x, m_worldOffset.y);
        player.transform.position += m_worldOffset;
        m_inEvilWorld = false;
        //camera.WorldSwitchCameraReset();
        camera.transform.position += m_worldOffset;
        player.GetComponent<Animator>().SetBool("evil", false);

        m_audioManager.PlaySound(AudioManager.SoundID.PickupPatch);
        m_audioManager.PlayMusic(AudioManager.MusicID.Friendly);

    }

    public void EnterEvilWorld()
    {
        //player.position -= new Vector2(m_worldOffset.x, m_worldOffset.y);
        player.transform.position -= m_worldOffset;
        m_inEvilWorld = true;
        //camera.WorldSwitchCameraReset();
        camera.transform.position -= m_worldOffset;
        player.GetComponent<Animator>().SetBool("evil", true);

        m_audioManager.PlaySound(AudioManager.SoundID.LosePatch);
        m_audioManager.PlayMusic(AudioManager.MusicID.Evil);
    }

    // Warum gequeued? Wegen physiks sprung?
    public void QueueSwitch(WorldSwitch worldSwitch)
    {
        m_queuedSwitch = worldSwitch;
    }
}
