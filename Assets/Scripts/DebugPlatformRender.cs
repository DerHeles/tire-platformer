using UnityEngine;

public class DebugPlatformRender : MonoBehaviour {

    private void Start ()
	{
	    var sprite = GetComponent<SpriteRenderer>();
	    if (sprite)
	    {
	        sprite.enabled = false;
	    }
	}
}
