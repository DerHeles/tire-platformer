using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuPrefab;
    [SerializeField] private GameObject audioManagerPrefab;

    // Use this for initialization
    void Start()
    {
        if (GameObject.Find("AudioManager") == null)
        {
            var am = Instantiate(audioManagerPrefab);
            am.name = "AudioManager";
        }
        if (GameObject.Find("Menu") == null)
        {
            var menu = Instantiate(menuPrefab);
            menu.name = "Menu";
        }
    }
}
