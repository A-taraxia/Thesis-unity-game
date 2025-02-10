using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BadGuyCutscene : MonoBehaviour
{
    public Transform targetPosition; // Assign the final stop position in the Inspector
    public float stopThreshold = 0.1f; // Adjust if needed

    void Update()
    {
        // Check if the Bad Guy's X position is close to the target's X position
        if (Mathf.Abs(transform.position.x - targetPosition.position.x) < stopThreshold)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        // Load the next scene first
        SceneManager.LoadScene("StatisticsScene");

        // Delay destroying BackgroundAudioManager to prevent null issues
        StartCoroutine(DestroyBackgroundAudio());
    }

    IEnumerator DestroyBackgroundAudio()
    {
        yield return new WaitForSeconds(1f); // Give time for scene transition
        if (BackgroundAudioManager.Instance != null)
        {
            Destroy(BackgroundAudioManager.Instance.gameObject);
        }
    }

}
