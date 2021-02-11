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
        StartCoroutine(LoadScene(SceneIndex));
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
}
