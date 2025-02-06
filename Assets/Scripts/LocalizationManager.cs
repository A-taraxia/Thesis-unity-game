using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro; // If using TextMeshPro

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance;

    public enum Language { English, Greek }
    private Language currentLanguage = Language.English;

    private Dictionary<string, string> englishTexts = new Dictionary<string, string>()
    {
        {"start_game", "Start Game"},
        {"exit_game", "Exit Game"},
        {"settings", "Settings"},
        {"exit_prompt", "Do you want to exit to the main menu?"},
        {"yes", "Yes"},
        {"no", "No"},
        {"volume", "Volume"},
        {"language", "Language"},
        {"closeSettings", "Close"},
        {"findSeat", "Find your seat."},
    };

    private Dictionary<string, string> greekTexts = new Dictionary<string, string>()
    {
        {"start_game", "Ξεκίνα"},
        {"exit_game", "Έξοδος"},
        {"settings", "Ρυθμίσεις"},
        {"exit_prompt", "Θέλετε να επιστρέψετε στο κύριο μενού;"},
        {"yes", "Ναι"},
        {"no", "Όχι"},
        {"volume", "Ένταση"},
        {"language", "Γλώσσα"},
        {"closeSettings", "Κλείσιμο"},
        {"findSeat", "Βρες την θέση σου."},
    };

    private Dictionary<string, string> activeLanguageDictionary;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadLanguage(); // Load saved language preference
        SceneManager.sceneLoaded += OnSceneLoaded; // Apply language when a new scene loads
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Clean up to prevent memory leaks
    }

    private void LoadLanguage()
    {
        int savedLanguage = PlayerPrefs.GetInt("Language", 0); // Default: English (0)
        currentLanguage = (Language)savedLanguage;
        activeLanguageDictionary = (currentLanguage == Language.English) ? englishTexts : greekTexts;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateLocalizedText(); // Update text when a new scene loads
    }

    public void ChangeLanguage(int languageIndex)
    {
        currentLanguage = (Language)languageIndex;
        PlayerPrefs.SetInt("Language", languageIndex);
        PlayerPrefs.Save();
        activeLanguageDictionary = (currentLanguage == Language.English) ? englishTexts : greekTexts;

        UpdateLocalizedText(); // Update all localized text in the scene
    }

    public string GetLocalizedText(string key)
    {
        if (activeLanguageDictionary.ContainsKey(key))
        {
            return activeLanguageDictionary[key];
        }
        return key; // Fallback to key if not found
    }

    public void UpdateLocalizedText()
    {
        LocalizedText[] localizedTexts = FindObjectsOfType<LocalizedText>();
        foreach (LocalizedText text in localizedTexts)
        {
            text.UpdateText();
        }
    }
}
