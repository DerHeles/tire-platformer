using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuPrefab;

    // Use this for initialization
    void Start()
    {
        if (GameObject.Find("Menu") == null)
        {
            var am = Instantiate(menuPrefab);
            am.name = "Menu";
        }
    }
}
