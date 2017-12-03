using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    [SerializeField] private Plant plant;

    private bool triggered;
    private Animator animator;
    private AudioManager audioManager;

    private void Start ()
    {
        animator = GetComponent<Animator>();
	    animator.speed = 0.0f;
	    audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void Update () {
        if (triggered)
	        return;

	    if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.98f)
	    {
	        triggered = true;
	        plant.Grow();
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered)
            return;
        if (other.gameObject.CompareTag("Player"))
        {
            animator.speed = 0.6f;
            audioManager.PlaySound(AudioManager.SoundID.Bottle);
        }
    }
}
