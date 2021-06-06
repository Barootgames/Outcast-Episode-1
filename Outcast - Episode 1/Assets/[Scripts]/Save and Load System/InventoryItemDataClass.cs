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
    public string ShortInfo;

    [TextArea(2, 5)]
    public string MainInfo;

    public Sprite ItemSprite;
    public Sprite BackDocument;

    public bool hasViewed;
}
