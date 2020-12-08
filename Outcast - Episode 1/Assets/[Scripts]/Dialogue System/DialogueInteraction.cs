using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteraction : MonoBehaviour
{
    public Transform playerDialogueStartTransform;

    public ConversationObject introConversation;
    public ConversationObject mainConversation;
    public ConversationObject exitConversation;

    public ConversationObject dialogue;

    bool dialogueCompleted = false;

    PlayerMovement playerMovement;
    CharacterController2D character;

    public DialogueController dialogueController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>() && collision.isTrigger)
        {
            dialogueController.conversation = introConversation;
            dialogueController.SetDialogueInteraction(this);
            dialogueController.gameObject.SetActive(true);
            dialogueController.ResetDialogue();
            playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
            character = collision.gameObject.GetComponent<CharacterController2D>();
            playerMovement.enabled = false;
            character.enabled = false;
            playerMovement.gameObject.GetComponent<Animator>().SetFloat("Speed", 0f);
        }
    }

    public void OnDialogueEnded()
    {
        playerMovement.enabled = true;
        character.enabled = true;
        dialogueCompleted = true;
    }
}
