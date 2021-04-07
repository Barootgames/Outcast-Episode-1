using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventController : MonoBehaviour
{
    public string EventName;
    public int EventId;

    InteractionController interaction;
    InteractionControllerItemActivate interactionControllerItemActivate;
    GameDataController gameData;
    // Start is called before the first frame update
    void Start()
    {
        gameData = FindObjectOfType<GameDataController>();
        interaction = GetComponent<InteractionController>();
        interactionControllerItemActivate = GetComponent<InteractionControllerItemActivate>();

        foreach(GameEventDataClass gameEvent in gameData.gameData.gameEvents)
        {
            if(gameEvent.EventName.Equals(EventName) || gameEvent.EventId == EventId)
            {
                if (gameEvent.isFinished)
                {
                    if (interaction)
                    {
                        interaction.enabled = false;
                    }
                    if (interactionControllerItemActivate)
                    {
                        interactionControllerItemActivate.enabled = false;
                    }
                }
            }
        }
        SetGameEventAsFinished();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private GameEventDataClass FindGameEventByName()
    {
        foreach (GameEventDataClass gameEvent in gameData.gameData.gameEvents)
        {
            if (gameEvent.EventName.Equals(EventName))
            {
                return gameEvent;
            }
        }
        return null;
    }

    private GameEventDataClass FindGameEventById()
    {
        foreach (GameEventDataClass gameEvent in gameData.gameData.gameEvents)
        {
            if (gameEvent.EventName.Equals(EventId))
            {
                return gameEvent;
            }
        }
        return null;
    }

    public void SetGameEventAsFinished()
    {
        gameData.gameData.SetGameEventAsFinished(EventName);
    }
}
