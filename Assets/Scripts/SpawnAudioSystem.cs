using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAudioSystem : MonoBehaviour
{
    [SerializeField] private GameObject audioManagerPrefab;

    // Use this for initialization
    void Start()
    {
        if (GameObject.Find("AudioManager") == null)
        {
            var am = Instantiate(audioManagerPrefab);
            am.name = "AudioManager";
        }
    }
}
