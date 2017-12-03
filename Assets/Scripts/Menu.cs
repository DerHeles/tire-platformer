using UnityEngine;
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
    [SerializeField] private Toggle fullscreenTogglePause;
    [SerializeField] private GameObject menuBackground;
    [SerializeField] private GameObject gameEndPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Slider musicSliderOptions;
    [SerializeField] private Slider musicSliderPause;
    [SerializeField] private Slider sfxSliderOptions;
    [SerializeField] private Slider sfxSliderPause;

    [SerializeField] private GameObject audioManagerPrefab;
    private AudioManager audioManager;

    public PlayerController Player
    {
        get { return player; }
        set { player = value; }
    }
    private PlayerController player;

    private void Start()
    {
        // To maintain this between scenes
        DontDestroyOnLoad(gameObject);

        resDropdown.onValueChanged.AddListener(OnResolutionChanged);
        resDropdownPause.onValueChanged.AddListener(OnResolutionChanged);

        fullscreenToggle.onValueChanged.AddListener(OnFullscreenToggle);
        fullscreenToggle.isOn = Screen.fullScreen;

        fullscreenTogglePause.onValueChanged.AddListener(OnFullscreenToggle);
        fullscreenTogglePause.isOn = Screen.fullScreen;

        audioManager = GameObject.Find("AudioManager") ? GameObject.Find("AudioManager").GetComponent<AudioManager>() : Instantiate(audioManagerPrefab).GetComponent<AudioManager>();
        audioManager.name = "AudioManager";
        audioManager.PlayMenuMusic();

        resDropdown.captionText.text = "Resolution";
        resDropdownPause.captionText.text = "Resolution";
    }

    public void OnFullscreenToggle(bool isOn)
    {
        Screen.fullScreen = isOn;
    }

    public void OnResolutionChanged(int index)
    {
        Screen.SetResolution(Screen.resolutions[index].width, Screen.resolutions[index].height, Screen.fullScreen);
    }

    public void StartGame()
    {
        mainPanel.SetActive(false);
        menuBackground.SetActive(false);

        SceneManager.LoadScene("Main");
        audioManager.PlayIngameMusic();
    }

    public void SetMusicVolume(float volume)
    {
        audioManager.SetMusicVolume(volume);

        musicSliderOptions.value = volume;
        musicSliderPause.value = volume;
    }

    public void SetSfxVolume(float volume)
    {
        audioManager.SetSfxVolume(volume);

        sfxSliderOptions.value = volume;
        sfxSliderPause.value = volume;
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
        fullscreenToggle.isOn = Screen.fullScreen;

        mainPanel.SetActive(false);
        optionsPanel.SetActive(true);

        Resolution[] resolutions = Screen.resolutions;

        resDropdown.options.Clear();
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
        }
    }

    public void OpenCreditsMenu()
    {
        mainPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void OpenPauseMenu()
    {
        fullscreenTogglePause.isOn = Screen.fullScreen;

        pausePanel.SetActive(true);

        Resolution[] resolutions = Screen.resolutions;

        resDropdownPause.options.Clear();
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
        }
    }

    public void ClosePauseMenu()
    {
        pausePanel.SetActive(false);
        Player.PauseMenuClosed();
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
        audioManager.PlayMenuMusic();
    }

    public void Restart()
    {
        gameOverPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        audioManager.PlayIngameMusic();
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
