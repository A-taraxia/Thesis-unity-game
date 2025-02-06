using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public string nextSceneName;

    public void OnCutsceneEnd()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
