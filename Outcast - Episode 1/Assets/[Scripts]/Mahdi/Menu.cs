using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    [SerializeField] private GameObject LoadingPanel;
    [SerializeField] private Slider loadingBar;

    [SerializeField] private GameObject Summary;

    [SerializeField] private bool isOnSummary;

    [SerializeField] private GameObject RedLabel;
    [SerializeField] private GameObject RedLabelSelf;
    [SerializeField] private GameObject SummaryTitle;
    [SerializeField] private float SummaryTitleDefaultPosX;
    [SerializeField] private float SummaryTitleSelectedPosX;

    [SerializeField] private GameObject FirstTitle;
    [SerializeField] private float FirstTitleDefaultPosX;
    [SerializeField] private float FirstTitleSelectedPosX;

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    public void buttonQuit()
    {
        OnDataSummaryOff();
        Application.Quit();
    }

    public void OnDataSummary()
    {
        if (!Summary.activeInHierarchy)
        {
            Summary.SetActive(true);
        }
        if (!isOnSummary)
        {
            RedLabel.SetActive(false);
            RedLabelSelf.SetActive(true);
            SummaryTitle.GetComponent<RectTransform>().anchoredPosition = new Vector2(SummaryTitleSelectedPosX, SummaryTitle.GetComponent<RectTransform>().anchoredPosition.y);
            FirstTitle.GetComponent<RectTransform>().anchoredPosition = new Vector2(FirstTitleSelectedPosX, FirstTitle.GetComponent<RectTransform>().anchoredPosition.y);
        }
        Summary.GetComponent<Animator>().SetBool("fade", true);
        isOnSummary = true;
    }

    public void OnDataSummaryOff()
    {
        if (isOnSummary)
        {
            Summary.GetComponent<Animator>().SetBool("fade", false);
            RedLabel.SetActive(true);
            RedLabelSelf.SetActive(false);
            SummaryTitle.GetComponent<RectTransform>().anchoredPosition = new Vector2(SummaryTitleDefaultPosX, SummaryTitle.GetComponent<RectTransform>().anchoredPosition.y);
            FirstTitle.GetComponent<RectTransform>().anchoredPosition = new Vector2(FirstTitleDefaultPosX, FirstTitle.GetComponent<RectTransform>().anchoredPosition.y);
        }
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
