using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class VoiceOverManager : MonoBehaviour
{
    public TextMeshProUGUI scriptText; // Assign the UI Text
    public AudioSource audioSource; // Assign the AudioSource
    public AudioClip[] voiceOvers; // Assign 4 voice-over clips
    private string[] textKeys = { "vo_1", "vo_2", "vo_3", "vo_4" }; // Localization keys

    private int currentIndex = 0;

    void Start()
    {
        StartCoroutine(PlayVoiceOverSequence());
    }

    IEnumerator PlayVoiceOverSequence()
    {
        while (currentIndex < voiceOvers.Length)
        {
            // Get the correct localized text from LocalizationManager
            string localizedText = LocalizationManager.Instance.GetLocalizedText(textKeys[currentIndex]);
            scriptText.text = localizedText; // Set UI Text

            // Play corresponding voice-over
            audioSource.clip = voiceOvers[currentIndex];
            audioSource.Play();

            // Wait until the voice-over finishes
            yield return new WaitForSeconds(audioSource.clip.length + 1f);

            currentIndex++;
        }

        // After all voice-overs finish, wait a few seconds, then return to Main Menu
        yield return new WaitForSeconds(3f);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("MainMenu");

    }
    public void UpdateCurrentText()
    {
        //  Update the currently displayed text immediately after language change
        if (scriptText != null && currentIndex < textKeys.Length)
        {
            scriptText.text = LocalizationManager.Instance.GetLocalizedText(textKeys[currentIndex]);
        }
    }


    public void PauseVoiceOver()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }

    public void ResumeVoiceOver()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.UnPause();
        }
    }
}
