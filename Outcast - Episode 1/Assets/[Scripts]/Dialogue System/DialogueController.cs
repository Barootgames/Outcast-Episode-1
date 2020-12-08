using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    public ConversationObject introConversation;
    public ConversationObject mainConversation;
    public ConversationObject exitConversation;

    public ConversationObject conversation;

    public GameObject ChoicePrefab;

    public Transform ChoiceHolder;

    public RawImage LeftCharacterImage;
    public RawImage RightCharacterImage;

    public Image LeftCharacterImage1;
    public Image RightCharacterImage1;

    public Text LeftPersonName;
    public Text RightPersonName;

    public Text DialogueText;

    public int lineIndex = 0;

    public Button next;

    public List<int> uncheckedLines = new List<int>();

    public Text characterName;

    public string currentLineName;

    public Transform[] choiceButtons;

    public Sprite BlackSprite;
    public Sprite RedSprite;

    DialogueInteraction dialogueInteraction;

    int conversationIndex = 0;
    
    // Start is called before the first frame update
    void Start()
    {;
        conversation = introConversation;
        lineIndex = -1;
        UpdateName();
        NextLine();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetDialogue()
    {
        lineIndex = -1;
        currentLineName = "";
        uncheckedLines.Clear();
        UpdateName();
        NextLine();
    }

    public void NextLine()
    {
        lineIndex++;
        if (conversation.lines.Length > lineIndex && !uncheckedLines.Contains(lineIndex))
        {
            DialogueText.text = conversation.lines[lineIndex].text;
            UpdateName();

            if (conversation.lines[lineIndex].choices != null && conversation.lines[lineIndex].choices.Length > 0)
            {
                CLearChildren();
                for (int i = 0; i < conversation.lines[lineIndex].choices.Length; i++)
                {
                    if (CheckConditions(conversation.lines[lineIndex].choices[i]))
                    {
                        Choice _choice = conversation.lines[lineIndex].choices[i];
                        if (!_choice.hasViewed)
                        {
                            GameObject choice = Instantiate(ChoicePrefab.gameObject, ChoiceHolder);
                            if (_choice.isMandatory)
                            {
                                choice.GetComponent<Image>().sprite = RedSprite;
                            }
                            else
                            {
                                choice.GetComponent<Image>().sprite = BlackSprite;
                            }
                            choice.transform.position = choiceButtons[i].position;
                            choice.GetComponentInChildren<Text>().text = conversation.lines[lineIndex].choices[i].choice;
                            int nextLineIndex = conversation.lines[lineIndex].choices[i].lineIndex;
                            int choiceIndex = i;
                            choice.GetComponent<Button>().onClick.AddListener(delegate ()
                            {
                                conversation.lines[lineIndex].choices[choiceIndex].hasViewed = true;
                                for (int j = 0; j < conversation.lines[lineIndex].choices.Length; j++)
                                {
                                    if (conversation.lines[lineIndex].choices[j].lineIndex != nextLineIndex)
                                    {
                                        uncheckedLines.Add(conversation.lines[lineIndex].choices[j].lineIndex);
                                    }
                                }
                                SelectChoice(nextLineIndex);
                            });
                        }
                    }
                }
                //next.gameObject.SetActive(false);
            }
            else
            {
                if (conversation.lines[lineIndex].hasRecursion)
                {
                    print("recursiove");
                    lineIndex = conversation.lines[lineIndex].recursiveIndex - 1;
                    uncheckedLines.Clear();
                }
                CLearChildren();
            }
        }
        else if(conversation.lines.Length <= lineIndex)
        {
            conversationIndex = (conversationIndex + 1) % 3;
            if(conversationIndex == 1)
            {
                lineIndex = -1;
                conversation = mainConversation;
                uncheckedLines.Clear();
                NextLine();
            }
            else if(conversationIndex == 2)
            {
                lineIndex = -1;
                conversation = exitConversation;
                uncheckedLines.Clear();
                NextLine();
            }
            else
                next.gameObject.SetActive(false);
        }
    }

    bool CheckConditions(Choice choice)
    {
        if (choice.conditions != null)
        {
            bool ok = true;
            for(int i = 0; i < choice.conditions.conditions.Length; i++)
            {
                if (choice.conditions.conditions[i].OK)
                {
                    ok = ok && true;
                }
                else
                {
                    ok = ok && false;
                }
            }
            return ok;
        }
        return true;
    }

    void UpdateName()
    {
        /*
        if (!currentLineName.Equals(conversation.lines[lineIndex].character.fullName))
        {
            CharacterImage.sprite = conversation.lines[lineIndex].character.characterIcon;
            currentLineName = conversation.lines[lineIndex].character.fullName;
        }
        characterName.text = currentLineName;
        */
        LeftPersonName.text = conversation.leftCharacter.name;
        RightPersonName.text = conversation.rightCharacter.name;

        //LeftCharacterImage.texture = conversation.leftCharacter.characterRenderTexture;
        //RightCharacterImage.texture = conversation.rightCharacter.characterRenderTexture;
    }

    void CLearChildren()
    {
        for (int i = 0; i < ChoiceHolder.childCount; i++)
        {
            Destroy(ChoiceHolder.GetChild(i).gameObject);
        }
    }

    public void SelectChoice(int lineIndex)
    {
        this.lineIndex = lineIndex - 1;
        NextLine();
        next.gameObject.SetActive(true);
    }

    public void SetDialogueInteraction(DialogueInteraction interaction)
    {
        dialogueInteraction = interaction;
    }

    public void CloseDialogue()
    {
        dialogueInteraction.OnDialogueEnded();
        gameObject.SetActive(false);
    }
}
