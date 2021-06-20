using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelController : MonoBehaviour
{
    public static LoadLevelController loadLevelController;

    public static int respawnLocationIndex = -1;
    public static bool faceRightAfterLoad = true;

    private Animator quickFade;
    static bool RunNow = false;
   
    void Awake()
    {
        if(loadLevelController == null)
        {
            loadLevelController = this;
        }
        DontDestroyOnLoad(gameObject);

        if(quickFade == null)
          quickFade = GameObject.Find("QuickFade").transform.GetChild(0).GetComponent<Animator>();


        if(!RunNow)
        {
            RunNow = true;

            if (!quickFade.gameObject.activeInHierarchy)
                quickFade.gameObject.SetActive(true);


            quickFade.Play("QuickFadeOut");

            StartCoroutine(PanelFadeClosed());
        }
    }

    private void OnLevelWasLoaded()
    {
        if (quickFade == null)
            quickFade = GameObject.Find("QuickFade").transform.GetChild(0).GetComponent<Animator>();


        if (!quickFade.gameObject.activeInHierarchy)
              quickFade.gameObject.SetActive(true);


        quickFade.Play("QuickFadeOut");

        StartCoroutine(PanelFadeClosed());
    }

    IEnumerator PanelFadeClosed ()
    {
        yield return new WaitForSeconds(1);
        quickFade.gameObject.SetActive(false);
    }


    public void LoadSceneAsync(string nextSceneName)
    {
        if(!quickFade.gameObject.activeInHierarchy)
                quickFade.gameObject.SetActive(true);
        quickFade.Play("QuickFadeIn");
        StartCoroutine(WaitForFadeIn(1, nextSceneName));
    }

    IEnumerator WaitForFadeIn (float WaitTime , string _name)
    {
        yield return new WaitForSeconds(WaitTime);
        SceneManager.LoadSceneAsync(_name);
    }

}
