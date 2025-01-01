using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public string nextSceneName;

    public void OnCutsceneEnd()
    {
        Debug.Log("Cutscene ended. Loading next scene...");
        SceneManager.LoadScene(nextSceneName);
    }
}
