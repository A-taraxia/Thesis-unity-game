using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IndicatorScript : MonoBehaviour
{
    public TextMeshProUGUI reticleText; 
    public Color defaultColor = Color.white; 
    public Color interactColor = Color.red; 
    void Start()
    {
        reticleText.text = "."; 
        reticleText.color = defaultColor;
    }

    void OnMouseEnter()
    {
        reticleText.text = "+"; 
        reticleText.color = interactColor; 
    }

    void OnMouseExit()
    {
        reticleText.text = "."; 
        reticleText.color = defaultColor; 
    }
}
