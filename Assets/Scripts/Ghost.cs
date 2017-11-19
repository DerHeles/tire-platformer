using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    //GetComponent<Rigidbody2D>().velocity = new Vector2(1, 1) * 1.0f;
	    //Destroy(gameObject, 4.0f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Fly()
    {
        gameObject.SetActive(true);
        GetComponent<Rigidbody2D>().velocity = new Vector2(1.0f, 1.7f) * 3.0f;
        Destroy(gameObject, 4.0f);
    }
}
