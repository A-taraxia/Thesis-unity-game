using System.Collections;
using TMPro;
using UnityEngine;

public class DisplayMessage : MonoBehaviour
{
    public TextMeshProUGUI messageText;   // First message
    public TextMeshProUGUI secondText;    // Second message (hidden initially)
    public AudioSource dialogueAudioSource;  // Separate AudioSource for dialogue
    public AudioClip wagonIsEmptyClip;    // Assign WagonIsEmpty.wav in Inspector

    public float displayTime = 3f;
    public float fadeTime = 1f;
    public float delayBetweenMessages = 1f; // Small delay before second message


    void Start()
    {
        StartCoroutine(ShowMessages());
    }

    IEnumerator ShowMessages()
    {
        // Ensure LocalizationManager is active
        if (LocalizationManager.Instance == null)
        {
            yield break;
        }

        //Show first message
        string firstMessage = LocalizationManager.Instance.GetLocalizedText("findSeat");
        messageText.text = firstMessage;
        messageText.enabled = true;
        yield return new WaitForSeconds(displayTime);

        // Fade out first message
        yield return FadeOutText(messageText);

        yield return new WaitForSeconds(delayBetweenMessages);

        // Ensure second message exists in localization
        string localizedSecondMessage = LocalizationManager.Instance.GetLocalizedText("dial1");
        if (localizedSecondMessage == "dial1")
        {
            Debug.LogError("Localization key 'dial1' is missing!");
            yield break;
        }

        // Show second message
        secondText.text = localizedSecondMessage;
        secondText.enabled = true;

        // Play dialogue (WagonIsEmpty.wav) immediately
        if (dialogueAudioSource != null && wagonIsEmptyClip != null)
        {
            dialogueAudioSource.PlayOneShot(wagonIsEmptyClip);
            yield return new WaitForSeconds(wagonIsEmptyClip.length);
        }
        else
        {
            Debug.LogError("AudioSource or WagonIsEmptyClip is missing!");
        }

        // Hide second text once audio is done
        secondText.enabled = false;
    }

    IEnumerator FadeOutText(TextMeshProUGUI text)
    {
        Color originalColor = text.color;
        for (float t = 0; t < fadeTime; t += Time.deltaTime)
        {
            text.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(1, 0, t / fadeTime));
            yield return null;
        }
        text.enabled = false;
    }
}
