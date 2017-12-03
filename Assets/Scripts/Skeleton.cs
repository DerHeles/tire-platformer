using UnityEngine;

public class Skeleton : MonoBehaviour
{
    [SerializeField] private GameObject patchPrefab;
    [SerializeField] private Transform spawnLocation;
    [SerializeField] private WorldSystem world;
    [SerializeField] private GameObject arm;

    private AudioManager audioManager;

    private void Start ()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Colliding with skeleton arm when player has pushed it into the trigger
        if (other.gameObject.CompareTag("Skeleton"))
        {
            arm.SetActive(true);
            other.gameObject.SetActive(false);

            GameObject patch = Instantiate(patchPrefab, spawnLocation.position, Quaternion.identity);
            patch.GetComponent<Patch>().World = world;
            audioManager.PlaySound(AudioManager.SoundID.OpenShelf);
        }
    }
}
