using System.Collections;
using TMPro;
using UnityEngine;

public class DisplayMessage : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public float displayTime = 3f;
    public float fadeTime = 1f;

    void Start()
    {
        StartCoroutine(ShowMessage("Find your seat.", displayTime));
    }

    IEnumerator ShowMessage(string message, float delay)
    {
        messageText.text = message; 
        messageText.enabled = true;

        yield return new WaitForSeconds(delay);

        for (float t=0; t < fadeTime; t+=Time.deltaTime) {
            messageText.color = new Color(messageText.color.r, messageText.color.g, messageText.color.b, Mathf.Lerp(1, 0, t / fadeTime));
            yield return null;
        }
        messageText.enabled=false;
    }
}
