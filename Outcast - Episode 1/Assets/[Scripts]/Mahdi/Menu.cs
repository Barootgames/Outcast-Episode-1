using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    [SerializeField] private GameObject LoadingPanel;
    [SerializeField] private Slider loadingBar;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    public void buttonQuit()
    {
        Application.Quit();
    }

    public void buttonStart(int SceneIndex)
    {
        GameDataBinary data = SaveAndLoadSystem.LoadGame();
        GameDataController gameDataController = FindObjectOfType<GameDataController>();
        gameDataController.gameData.LoadFromGameDataBinary(data);
        StartCoroutine(LoadScene(gameDataController.gameData.CurrentSceneName));
    }

    IEnumerator LoadScene (int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        LoadingPanel.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadingBar.value = progress;
            yield return null;
        }
    }

    IEnumerator LoadScene(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        LoadingPanel.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadingBar.value = progress;
            yield return null;
        }
    }
}
