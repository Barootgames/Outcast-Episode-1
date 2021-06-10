using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoaderController : MonoBehaviour
{
    GameDataController dataController;
    // Start is called before the first frame update
    void Start()
    {
        dataController = FindObjectOfType<GameDataController>();
        GameDataBinary data = SaveAndLoadSystem.LoadGame();
        dataController.gameData.LoadFromGameDataBinary(data);
    }
}
