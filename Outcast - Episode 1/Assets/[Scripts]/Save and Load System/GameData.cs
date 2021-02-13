using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Game Data/GameData")]
public class GameData : ScriptableObject
{
    public string CurrentSceneName;
    public int RespawnPoint;
    public bool FaceRight;

    public List<InventoryItemDataClass> inventoryItems = new List<InventoryItemDataClass>();

    public List<string> itemIds = new List<string>();

    public List<string> GetInventoryItemIds()
    {
        return itemIds;
    }

    public void AddItem(string ItemId)
    {
        int count = 0;
        foreach(string itemId in itemIds)
        {
            if (!itemId.Equals(ItemId))
            {
                count++;
            }
        }
        if(count == itemIds.Count)
        {
            itemIds.Add(ItemId);
        }

        SaveAndLoadSystem.SaveGame(this);
    }

    public void RemoveItem(string ItemId)
    {
        itemIds.Remove(ItemId);
    }

    public void Combine(string ItemId1, string ItemId2, string result)
    {
        itemIds.Remove(ItemId1);
        itemIds.Remove(ItemId2);
        itemIds.Add(result);
    }
}
