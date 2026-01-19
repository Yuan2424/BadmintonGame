using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void loadLevel()
    {
        StartCoroutine(loadAsynchronous(SceneManager.GetActiveScene().buildIndex + 1));
    }
    IEnumerator loadAsynchronous(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            //Debug.Log("operation.progress");
            yield return null;
        }
    }
}
