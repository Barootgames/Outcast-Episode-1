using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionControllerItemActivate : MonoBehaviour
{
    public GameObject item;
    public LayerMask whatIsInteractable;
    public Collider2D m_collider2D;
    public AudioClip audioClip;
    public GameObject interactableIcon;

    public bool isAudio = true;

    public bool isInTrigger;

    AudioSource audioSource;

    bool canClick = true;

    public Sprite imageSprite;

    public bool isRiddle;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioClip != null)
        {
            audioSource.clip = audioClip;
            audioSource.loop = false;
            audioSource.playOnAwake = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (isInTrigger && canClick)
        {
            CheckForClick();
            CheckForTouch();
        }
        interactableIcon.SetActive(isInTrigger);
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
                {
                    Interact();
                }

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
                        {
                            Interact();
                        }

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
        if (isAudio)
        {
            audioSource.Play();
        }
        if(!isRiddle)
            item.GetComponent<Image>().sprite = imageSprite;
        item.SetActive(true);
        if(isRiddle)
            FindObjectOfType<GameDataController>().gameData.isOnCanvas = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            isInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            isInTrigger = false;
        }
    }
}
