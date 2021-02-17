using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameDataBinary
{
    public string CurrentSceneName;
    public int RespawnPoint;
    public bool FaceRight;

    public List<string> InventoryItemIds;

    public GameDataBinary(GameData data)
    {
        CurrentSceneName = data.CurrentSceneName;
        RespawnPoint = data.RespawnPoint;
        FaceRight = data.FaceRight;

        InventoryItemIds = data.GetInventoryItemIds();
    }
}

