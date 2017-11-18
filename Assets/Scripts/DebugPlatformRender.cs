using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPlatformRender : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	    var sprite = GetComponent<SpriteRenderer>();
	    if (sprite)
	    {
	        sprite.enabled = false;
	    }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
