using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    [SerializeField] private GameObject LoadingPanel;
    [SerializeField] private Slider loadingBar;

    [SerializeField] private GameObject Summary;

    [SerializeField] private GameObject NewGame;
    [SerializeField] private GameObject Continue;
    [SerializeField] private GameObject StartGame;

    [SerializeField] private bool isOnSummary;

    [SerializeField] private Camera _mCamera;
    [SerializeField] private float _mCameraPos;
    [SerializeField] private float _mCameraPosY;
    [SerializeField] private float _mCameraLerpDuration;

    private bool cameraLerp = false;
    private int cameraLerpDircetion = 0; //0 idle, 1 right, -1 left;
    private int cameraLerpDircetionY = 0; //0 idle, 1 Up, -1 Down;

    public float timer = 0;



    void Start()
    {
        ConvertAspectRatio();
    }
    
    void Update()
    {

    }

    public void buttonQuit()
    {
        OnDataSummaryOff();
        Application.Quit();
    }

    void ConvertAspectRatio()
    {
        float aspectRatioDesign = (16f / 10f);
        float orthographicStartSize = 1205f;

        float inverseAspectRatio = 1 / aspectRatioDesign;
        float currentAspectRatio = (float)Screen.width / (float)Screen.height;

        _mCamera.orthographicSize = aspectRatioDesign * (orthographicStartSize / currentAspectRatio);
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

    public void OnNewGame()
    {
        GameDataController gameDataController = FindObjectOfType<GameDataController>();
        gameDataController.gameData.ResetGameData();
        SaveAndLoadSystem.SaveGame(gameDataController.gameData);
        StartCoroutine(LoadScene(gameDataController.gameData.CurrentSceneName));
    }
    public void buttonStart(int SceneIndex)
    {
        GameDataBinary data = SaveAndLoadSystem.LoadGame();
        GameDataController gameDataController = FindObjectOfType<GameDataController>();
        gameDataController.gameData.LoadFromGameDataBinary(data);
        StartCoroutine(LoadScene(gameDataController.gameData.CurrentSceneName));
    }

    public void OnCredits()
    {
        if (_mCamera.transform.position.x == 0)
        {
            if (!cameraLerp)
            {
                OnDataSummaryOff();
                cameraLerp = true;
                cameraLerpDircetion = 0;
                cameraLerpDircetionY = -1;
                StopCoroutine(CameraLerp());
                StartCoroutine(CameraLerp());
            }
        }
    }

    public void OnGallery()
    {
        if (_mCamera.transform.position.x == 0)
        {
            if (!cameraLerp)
            {
                OnDataSummaryOff();
                cameraLerp = true;
                cameraLerpDircetion = 1;
                cameraLerpDircetionY = 0;
                StopCoroutine(CameraLerp());
                StartCoroutine(CameraLerp());
            }
        }
    }

    public void OnGalleryOff()
    {
        if (_mCamera.transform.position.x == 0)
        {
            if (!cameraLerp)
            {
                OnDataSummaryOff();
                cameraLerp = true;
                cameraLerpDircetion = -1;
                cameraLerpDircetionY = 0;
                StopCoroutine(CameraLerp());
                StartCoroutine(CameraLerp());
            }
        }
    }

    public void OnTutorial()
    {
        if (_mCamera.transform.position.x == 0)
        {
            if (!cameraLerp)
            {
                OnDataSummaryOff();
                cameraLerp = true;
                cameraLerpDircetion = -1;
                cameraLerpDircetionY = 0;
                StopCoroutine(CameraLerp());
                StartCoroutine(CameraLerp());
            }
        }
    }

    public void OnTutorialOff()
    {
        if (_mCamera.transform.position.x == 0)
        {
            if (!cameraLerp)
            {
                OnDataSummaryOff();
                cameraLerp = true;
                cameraLerpDircetion = 1;
                cameraLerpDircetionY = 0;
                StopCoroutine(CameraLerp());
                StartCoroutine(CameraLerp());
            }
        }
    }

    public void OnGalleryAndTutorial()
    {
        if (_mCamera.transform.position.x == 0)
        {
            if (!cameraLerp)
            {
                OnDataSummaryOff();
                cameraLerp = true;
                cameraLerpDircetion = 0;
                cameraLerpDircetionY = 1;
                StopCoroutine(CameraLerp());
                StartCoroutine(CameraLerp());
            }
        }
    }

    public void OnGalleryAndTutorialOff()
    {
        if (_mCamera.transform.position.x == 0)
        {
            if (!cameraLerp)
            {
                OnDataSummaryOff();
                cameraLerp = true;
                cameraLerpDircetion = 0;
                cameraLerpDircetionY = -1;
                StopCoroutine(CameraLerp());
                StartCoroutine(CameraLerp());
            }
        }
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
                cameraLerpDircetionY = 0;
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
        Vector3 end = new Vector3(_mCamera.transform.position.x + (cameraLerpDircetion * _mCameraPos), _mCamera.transform.position.y + (cameraLerpDircetionY * _mCameraPosY), _mCamera.transform.position.z);
        while (timer < _mCameraLerpDuration)
        {
            timer += Time.fixedDeltaTime;
            _mCamera.transform.position = Vector3.Lerp(_mCamera.transform.position, end, timer/_mCameraLerpDuration);
            yield return null;
        }
        cameraLerp = false;
    }
}
