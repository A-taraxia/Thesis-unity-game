using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BadGuyMovement : MonoBehaviour
{
    public Transform[] positions;
    public Image screenFade;
    public float timeBeforeMove = 5f;
    public float timeBeforeFlicker = 900f;
    public float flickerTotalDuration = 1.2f;
    public float flickerMinTime = 0.05f;
    public float flickerMaxTime = 0.2f;

    private Camera mainCamera;
    private int currentPositionIndex = 0;
    private bool isMoving = false;
    private float notLookingTime = 0f;
    private float lookingTime = 0f;

    private bool dialogFinished = false;  // New: Prevent movement before intro audio ends

    void Start()
    {
        mainCamera = Camera.main;
        screenFade.color = new Color(0, 0, 0, 0);

        StartCoroutine(PlayIntroDialog()); // Start the intro dialog before anything else
    }

    void Update()
    {
        if (!dialogFinished) return;  //  New: Wait until dialog finishes

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

    IEnumerator PlayIntroDialog()
    {
        DialogManager voiceOver = FindObjectOfType<DialogManager>();
        if (voiceOver != null)
        {
            yield return voiceOver.PlayVoiceOverWithText("dial2", "BreathingLoop");
        }
        else
        {
            Debug.LogError("VoiceOverManager not found in the scene!");
        }
        dialogFinished = true;
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
        lookingTime = 0f;

        int flickerCount = Random.Range(4, 7);

        for (int i = 0; i < flickerCount; i++)
        {
            screenFade.color = new Color(0, 0, 0, 1);
            yield return new WaitForSeconds(Random.Range(flickerMinTime, flickerMaxTime));

            if (i == flickerCount - 2)
            {
                if (currentPositionIndex < positions.Length - 1)
                {
                    currentPositionIndex++;
                    MoveToNewPosition();
                }
                if (currentPositionIndex == positions.Length - 1)
                {
                    EndGame();
                }
            }

            screenFade.color = new Color(0, 0, 0, 0);
            yield return new WaitForSeconds(Random.Range(flickerMinTime, flickerMaxTime));
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
        StartCoroutine(FlickerBeforeSceneChange());
    }

    IEnumerator FlickerBeforeSceneChange()
    {
        isMoving = true;

        int flickerCount = Random.Range(5, 8);

        for (int i = 0; i < flickerCount; i++)
        {
            screenFade.color = new Color(0, 0, 0, 1);
            yield return new WaitForSeconds(Random.Range(flickerMinTime, flickerMaxTime));

            screenFade.color = new Color(0, 0, 0, 0);
            yield return new WaitForSeconds(Random.Range(flickerMinTime, flickerMaxTime));
        }

        screenFade.color = new Color(0, 0, 0, 1);
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("BadGuyApproachCutscene");
    }
}
