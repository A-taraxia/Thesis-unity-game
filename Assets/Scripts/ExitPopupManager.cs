using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitPopupManager : MonoBehaviour
{
    public GameObject exitPopup;

    private bool isPaused = false;

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

        // Pause or resume the background audio
        if (isPaused)
            BackgroundAudioManager.Instance?.PauseBackgroundAudio();
        else
            BackgroundAudioManager.Instance?.ResumeBackgroundAudio();
    }

    public void ConfirmExit()
    {
        Time.timeScale = 1f; // Resume time before switching scenes

        // Check if BackgroundAudioManager exists before destroying
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
