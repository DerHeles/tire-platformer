using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Dropdown resDropdown;
    [SerializeField] private Dropdown resDropdownPause;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private GameObject menuBackground;
    [SerializeField] private GameObject gameEndPanel;
    [SerializeField] private GameObject gameOverPanel;

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        //Resolution[] resolutions = Screen.resolutions;

        //resDropdown.options.Clear();
        //int i = 0;
        //foreach (var res in resolutions)
        //{
        //    string text = "";
        //    if (res.width.ToString().Length == 3)
        //        text += "  ";
        //    text += res.width.ToString();

        //    text += " x ";
        //    if (res.height.ToString().Length == 3)
        //        text += "  ";
        //    text += res.height.ToString();

        //    text += " @ " + res.refreshRate + "Hz";

        //    resDropdown.options.Add(new Dropdown.OptionData(text));

        //    if (Screen.width == res.width && Screen.height == res.height &&
        //        Screen.currentResolution.refreshRate == res.refreshRate)
        //    {
        //        resDropdown.value = i;
        //        resDropdown.Select();
        //    }
        //    i++;
        //}


        //resDropdown.Show();
        //resDropdown.captionText.text = resDropdown.options[0].text;

        resDropdown.onValueChanged.AddListener(OnResolutionChanged);
        resDropdownPause.onValueChanged.AddListener(OnResolutionChanged);

        fullscreenToggle.onValueChanged.AddListener(OnFullscreenToggle);

        fullscreenToggle.isOn = Screen.fullScreen;
    }

    public void OnFullscreenToggle(bool isOn)
    {
        Screen.fullScreen = isOn;
    }

    public void OnResolutionChanged(int index)
    {
        //Debug.Log("Resolution (" + index + ") = " + resDropdown.options[index].text);
        Screen.SetResolution(Screen.resolutions[index].width, Screen.resolutions[index].height, Screen.fullScreen);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main");
        mainPanel.SetActive(false);
        
        menuBackground.SetActive(false);
    }

    public void SetMusicVolume(float volume)
    {
        Debug.Log("Music Volume = " + volume);
    }

    public void SetSfxVolume(float volume)
    {
        Debug.Log("SFX Volume = " + volume);
    }

    public void QuitGame()
    {
        #if UNITY_STANDALONE
                Application.Quit();
        #endif
        
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void OpenOptionsMenu()
    {
        mainPanel.SetActive(false);
        optionsPanel.SetActive(true);

        Resolution[] resolutions = Screen.resolutions;

        resDropdown.options.Clear();
        int i = 0;
        foreach (var res in resolutions)
        {
            string text = "";
            if (res.width.ToString().Length == 3)
                text += "  ";
            text += res.width.ToString();

            text += " x ";
            if (res.height.ToString().Length == 3)
                text += "  ";
            text += res.height.ToString();

            text += " @ " + res.refreshRate + "Hz";

            resDropdown.options.Add(new Dropdown.OptionData(text));

            if (Screen.width == res.width && Screen.height == res.height &&
                Screen.currentResolution.refreshRate == res.refreshRate)
            {
                resDropdown.value = i;
                resDropdown.Select();
            }
            i++;
        }
    }

    //public void OpenMainMenu()
    //{
    //    creditsPanel.SetActive(false);
    //    optionsPanel.SetActive(false);
    //    mainPanel.SetActive(true);
    //    menuBackground.SetActive(true);
    //    gameEndPanel.SetActive(false);
    //    gameOverPanel.SetActive(false);
    //}

    public void OpenCreditsMenu()
    {
        mainPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void OpenPauseMenu()
    {
        pausePanel.SetActive(true);

        Resolution[] resolutions = Screen.resolutions;

        resDropdownPause.options.Clear();
        int i = 0;
        foreach (var res in resolutions)
        {
            string text = "";
            if (res.width.ToString().Length == 3)
                text += "  ";
            text += res.width.ToString();

            text += " x ";
            if (res.height.ToString().Length == 3)
                text += "  ";
            text += res.height.ToString();

            text += " @ " + res.refreshRate + "Hz";

            resDropdownPause.options.Add(new Dropdown.OptionData(text));

            if (Screen.width == res.width && Screen.height == res.height &&
                Screen.currentResolution.refreshRate == res.refreshRate)
            {
                resDropdownPause.value = i;
                resDropdownPause.Select();
            }
            i++;
        }
    }

    public void ClosePauseMenu()
    {
        pausePanel.SetActive(false);
    }

    public void CloseOptionsMenu()
    {
        optionsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void CloseCreditsMenu()
    {
        creditsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void QuitGameToMainMenu()
    {
        gameOverPanel.SetActive(false);
        gameEndPanel.SetActive(false);
        pausePanel.SetActive(false);
        menuBackground.SetActive(true);
        mainPanel.SetActive(true);
        
        SceneManager.LoadScene("Menu");
    }

    public void Restart()
    {
        gameOverPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OpenGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    public void OpenGameEndPanel()
    {
        gameEndPanel.SetActive(true);
    }
}
