[System.Serializable]
public class LocalizationData
{
    public LanguageData English;
    public LanguageData Greek;
}

[System.Serializable]
public class LanguageData
{
    public string start_game;
    public string exit_game;
    public string settings;
}
