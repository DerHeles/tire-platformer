using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform m_playerTransform;
    private Vector3 m_offset;

    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;

    private bool m_cameraSwitch = false;

    private float m_switchTimer;
    private bool m_switch = false;

    // Use this for initialization
    void Start()
    {
        m_offset = transform.position - m_playerTransform.position;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Smooth Follow
        Vector3 point = GetComponent<Camera>().WorldToViewportPoint(m_playerTransform.position);
        Vector3 delta = m_playerTransform.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
        Vector3 destination = transform.position + delta;
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);

        /*
        if (m_cameraSwitch)
        {
            if (m_switch)
            {
                Vector3 point = GetComponent<Camera>().WorldToViewportPoint(m_playerTransform.position);
                Vector3 delta = m_playerTransform.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
                Vector3 destination = transform.position + delta;
                transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            }
            m_switchTimer -= Time.deltaTime;
            if (m_switchTimer <= 0.0f)
            {
                //m_switchTimer = 0.0f;
                m_cameraSwitch = false;
                dampTime = 0.1f;
            }
            
        }
        else
        {
            // Simple follow
            //transform.position = m_playerTransform.position + m_offset;

            // Smooth Follow
            Vector3 point = GetComponent<Camera>().WorldToViewportPoint(m_playerTransform.position);
            Vector3 delta = m_playerTransform.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }
        */
    }

    public void WorldSwitchCameraReset()
    {
        m_cameraSwitch = true;
        transform.position = m_playerTransform.position + m_offset;
        m_switchTimer = 0.5f;
        dampTime = 0.0f;
        m_switch = true;
    }
}
