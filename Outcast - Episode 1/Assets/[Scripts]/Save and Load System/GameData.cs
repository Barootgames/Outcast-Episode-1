using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Game Data/GameData")]
public class GameData : ScriptableObject
{
    public string CurrentSceneName;
    public int RespawnPoint;
    public bool FaceRight;
    public bool isOnCanvas = false;

    public List<InventoryItemDataClass> inventoryItems = new List<InventoryItemDataClass>();
    public List<GameEventDataClass> gameEvents = new List<GameEventDataClass>();

    public List<string> itemIds = new List<string>();
    public List<string> missions = new List<string>();
    public string currentMission;
    public int currentMissionIndex;

    public bool[] steps = new bool[70];

    public List<string> GetInventoryItemIds()
    {
        return itemIds;
    }

    public void AddItem(string ItemId)
    {
        int count = 0;
        foreach (string itemId in itemIds)
        {
            if (!itemId.Equals(ItemId))
            {
                count++;
            }
        }
        if (count == itemIds.Count)
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

    public void SetGameEventAsFinished(string eventName)
    {
        foreach(GameEventDataClass gameEvent in gameEvents)
        {
            if (gameEvent.EventName.Equals(eventName))
            {
                gameEvent.isFinished = true;
                SaveAndLoadSystem.SaveGame(this);
                break;
            }
        }
    }

    public void LoadGameEventData(GameDataBinary data)
    {
        foreach(GameEventDataClass gameEvent in gameEvents)
        {
            gameEvent.isFinished = data.GameEvents[gameEvent.EventName];
        }
    }

    public void LoadFromGameDataBinary(GameDataBinary data)
    {
        CurrentSceneName = data.CurrentSceneName;
        RespawnPoint = data.RespawnPoint;
        FaceRight = data.FaceRight;
        for (int i = 0; i < data.steps.Length; i++)
        {
            steps[i] = data.steps[i];
            Step._steps[i] = data.steps[i];
        }
        itemIds = data.InventoryItemIds;
    }
}
