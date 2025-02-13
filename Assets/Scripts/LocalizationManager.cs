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
        {"vo_1", "Every day, millions of women navigate public spaces and transportation. While most journeys are safe, some face the threat of harassment or violence. Knowledge and awareness can help prevent and respond to such situations"},
        {"vo_2", "In the European Union, 33% of women have experienced physical or sexual violence since age 15. (Source: FRA, 2014)\r\n18% of these incidents occur in public spaces, including streets, parks, and transportation hubs. (FRA, 2014)\r\nIn the UK, over one-third of women report experiencing sexual harassment on public transport. (British Transport Police, 2023)\r\nMany incidents go unreported due to fear, stigma, or lack of trust in authorities."},
        {"vo_3", "Trust Your Instincts – If a situation feels unsafe, act quickly. Move to a more crowded area or alert someone.\r\nUse Your Voice – Clearly and loudly say \"STOP\" or \"LEAVE ME ALONE\" to draw attention.\r\nFind a Safe Spot – On public transport, move near the driver or exit. In public areas, seek out well-lit, populated spaces.\r\nCall for Help – Use emergency numbers or apps to alert authorities.\r\nReport the Incident – If safe, document details and report to authorities. Your report can help prevent future incidents."},
        {"vo_4", "Awareness is the first step to change. Stand together. Speak up. Support each other."},
        {"dial1", "How nice? The wagon is empty..."},
        {"dial2", "I thought I was alone..."},
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
        {"vo_1", "Κάθε μέρα, εκατομμύρια γυναίκες μετακινούνται σε δημόσιους χώρους και μέσα μεταφοράς. Αν και οι περισσότερες διαδρομές είναι ασφαλείς, κάποιες γυναίκες αντιμετωπίζουν την απειλή της παρενόχλησης ή της βίας. Η γνώση και η ευαισθητοποίηση μπορούν να βοηθήσουν στην πρόληψη και την αντίδραση σε τέτοιες καταστάσεις."},
        {"vo_2", "Στην Ευρωπαϊκή Ένωση, το 33% των γυναικών έχουν βιώσει σωματική ή σεξουαλική βία από την ηλικία των 15 ετών. (Πηγή: FRA, 2014)\nΤο 18% αυτών των περιστατικών συμβαίνουν σε δημόσιους χώρους, όπως δρόμοι, πάρκα και σταθμοί μεταφορών. (FRA, 2014)\nΣτο Ηνωμένο Βασίλειο, περισσότερες από το ένα τρίτο των γυναικών αναφέρουν ότι έχουν βιώσει σεξουαλική παρενόχληση σε δημόσιες συγκοινωνίες. (British Transport Police, 2023)\nΠολλά περιστατικά δεν καταγγέλλονται λόγω φόβου, στίγματος ή έλλειψης εμπιστοσύνης στις αρχές."},
        {"vo_3", "Εμπιστεύσου το ένστικτό σου – Αν μια κατάσταση φαίνεται μη ασφαλής, δράσε γρήγορα. Μετακινήσου σε πιο πολυσύχναστο μέρος ή ειδοποίησε κάποιον.\nΧρησιμοποίησε τη φωνή σου – Πες ξεκάθαρα και δυνατά \"ΣΤΑΜΑΤΑ\" ή \"ΑΦΗΣΕ ΜΕ ΗΣΥΧΗ\" για να τραβήξεις την προσοχή.\nΒρες ένα ασφαλές σημείο – Στα μέσα μεταφοράς, μετακινήσου κοντά στον οδηγό ή στην έξοδο. Σε δημόσιους χώρους, αναζήτησε φωτεινά, πολυσύχναστα μέρη.\nΚάλεσε βοήθεια – Χρησιμοποίησε αριθμούς έκτακτης ανάγκης ή εφαρμογές για να ειδοποιήσεις τις αρχές.\nΑναφορά του περιστατικού – Αν είναι ασφαλές, κατέγραψε λεπτομέρειες και ανέφερε το στις αρχές. Η αναφορά σου μπορεί να βοηθήσει στην πρόληψη μελλοντικών περιστατικών."},
        {"vo_4", "Η ευαισθητοποίηση είναι το πρώτο βήμα για αλλαγή. Σταθείτε μαζί. Μιλήστε ανοιχτά. Υποστηρίξτε η μία την άλλη."},
        {"dial1", "Τι καλά; το βαγόνι είναι άδειο..."},
        {"dial2", "Νόμιζα ότι είμαι μόνη μου..."},

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
        FindObjectOfType<SettingsController>()?.UpdateVolumeText();
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
