using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private Dropdown resDropdown;
    

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        Resolution[] resolutions = Screen.resolutions;

        resDropdown.options.Clear();

        foreach (var res in resolutions)
        {
            //Debug.Log(res);
            resDropdown.options.Add(new Dropdown.OptionData(res.width + " x " + res.height));

        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main");
        mainPanel.SetActive(false);

        Resolution[] resolutions = Screen.resolutions;

        //resDropdown.options.Clear();

        foreach (var res in resolutions)
        {
            Debug.Log(res);
            //resDropdown.options.Add(new Dropdown.OptionData(res.ToString()));
        }


    }

    public void SetMusicVolume(float volume)
    {
        Debug.Log("Music Volume = " + volume);
    }

    public void SetSfxVolume(float volume)
    {
        Debug.Log("SFX Volume = " + volume);
    }

}
