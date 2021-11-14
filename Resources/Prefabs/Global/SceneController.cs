using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Popup.Defines;




public class SceneController : MonoBehaviour
{
    AsyncOperation operation = null;

    public void Load(SceneType sceneType)
    {
        operation = SceneManager.LoadSceneAsync((int)sceneType);
        StartCoroutine(LoadAsync());
    }

    IEnumerator LoadAsync()
    {
        while (!operation.isDone)
            yield return null;
        operation.allowSceneActivation = true;
        operation = null;
    }
}
