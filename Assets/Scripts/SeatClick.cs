using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class SeatClick : MonoBehaviour
{
    public string nextSceneName;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Seat")) 
                {
                    Debug.Log("Next Scene");
                    StartCoroutine(SitDownAndChangeScene());
                }
            }
        }
    }

    IEnumerator SitDownAndChangeScene()
    {

        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene(nextSceneName);
    }
}
