using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SeatClick : MonoBehaviour
{
    public string nextSceneName; // Name of the scene to load after sitting
    public Transform player;     // Reference to the player's Transform
    public float interactionRange = 20f; // Maximum distance to interact with the seat

    void Update()
    {
        // Check if the player is within range of the seat
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= interactionRange)
        {
            // Check if the player presses the interaction button (e.g., left mouse button or "E")
            if (Input.GetMouseButtonDown(0)) // Replace with "Input.GetKeyDown(KeyCode.E)" if desired
            {
                StartCoroutine(SitDownAndChangeScene());
            }
        }
    }

    IEnumerator SitDownAndChangeScene()
    {
        // Optional: Add sitting animation or camera effect here
        yield return new WaitForSeconds(0.5f);

        // Load the next scene
        SceneManager.LoadScene(nextSceneName);
    }

}
