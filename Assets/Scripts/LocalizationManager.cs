using System.IO;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager instance;
    private LanguageData localizedText;
    private string currentLanguage = "English";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetLanguage(int languageIndex)
    {
        string[] languages = { "English", "Greek" };
        currentLanguage = languages[languageIndex];

        LoadLocalizedText();
    }

    private void LoadLocalizedText()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "languages.json");

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

            if (currentLanguage == "English")
            {
                localizedText = loadedData.English;
            }
            else if (currentLanguage == "Greek")
            {
                localizedText = loadedData.Greek;
            }

            Debug.Log("Loaded language: " + currentLanguage);
        }
        else
        {
            Debug.LogError("Cannot find file: " + filePath);
        }
    }

    public string GetLocalizedValue(string key)
    {
        switch (key)
        {
            case "start_game":
                return localizedText.start_game;
            case "exit_game":
                return localizedText.exit_game;
            case "settings":
                return localizedText.settings;
            default:
                Debug.LogWarning("Key not found: " + key);
                return key;
        }
    }
}
