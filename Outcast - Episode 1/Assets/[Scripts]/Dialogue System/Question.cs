using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuestion", menuName = "Dialogue System/Question")]
public class Question : ScriptableObject
{
    [TextArea(2, 5)]
    public string text;

    //public Choice[] choices;
}
