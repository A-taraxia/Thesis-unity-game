using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BadGuyCutscene : MonoBehaviour
{
    public Transform targetPosition; // Assign in Inspector (position in front of player)
    public Animator animator; // Assign in Inspector (Animator component)
    public float moveSpeed = 1.5f; // Adjust speed to match animation

    private bool isWalking = false;

    void Start()
    {
        StartCoroutine(PlayStandUpThenWalk());
    }

    IEnumerator PlayStandUpThenWalk()
    {
        animator.SetTrigger("StandUp"); // Trigger the standing up animation
        yield return new WaitForSeconds(2f); // Wait for stand-up animation to finish

        animator.SetBool("IsWalking", true); // Start walking animation
        yield return new WaitForSeconds(0.2f); // Ensure animation starts BEFORE movement

        isWalking = true; // Now start moving
    }

    void Update()
    {
        if (isWalking)
        {
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 currentPosition = transform.position;

        // Move only on X-axis
        Vector3 target = new Vector3(targetPosition.position.x, currentPosition.y, currentPosition.z);

        transform.position = Vector3.MoveTowards(currentPosition, target, moveSpeed * Time.deltaTime);

        // If we reach the target, stop walking
        if (Mathf.Abs(transform.position.x - target.x) < 0.1f)
        {
            animator.SetBool("IsWalking", false);
            isWalking = false;

            // Fade out and switch scene
            StartCoroutine(FadeOutAndChangeScene());
        }
    }

    IEnumerator FadeOutAndChangeScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("StatisticsScene");
    }
}
