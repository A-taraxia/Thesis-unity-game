using TMPro;
using UnityEngine;

public class LocalizedText : MonoBehaviour
{
    public string key;
    private TextMeshProUGUI textMesh;

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();

        if (textMesh == null)
        {
            Debug.LogError("TextMeshProUGUI component is missing on this GameObject.");
        }

        UpdateText();
    }


    public void UpdateText()
    {
        textMesh.text = LocalizationManager.instance.GetLocalizedValue(key);
    }
}
