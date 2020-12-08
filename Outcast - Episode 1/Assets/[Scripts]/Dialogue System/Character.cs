using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Dialogue System/Character")]
public class Character : ScriptableObject
{
    public string fullName;
    public Sprite characterIcon;
    public RenderTexture characterRenderTexture;
}
