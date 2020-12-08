using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Line
{
    public Character character;

    [TextArea(2, 5)]
    public string text;

    public Choice[] choices;

    public bool hasRecursion;
    public int recursiveIndex;

}

[System.Serializable]
public struct Choice {

    [TextArea(2, 5)]
    public string choice;

    public int lineIndex;

    public bool isMandatory;

    public bool hasViewed;

    public ConditionObject conditions;
}


[CreateAssetMenu(fileName = "NewConversation", menuName = "Dialogue System/Conversation")]
public class ConversationObject : ScriptableObject
{
    public Character leftCharacter;
    public Character rightCharacter;

    public Line[] lines;

}
