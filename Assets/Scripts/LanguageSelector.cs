using TMPro;
using UnityEngine;

public class LanguageSelector : MonoBehaviour
{
    public TMP_Dropdown languageDropdown;

    void Start()
    {
        int savedLanguage = PlayerPrefs.GetInt("SelectedLanguage", 0);
        languageDropdown.value = savedLanguage;

        LocalizationManager.instance.SetLanguage(savedLanguage);

        languageDropdown.onValueChanged.AddListener(ChangeLanguage);
    }

    public void ChangeLanguage(int selectedLanguage)
    {
        PlayerPrefs.SetInt("SelectedLanguage", selectedLanguage);
        PlayerPrefs.Save();

        LocalizationManager.instance.SetLanguage(selectedLanguage);

        LocalizedText[] localizedTexts = FindObjectsOfType<LocalizedText>();
        foreach (var localizedText in localizedTexts)
        {
            localizedText.UpdateText();
        }
    }
}
