using UnityEngine;

public class Ghost : MonoBehaviour {
    
    public void Fly()
    {
        gameObject.SetActive(true);
        GetComponent<Rigidbody2D>().velocity = new Vector2(1.0f, 1.7f) * 3.0f;
        Destroy(gameObject, 4.0f);
    }
}
