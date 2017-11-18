using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondFloor : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D[] platforms;

    void ActivateSecondFloor()
    {
        foreach (var floor in platforms)
        {
            floor.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // DO NOTHING
    }
}
