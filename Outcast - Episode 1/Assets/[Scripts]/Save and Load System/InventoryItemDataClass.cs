using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItemDataClass
{
    public string ItemId;

    [TextArea(2, 5)]
    public string ItemName;

    public bool isDocument;
    public string DocumentName;

    [TextArea(2, 5)]
    public string Info;


    public Sprite ItemSprite;

    public bool hasViewed;
}
