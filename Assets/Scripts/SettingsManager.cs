using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;


public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;

    public GameObject settingsPanel;
    public Slider volumeSlider;
    public TMP_Dropdown languageDropdown;
    public TextMeshProUGUI volumeText;


    private float defaultVolume = 1f;
    private bool isPaused = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        InitializeVolume();
        InitializeLanguageDropdown();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            ToggleSettingsPanel();
        }
    }

    private void InitializeVolume()
    {
        float savedVolume = PlayerPrefs.GetFloat("Volume", defaultVolume);
        AudioListener.volume = savedVolume;

        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume * 100f;
            volumeSlider.onValueChanged.AddListener(delegate { AdjustVolume(); });
        }
    }

    private void InitializeLanguageDropdown()
    {
        if (languageDropdown != null)
        {
            languageDropdown.onValueChanged.AddListener(ChangeLanguage);
            languageDropdown.value = PlayerPrefs.GetInt("Language", 0); // Load saved language
        }
    }

    public void ChangeLanguage(int index)
    {
        LocalizationManager.Instance.ChangeLanguage(index);
    }

    public void ToggleSettingsPanel()
    {
        if (settingsPanel == null)
        {
            Debug.LogError("SettingsPanel is not assigned in the Inspector!");
            return;
        }

        isPaused = !settingsPanel.activeSelf;
        settingsPanel.SetActive(!settingsPanel.activeSelf);

        // Find the Dialog Audio Source in the scene
        AudioSource dialogAudio = GameObject.Find("Dialog Audio Source")?.GetComponent<AudioSource>();
        VoiceOverManager voiceOverManager = FindObjectOfType<VoiceOverManager>();

        if (settingsPanel.activeSelf)
        {
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            // Only pause if BackgroundAudioManager exists
            if (BackgroundAudioManager.Instance != null)
            {
                BackgroundAudioManager.Instance.PauseBackgroundAudio();
            }

            if (dialogAudio != null && dialogAudio.isPlaying)
            {
                dialogAudio.Pause();
            }

            voiceOverManager?.PauseVoiceOver();

            EventSystem.current?.SetSelectedGameObject(null);
        }
        else
        {
            Time.timeScale = 1f;

            if (SceneManager.GetActiveScene().name == "MainMenu")
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }

            // Only resume if BackgroundAudioManager exists
            if (BackgroundAudioManager.Instance != null)
            {
                BackgroundAudioManager.Instance.ResumeBackgroundAudio();
            }

            if (dialogAudio != null)
            {
                dialogAudio.UnPause();
            }

            voiceOverManager?.ResumeVoiceOver();
        }
    }



    public void AdjustVolume()
    {
        if (volumeSlider != null)
        {
            float newVolume = volumeSlider.value / 100f;
            AudioListener.volume = newVolume;
            PlayerPrefs.SetFloat("Volume", newVolume);
        }
    }
    public void CloseSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }

        // Resume the game
        Time.timeScale = 1f;

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Resume Background Audio if it still exists
        if (BackgroundAudioManager.Instance != null)
        {
            BackgroundAudioManager.Instance.ResumeBackgroundAudio();
        }

        // Resume Dialog Audio if it exists in the scene
        AudioSource dialogAudio = GameObject.Find("Dialog Audio Source")?.GetComponent<AudioSource>();
        if (dialogAudio != null)
        {
            dialogAudio.UnPause();
        }

        // Resume VoiceOver if in the Statistics Scene
        VoiceOverManager voiceOverManager = FindObjectOfType<VoiceOverManager>();
        if (voiceOverManager != null)
        {
            voiceOverManager.ResumeVoiceOver();
        }
    }
    public void OpenSettings()
    {
        // Ensure settingsPanel is correctly assigned
        if (settingsPanel == null)
        {
            settingsPanel = GameObject.Find("SettingsPanel"); // Auto-find it
        }

        if (settingsPanel == null)
        {
            Debug.LogError("SettingsPanel is missing in this scene!");
            return;
        }

        settingsPanel.SetActive(true);
        isPaused = true;

        // Pause the game
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Pause background audio if it exists
        if (BackgroundAudioManager.Instance != null)
        {
            BackgroundAudioManager.Instance.PauseBackgroundAudio();
        }

        // Update the volume text immediately when opening settings
        UpdateVolumeText();
    }

    public void UpdateVolumeText()
    {
        if (volumeSlider != null && volumeText != null)
        {
            string localizedVolumeText = LocalizationManager.Instance.GetLocalizedText("volume");
            volumeText.text = localizedVolumeText + ": " + volumeSlider.value.ToString("0");
        }
    }



}