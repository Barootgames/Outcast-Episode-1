using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    int totalScenes;
    private void Start()
    {
        totalScenes = SceneManager.sceneCountInBuildSettings;
    }
    public void LoadNextScene()
    {
        int currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentBuildIndex + 1 <= totalScenes)
            SceneManager.LoadScene(currentBuildIndex + 1);
    }

    public void LoadPreviousScene()
    {
        int currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentBuildIndex - 1 >= 0)
            SceneManager.LoadScene(currentBuildIndex - 1);
    }
}
