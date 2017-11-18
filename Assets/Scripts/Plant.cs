using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    private bool m_grown;

    public Sprite grownSprite;
    public Animator anim;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Grow()
    {
        m_grown = true;
        transform.localScale = new Vector3(1, 1, 1);
    }
}
