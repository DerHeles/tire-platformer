using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public GameObject patchPrefab;
    public Transform spawnLocation;
    public WorldSystem world;
    public GameObject arm;
    private AudioManager m_audioManager;

    // Use this for initialization
    void Start ()
    {
        m_audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Skeleton"))
        {
            arm.SetActive(true);
            other.gameObject.SetActive(false);
            //other.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            //other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            //other.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0.0f;
            //other.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 134.0f));
            //other.gameObject.transform.parent = armSocket;
            //other.gameObject.transform.localPosition = new Vector3(-0.4089f, 0.311f, 0f);
            //other.gameObject.transform.localPosition = Vector3.zero;
            GameObject patch = Instantiate(patchPrefab, spawnLocation.position, Quaternion.identity);
            patch.GetComponent<Patch>().world = world;
            m_audioManager.PlaySound(AudioManager.SoundID.OpenShelf);
            Debug.Log("Arm");
        }
    }
}
