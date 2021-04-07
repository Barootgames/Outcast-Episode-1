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

    public Dictionary<string, bool> GameEvents;

    public bool[] steps = new bool[70];

    public int missionIndex;

    public GameDataBinary(GameData data)
    {
        CurrentSceneName = data.CurrentSceneName;
        RespawnPoint = data.RespawnPoint;
        FaceRight = data.FaceRight;

        InventoryItemIds = data.GetInventoryItemIds();

        GameEvents = new Dictionary<string, bool>();

        foreach(GameEventDataClass gameEvent in data.gameEvents)
        {
            GameEvents.Add(gameEvent.EventName, gameEvent.isFinished);
        }

        missionIndex = data.currentMissionIndex;

        for(int i = 0; i < data.steps.Length; i++)
        {
            steps[i] = data.steps[i];
        }
    }
}

