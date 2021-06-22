using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextSceneWithTransition : MonoBehaviour
{
    public Animator SceneTransitionAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene()
    {
        StartCoroutine(LoadSceneWithTransition(SceneManager.GetActiveScene().buildIndex + 1));
    }
    IEnumerator LoadSceneWithTransition(int sceneIndex)
    {
        yield return new WaitForSeconds(1f);
        SceneTransitionAnimator.SetTrigger("transit");
        yield return new WaitForSeconds(1f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            yield return null;
        }
    }
}
