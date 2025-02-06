using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public GameObject settingsPanel;
    public ScreenFader screenFader;   // Reference to the ScreenFader script
    private SettingsManager settingsManager; // Reference to the persistent SettingsManager

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

    void Start()
    {
        // Find the persistent SettingsManager
        settingsManager = FindObjectOfType<SettingsManager>();
    }

    public void OpenSettings()
    {
        if (settingsManager == null)
        {
            return;
        }

        settingsManager.OpenSettings(); //  Call OpenSettings() from the persistent manager
    }


}