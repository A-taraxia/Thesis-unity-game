using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public Slider volumeSlider;
    public TextMeshProUGUI volumeText; 

    void Start()
    {
        if (volumeSlider != null)
        {
            volumeSlider.value = AudioListener.volume * 100;
            volumeSlider.onValueChanged.AddListener(delegate { AdjustVolume(); });
        }
    }

    public void AdjustVolume()
    {
        AudioListener.volume = volumeSlider.value / 100f; 

        if (volumeText != null)
        {
            volumeText.text = "Volume:" + volumeSlider.value.ToString("0");
        }
    }
}
