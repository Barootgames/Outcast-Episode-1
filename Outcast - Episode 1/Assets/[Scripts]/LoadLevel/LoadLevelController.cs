using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelController : MonoBehaviour
{

    public static LoadLevelController loadLevelController;

    public static int respawnLocationIndex = -1;
    public static bool faceRightAfterLoad = true;
    // Start is called before the first frame update
    void Start()
    {
        if(loadLevelController == null)
        {
            loadLevelController = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadSceneAsync(string nextSceneName)
    {
        SceneManager.LoadSceneAsync(nextSceneName);
    }
}
