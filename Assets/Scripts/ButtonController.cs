using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public GameObject settingsPanel;
    public ScreenFader screenFader;   // Reference to the ScreenFader script

    public void StartGame()
    {
        if (screenFader != null)
        {
            screenFader.FadeToScene("FindSeatScene"); // Trigger the fade-out effect
        }
        else
        {
            Debug.LogError("ScreenFader is not assigned!");
        }
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }

    public void OpenSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);
        }
    }

    public void CloseSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);

            // If we're in the main menu, keep the cursor visible
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MainMenu")
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                // In other scenes (gameplay), we resume the game and lock/hide the cursor
                Time.timeScale = 1f;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

}
