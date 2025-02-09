using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public Slider volumeSlider;
    public TextMeshProUGUI volumeText;
    private string volumeKey = "volume"; // Key for localization

    void Start()
    {
        if (volumeSlider != null)
        {
            // Load saved volume and update slider
            float savedVolume = PlayerPrefs.GetFloat("Volume", 1f); // Default volume: 1
            AudioListener.volume = savedVolume;
            volumeSlider.value = savedVolume * 100;

            volumeSlider.onValueChanged.AddListener(delegate { AdjustVolume(); });

            // Update volume text on start
            UpdateVolumeText();
        }
    }

    public void AdjustVolume()
    {
        AudioListener.volume = volumeSlider.value / 100f;
        PlayerPrefs.SetFloat("Volume", AudioListener.volume); // Save volume setting

        UpdateVolumeText();
    }

    public void UpdateVolumeText()
    {
        if (volumeText != null)
        {
            string localizedVolumeText = LocalizationManager.Instance.GetLocalizedText(volumeKey);
            volumeText.text = localizedVolumeText + ": " + volumeSlider.value.ToString("0");
        }
    }

    // Call this when the settings panel is opened
    public void OnSettingsOpened()
    {
        UpdateVolumeText();
    }
}
