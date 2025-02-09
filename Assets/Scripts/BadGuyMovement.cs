using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class BadGuyMovement : MonoBehaviour
{
    public Transform[] positions; // Assign 4 positions in Inspector
    public Image screenFade; // Assign a black UI Image covering the screen
    public float timeBeforeMove = 5f; // Move if not looked at for 5 sec
    public float timeBeforeFlicker = 900f; // 15 min stare causes flickering
    public float flickerTotalDuration = 1.2f; // Total flicker duration
    public float flickerMinTime = 0.05f; // Min time for a flicker
    public float flickerMaxTime = 0.2f; // Max time for a flicker

    private Camera mainCamera;
    private int currentPositionIndex = 0;
    private bool isMoving = false;
    private float notLookingTime = 0f;
    private float lookingTime = 0f;

    void Start()
    {
        mainCamera = Camera.main;
        screenFade.color = new Color(0, 0, 0, 0); // Start with a clear screen
    }

    void Update()
    {
        if (IsEnemyVisible())
        {
            notLookingTime = 0f;
            lookingTime += Time.deltaTime;
        }
        else
        {
            lookingTime = 0f;
            notLookingTime += Time.deltaTime;

            if (notLookingTime >= timeBeforeMove && !isMoving)
            {
                StartCoroutine(MoveCloser());
            }
        }

        if (lookingTime >= timeBeforeFlicker && !isMoving)
        {
            StartCoroutine(FlickerAndMove());
        }
    }

    bool IsEnemyVisible()
    {
        Vector3 viewportPoint = mainCamera.WorldToViewportPoint(transform.position);
        return viewportPoint.z > 0 && viewportPoint.x > 0 && viewportPoint.x < 1 && viewportPoint.y > 0 && viewportPoint.y < 1;
    }

    IEnumerator MoveCloser()
    {
        isMoving = true;

        if (currentPositionIndex < positions.Length - 1)
        {
            currentPositionIndex++;
            MoveToNewPosition();
        }

        if (currentPositionIndex == positions.Length - 1)
        {
            EndGame();
        }

        yield return new WaitForSeconds(1f);
        isMoving = false;
    }

    IEnumerator FlickerAndMove()
    {
        isMoving = true;
        lookingTime = 0f; // Reset looking time

        float elapsedTime = 0f;
        int flickerCount = Random.Range(4, 7); // Randomize flicker count

        for (int i = 0; i < flickerCount; i++)
        {
            screenFade.color = new Color(0, 0, 0, 1); // Black screen
            yield return new WaitForSeconds(Random.Range(flickerMinTime, flickerMaxTime));

            if (i == flickerCount - 2) // Move on the second-to-last flicker
            {
                if (currentPositionIndex < positions.Length - 1)
                {
                    currentPositionIndex++;
                    MoveToNewPosition();
                }
                if (currentPositionIndex == positions.Length - 1)
                {
                    Debug.Log("Game Over - Bad Guy teleported in front!");
                    EndGame();
                }
            }

            screenFade.color = new Color(0, 0, 0, 0); // Clear screen
            yield return new WaitForSeconds(Random.Range(flickerMinTime, flickerMaxTime));
            elapsedTime += flickerMinTime + flickerMaxTime;
        }

        yield return new WaitForSeconds(1f);
        isMoving = false;
    }

    void MoveToNewPosition()
    {
        transform.position = positions[currentPositionIndex].position;
        transform.rotation = positions[currentPositionIndex].rotation;
    }

    void EndGame()
    {
        // Destroy background music when transitioning to the statistics scene
        if (BackgroundAudioManager.Instance != null)
        {
            Destroy(BackgroundAudioManager.Instance.gameObject);
        }

        // Load the next scene
        SceneManager.LoadScene("StatisticsScene");
    }
}
