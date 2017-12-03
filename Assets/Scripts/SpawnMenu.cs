using UnityEngine;

public class SpawnMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuPrefab;

    private void Start()
    {
        if (GameObject.Find("Menu") == null)
        {
            var menu = Instantiate(menuPrefab);
            menu.name = "Menu";
        }
    }
}
