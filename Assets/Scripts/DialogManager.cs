using System.Collections;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI dialogText;
    public AudioSource dialogAudioSource;
    public AudioClip breathingSound; // Assign the breathing loop in the inspector
    public AudioClip voiceOverClip; // Assign in Inspector instead of loading dynamically


    private bool isPlayingBreathing = false;

    public IEnumerator PlayVoiceOverWithText(string dialogKey, string breathingKey)
    {
        if (dialogText == null || dialogAudioSource == null)
        {
            Debug.LogError("Missing references in VoiceOverManager!");
            yield break;
        }

        string localizedText = LocalizationManager.Instance.GetLocalizedText(dialogKey);
        dialogText.text = localizedText;
        dialogText.enabled = true;

        AudioClip voiceClip = voiceOverClip; // Use the manually assigned clip

        if (voiceClip != null)
        {
            dialogAudioSource.clip = voiceClip;
            dialogAudioSource.Play();
            yield return new WaitForSeconds(voiceClip.length); // Wait for the audio to finish
        }
        else
        {
            Debug.LogError($"Voice clip '{dialogKey}' not found in Resources/Sounds/");
        }

        dialogText.enabled = false;

        // Start the breathing sound loop after the dialog ends
        if (!isPlayingBreathing)
        {
            AudioClip breathingClip = breathingSound; // Assign manually from Inspector
            if (breathingClip != null)
            {
                dialogAudioSource.clip = breathingClip;
                dialogAudioSource.loop = true;
                StartCoroutine(FadeInBreathingSound(5f)); // 3 seconds fade-in
                isPlayingBreathing = true;
            }
            else
            {
                Debug.LogError("Breathing sound not assigned in the Inspector!");
            }
        }
    }
    IEnumerator FadeInBreathingSound(float duration)
    {
        float startVolume = 0f;
        float targetVolume = 1f; // Adjust if needed
        float elapsedTime = 0f;

        dialogAudioSource.volume = startVolume; // Start with no sound
        dialogAudioSource.clip = breathingSound;
        dialogAudioSource.loop = true;
        dialogAudioSource.Play();

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            dialogAudioSource.volume = Mathf.Lerp(startVolume, targetVolume, elapsedTime / duration);
            yield return null;
        }

        dialogAudioSource.volume = targetVolume; // Ensure it reaches full volume
    }

}
