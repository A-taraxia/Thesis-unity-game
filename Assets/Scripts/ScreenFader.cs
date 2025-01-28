using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScreenFader : MonoBehaviour
{
    public Image blackScreenImage;   // Reference to the Image component
    public float fadeDuration = 1f; // Duration of the fade effect

    private void Start()
    {
        // Ensure the black screen starts transparent and raycast blocking is disabled
        blackScreenImage.raycastTarget = false;
        SetAlpha(0f);

        // Start with a fade-in effect
        StartCoroutine(FadeIn());
    }

    public void FadeToScene(string sceneName)
    {
        // Start the fade-out effect and load the scene
        StartCoroutine(FadeOutAndLoadScene(sceneName));
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color color = blackScreenImage.color;

        // Enable the black screen to block raycasts during the fade
        blackScreenImage.raycastTarget = true;

        // Fade from black (alpha = 1) to transparent (alpha = 0)
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            blackScreenImage.color = color;
            yield return null;
        }

        // Ensure the screen is fully transparent and raycasts are disabled
        SetAlpha(0f);
        blackScreenImage.raycastTarget = false;
    }

    private IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        float elapsedTime = 0f;
        Color color = blackScreenImage.color;

        // Enable the black screen to block raycasts during the fade
        blackScreenImage.raycastTarget = true;

        // Fade from transparent (alpha = 0) to black (alpha = 1)
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            blackScreenImage.color = color;
            yield return null;
        }

        // Ensure the screen is fully black
        SetAlpha(1f);

        // Load the next scene
        SceneManager.LoadScene(sceneName);
    }

    private void SetAlpha(float alpha)
    {
        Color color = blackScreenImage.color;
        color.a = alpha;
        blackScreenImage.color = color;
    }
}
