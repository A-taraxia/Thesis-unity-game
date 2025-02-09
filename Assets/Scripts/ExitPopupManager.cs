using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitPopupManager : MonoBehaviour
{
    public GameObject exitPopup;
    private bool isPaused = false;

    private BackgroundAudioManager bgAudioManager;
    private VoiceOverManager voiceOverManager;

    void Start()
    {
        // Find the BackgroundAudioManager and VoiceOverManager (if they exist)
        bgAudioManager = FindObjectOfType<BackgroundAudioManager>();
        voiceOverManager = FindObjectOfType<VoiceOverManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleExitPopup();
        }
    }

    public void ToggleExitPopup()
    {
        if (exitPopup == null) return;

        isPaused = !exitPopup.activeSelf;
        exitPopup.SetActive(isPaused);

        // Pause or resume the game
        Time.timeScale = isPaused ? 0f : 1f;

        // Show or hide cursor
        Cursor.visible = isPaused;
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;

        // Safely pause/resume background audio (only if it exists)
        if (bgAudioManager != null)
        {
            if (isPaused)
                bgAudioManager.PauseBackgroundAudio();
            else
                bgAudioManager.ResumeBackgroundAudio();
        }

        // Pause/resume the voice-over AudioSource
        if (voiceOverManager != null)
        {
            if (isPaused)
                voiceOverManager.PauseVoiceOver();
            else
                voiceOverManager.ResumeVoiceOver();
        }
    }

    public void ConfirmExit()
    {
        Time.timeScale = 1f; // Resume time before switching scenes

        // Destroy BackgroundAudioManager safely
        if (bgAudioManager != null)
        {
            Destroy(bgAudioManager.gameObject);
        }

        SceneManager.LoadScene("MainMenu");
    }

    public void CancelExit()
    {
        ToggleExitPopup(); // Close the popup and resume the game
    }
}
