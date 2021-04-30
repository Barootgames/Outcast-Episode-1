using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionOff : MonoBehaviour
{

   public GameDataController gamedata;

    public void Awake()
    {
        gamedata = GameObject.FindObjectOfType<GameDataController>();
    }

    public void OnEnable()
    {
        gamedata.gameData.isOnCanvas = true;
    }

    public void OnDisable()
    {
        gamedata.gameData.isOnCanvas = false;
    }
}
