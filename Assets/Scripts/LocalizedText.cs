using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LocalizedText : MonoBehaviour
{
    public string key; // The localization key for this text
    private Text uiText;
    private TMP_Text tmpText;

    void Awake()
    {
        uiText = GetComponent<Text>();
        tmpText = GetComponent<TMP_Text>();
        UpdateText();
    }

    public void UpdateText()
    {
        string localizedString = LocalizationManager.Instance.GetLocalizedText(key);

        if (uiText != null) uiText.text = localizedString;
        if (tmpText != null) tmpText.text = localizedString;
    }
}
