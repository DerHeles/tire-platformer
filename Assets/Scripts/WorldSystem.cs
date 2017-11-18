using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSystem : MonoBehaviour
{

    [SerializeField] private Transform worldEvil;
    [SerializeField] private Transform worldFriendly;

    private Vector3 m_worldOffset;
    private bool m_inEvilWorld = false;

    [SerializeField] private Rigidbody2D player;
    [SerializeField] private CameraFollow camera;

    // Use this for initialization
    void Start ()
    {
        m_worldOffset = worldEvil.position - worldFriendly.position;
    }

    private void Update()
    {
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
        camera.WorldSwitchCameraReset();
    }

    public void EnterEvilWorld()
    {
        //player.position -= new Vector2(m_worldOffset.x, m_worldOffset.y);
        player.transform.position -= m_worldOffset;
        m_inEvilWorld = true;
        camera.WorldSwitchCameraReset();
    }
}
