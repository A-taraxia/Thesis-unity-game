using UnityEngine;
using TMPro;

public class IndicatorScript : MonoBehaviour
{
    public TextMeshProUGUI reticleText;      // Reference to the reticle text
    public Color defaultColor = Color.white; // Default reticle color
    public Color interactColor = Color.red; // Reticle color when interaction is possible
    public Transform player;                // Reference to the player's Transform
    public float interactionRange = 20f;    // Maximum distance for interaction

    private bool isPlayerInRange = false;   // Tracks if the player is within range
    private bool isMouseOver = false;       // Tracks if the mouse is hovering over the object

    void Start()
    {
        // Initialize the reticle to default
        reticleText.text = ".";
        reticleText.color = defaultColor;
    }

    void Update()
    {
        // Ensure the player reference is assigned
        if (player == null)
        {
            Debug.LogError("Player reference is not assigned in the IndicatorScript.");
            return;
        }

        // Calculate the distance to the player
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        // Update whether the player is in range
        isPlayerInRange = distanceToPlayer <= interactionRange;

        // Continuously update the reticle if the mouse is hovering over the object
        if (isMouseOver)
        {
            if (isPlayerInRange)
            {
                reticleText.text = "+";
                reticleText.color = interactColor;
            }
            else
            {
                ResetReticle(); // If the player moves out of range, reset the reticle
            }
        }
    }

    void OnMouseEnter()
    {
        // Mark that the mouse is over the object
        isMouseOver = true;

        // Update the reticle if the player is in range
        if (isPlayerInRange)
        {
            reticleText.text = "+";
            reticleText.color = interactColor;
        }
    }

    void OnMouseExit()
    {
        // Mark that the mouse is no longer over the object
        isMouseOver = false;

        // Reset the reticle regardless of range when the mouse exits
        ResetReticle();
    }

    private void ResetReticle()
    {
        reticleText.text = ".";
        reticleText.color = defaultColor;
    }
}
