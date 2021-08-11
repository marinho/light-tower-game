using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneUtils : MonoBehaviour
{
    int LoadingScene = 1;
    int MainScene = 2;

    public void Load(int sceneIndex) {
        StartCoroutine(LoadSceneInBackground(LoadingScene));
        StartCoroutine(LoadSceneInBackground(sceneIndex));
    }

    public void LoadMainScene() {
        Load(MainScene);
    }

    IEnumerator LoadSceneInBackground(int sceneIndex) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void Quit() {
        Application.Quit();
    }

    public void OpenURL(string url) {
        Application.OpenURL(url);
    }
}
