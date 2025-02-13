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
        StartCoroutine(HandleSceneTransition());
    }

    IEnumerator HandleSceneTransition()
    {
        // Destroy BackgroundAudioManager first
        if (BackgroundAudioManager.Instance != null)
        {
            Destroy(BackgroundAudioManager.Instance.gameObject);
        }

        //Wait a short moment for a smoother transition (optional)
        yield return new WaitForSeconds(0.5f);

        // Load the next scene
        SceneManager.LoadScene("StatisticsScene");
    }
}
