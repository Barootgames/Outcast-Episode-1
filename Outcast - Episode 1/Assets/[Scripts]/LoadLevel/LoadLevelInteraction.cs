using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadLevelInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    public LayerMask whatIsInteractable;
    public GameObject nextSceneAlert;
    public bool canGoNextScene = false;

    public LoadLevelController loadLevelControllerPrefab;
    public string textInfo;
    public Text textInfoText;
    public string nextSceneName;
    public bool buttonInteraction;
    public ConditionObject condition;
    public bool hasCondition;

    AudioSource audioSource;

    public AudioClip audioClipOK;
    public AudioClip audioClipNOK;

    public int respawnLocationIndex;
    public bool faceRightAfterLoad;


    void Start()
    {
        LoadLevelController loadLevel = FindObjectOfType<LoadLevelController>();
        if(loadLevel == null)
        {
            Instantiate(loadLevelControllerPrefab);
        }

        if (!buttonInteraction)
        {
            nextSceneAlert.GetComponent<Button>().enabled = false;
        }
        else
        {
            nextSceneAlert.GetComponent<Button>().enabled = true;
        }

        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.clip = audioClipOK;
    }

    // Update is called once per frame
    void Update()
    {
        if (canGoNextScene && !buttonInteraction)
        {
            CheckForClick();
            CheckForTouch();
        }
        nextSceneAlert.SetActive(canGoNextScene);
    }

    void CheckForClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero, 0f, whatIsInteractable);
            if (hit)
            {
                if (hit.collider.gameObject.name.Equals(gameObject.name))
                    Interact();
            }
        }
    }

    void CheckForTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Handle finger movements based on TouchPhase
            switch (touch.phase)
            {
                //When a touch has first been detected, change the message and record the starting position
                case TouchPhase.Began:
                    Vector2 origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero, 0f, whatIsInteractable);
                    if (hit)
                    {
                        if (hit.collider.gameObject.name.Equals(gameObject.name))
                            Interact();
                    }
                    break;

                //Determine if the touch is a moving touch
                case TouchPhase.Moved:
                    break;

                case TouchPhase.Ended:
                    break;
            }
        }
    }

    public void Interact()
    {
        if (CheckCondition())
        {
            if(SceneManager.GetActiveScene().buildIndex == 3)
            {
                if(nextSceneName == "Scene 4 VIP Room")
                {
                    if (!GameObject.FindObjectOfType<Step>().Steps[12])
                                   return;





                }
            }


            audioSource.clip = audioClipOK;
            audioSource.Play();
            LoadLevelController.faceRightAfterLoad = faceRightAfterLoad;
            print(LoadLevelController.faceRightAfterLoad);
            LoadLevelController.respawnLocationIndex = respawnLocationIndex;
            LoadLevelController.loadLevelController.LoadSceneAsync(nextSceneName);
        }
        else
        {
            audioSource.clip = audioClipNOK;
            audioSource.Play();
            textInfoText.text = textInfo;
        }
    }

    bool CheckCondition()
    {
        bool ok = true;
        if (hasCondition)
        {
            if (condition != null)
            {
                for(int i = 0; i < condition.conditions.Length; i++)
                {
                    ok = ok && condition.conditions[i].OK;
                }
                return ok;
            }
            return true;
        }
        else
            return true;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (!canGoNextScene)
            {
                canGoNextScene = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (canGoNextScene)
            {
                canGoNextScene = false;
            }
        }
    }
}
