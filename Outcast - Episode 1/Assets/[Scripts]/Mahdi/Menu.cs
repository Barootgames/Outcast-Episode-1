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

    [SerializeField] private Camera _mCamera;
    [SerializeField] private float _mCameraPos;
    [SerializeField] private float _mCameraLerpDuration;

    private bool cameraLerp = false;
    private int cameraLerpDircetion = 0; //0 idle, 1 right, -1 left;

    public float timer = 0;



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
        Summary.GetComponent<Animator>().SetBool("fade", true);
        isOnSummary = true;
    }

    public void OnDataSummaryOff()
    {
        if (isOnSummary)
        {
            Summary.GetComponent<Animator>().SetBool("fade", false);
        }
    }

    public void buttonStart(int SceneIndex)
    {
        GameDataBinary data = SaveAndLoadSystem.LoadGame();
        GameDataController gameDataController = FindObjectOfType<GameDataController>();
        gameDataController.gameData.LoadFromGameDataBinary(data);
        StartCoroutine(LoadScene(gameDataController.gameData.CurrentSceneName));
    }

    public void OnSettings()
    {
        if(_mCamera.transform.position.x == 0)
        {
            if (!cameraLerp)
            {
                OnDataSummaryOff();
                cameraLerp = true;
                cameraLerpDircetion = 1;
                StopCoroutine(CameraLerp());
                StartCoroutine(CameraLerp());
            }
        }
    }

    public void OnSettingsOff()
    {
        if (Mathf.Abs(_mCamera.transform.position.x) == _mCameraPos)
        {
            if (!cameraLerp)
            {
                cameraLerp = true;
                cameraLerpDircetion = -1;
                StopCoroutine(CameraLerp());
                StartCoroutine(CameraLerp());
            }
        }
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

    IEnumerator CameraLerp()
    {
        timer = 0;
        Vector3 end = new Vector3(_mCamera.transform.position.x + (cameraLerpDircetion * _mCameraPos), _mCamera.transform.position.y, _mCamera.transform.position.z);
        while (timer < _mCameraLerpDuration)
        {
            timer += Time.fixedDeltaTime;
            _mCamera.transform.position = Vector3.Lerp(_mCamera.transform.position, end, timer/_mCameraLerpDuration);
            yield return null;
        }
        cameraLerp = false;
    }
}
