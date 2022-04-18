using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void Start()
    {
        Invoke(nameof(ijj), 2f);
    }

    private void ijj()
    {
        Debug.Log("Start Loading");
        StartCoroutine(LoadingScene());
    }

    private IEnumerator LoadingScene()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(1);
        asyncOperation.allowSceneActivation = false;
        while (asyncOperation.progress < 0.9f)
        {
            yield return null;
        }

        yield return null;
        asyncOperation.allowSceneActivation = true;
        Debug.Log("End Loading");
    }
}
