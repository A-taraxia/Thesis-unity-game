using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;

    public GameObject settingsPanel;                 // Reference to the settings panel
    public Slider volumeSlider;                      // Reference to the volume slider
    private float defaultVolume = 1f;                // Default volume value

    private bool isPaused = false;                   // Track if the game is paused

    void Awake()
    {
        // Singleton Pattern to persist SettingsManager across scenes
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
    }

    void Update()
    {
        // Open/close settings with F2
        if (Input.GetKeyDown(KeyCode.F2))
        {
            ToggleSettingsPanel();
        }
    }

    private void InitializeVolume()
    {
        // Load saved volume or use default
        float savedVolume = PlayerPrefs.GetFloat("Volume", defaultVolume);
        AudioListener.volume = savedVolume;

        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume * 100f; // Initialize slider value
            volumeSlider.onValueChanged.AddListener(delegate { AdjustVolume(); });
        }
    }


public void ToggleSettingsPanel()
{
    if (settingsPanel == null)
    {
        Debug.LogError("SettingsPanel is not assigned in the Inspector!");
        return;
    }

    isPaused = !settingsPanel.activeSelf; // Toggle the pause state
    settingsPanel.SetActive(!settingsPanel.activeSelf); // Show/hide the panel

    if (settingsPanel.activeSelf)
    {
        // Pause the game and show the cursor
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Pause background audio only if it still exists
        if (BackgroundAudioManager.Instance != null)
        {
            BackgroundAudioManager.Instance.PauseBackgroundAudio();
        }
            // Ensure UI elements are interactable
            EventSystem.current?.SetSelectedGameObject(null); // Reset selection
    }
    else
    {
        // Resume the game
        Time.timeScale = 1f;

        // Check if we're in the Main Menu
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            // Keep the cursor visible and unlocked in the Main Menu
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            // Hide and lock the cursor in other scenes (gameplay)
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
            // Resume background audio only if it still exists
            if (BackgroundAudioManager.Instance != null)
            {
                BackgroundAudioManager.Instance.ResumeBackgroundAudio();
            }
        }
}


    public void AdjustVolume()
    {
        if (volumeSlider != null)
        {
            float newVolume = volumeSlider.value / 100f; // Normalize slider value
            AudioListener.volume = newVolume;
            PlayerPrefs.SetFloat("Volume", newVolume); // Save the new volume
        }
    }
}
