using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitPopupManager : MonoBehaviour
{
    public GameObject exitPopup;
    private bool isPaused = false;
    private AudioSource dialogAudioSource; // Reference to the dialogue audio

    void Start()
    {
        // Find the dialogue audio source in the scene
        dialogAudioSource = GameObject.Find("Dialog Audio Source")?.GetComponent<AudioSource>();
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

        // Find Dialog Audio Source in the scene
        AudioSource dialogAudio = GameObject.Find("Dialog Audio Source")?.GetComponent<AudioSource>();
        VoiceOverManager voiceOverManager = FindObjectOfType<VoiceOverManager>(); // Find voice-over manager

        if (isPaused)
        {
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            // Pause Background Audio if it exists
            if (BackgroundAudioManager.Instance != null)
            {
                BackgroundAudioManager.Instance.PauseBackgroundAudio();
            }

            // Pause Dialog Audio if it's playing
            if (dialogAudio != null && dialogAudio.isPlaying)
            {
                dialogAudio.Pause();
            }

            // Pause Voice Over if it exists
            if (voiceOverManager != null)
            {
                voiceOverManager.PauseVoiceOver();
            }
        }
        else
        {
            Time.timeScale = 1f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            //Resume Background Audio if it exists
            if (BackgroundAudioManager.Instance != null)
            {
                BackgroundAudioManager.Instance.ResumeBackgroundAudio();
            }

            // Resume Dialog Audio
            if (dialogAudio != null)
            {
                dialogAudio.UnPause();
            }

            // Resume Voice Over
            if (voiceOverManager != null)
            {
                voiceOverManager.ResumeVoiceOver();
            }
        }
    }



    public void ConfirmExit()
    {
        Time.timeScale = 1f; // Resume time before switching scenes

        // Destroy BackgroundAudioManager when returning to main menu
        if (BackgroundAudioManager.Instance != null)
        {
            Destroy(BackgroundAudioManager.Instance.gameObject);
        }

        SceneManager.LoadScene("MainMenu");
    }

    public void CancelExit()
    {
        ToggleExitPopup(); // Close the popup and resume the game
    }
}
