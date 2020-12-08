using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Condition
{
    [TextArea(1,3)]
    public string title;
    public bool OK;
}

[CreateAssetMenu(fileName = "NewCondition", menuName = "Dialogue System/Condition")]
public class ConditionObject : ScriptableObject
{
    public Condition[] conditions;
}
