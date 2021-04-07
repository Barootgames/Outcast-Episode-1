using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Step : MonoBehaviour
{

    public static bool[] _steps = new bool[70];   // in bayad  save  beshe
    [HideInInspector] public bool[] Steps = new bool [70];
    GameDataController gameData;

    // info
    /*
      // 0 = tutorail_walk  // 1 = tutorail_run
    // 2 = tutorail_interaction  // 3 = tutorail_rest
    // 4 = First thunder    // 5 = secend thunder
    // 6 = telephone_bird  // 7 = dialog_jamshid_1
    // 8 = pickup_fuse2   // 9 = fuse_in_place
    // 10 = button_fuse_on   // 11 = jamshid_dialog2
    // 12 = key_Used_room_4   13 sleep artan
    // 14 = wakeUp On Dream    15 = enterTrigger a
    // 16 = enterTrigger a    17 = enterTrigger c
    // 18 = touch T wall       19 = touch T elameyi
    // 20 = touch ToMagaseri    21 = pick up Tape
    // 22 = pickup BookR        23 = lightCoffeeShop
    // 24 pickUp Battery      25 = moamaie  key
    // 26 = moamaie control   27 = moamaie lock
    // 28 = control Battery full    29 = pickUp newspaper
    // 30 = dar pedram     31 = fall loster
    // 32 = safe door open   33 = pick zero key
    // 34 = open cabinet2 (WC)   35 = open cabinet


    */

    private void Awake()
    {
        gameData = FindObjectOfType<GameDataController>();
        for(int i = 0; i < gameData.gameData.steps.Length; i++)
        {
            _steps[i] = gameData.gameData.steps[i];
        }
        for (int i = 0; i < 50; i++)
        {
            Steps[i] = _steps[i];
        }
    }


    public void  DoWork (int element)
    {
        _steps[element] = true;
        Steps[element] = true;
        gameData.gameData.steps[element] = true;
        gameData.gameData.CurrentSceneName = SceneManager.GetActiveScene().name;
        SaveAndLoadSystem.SaveGame(gameData.gameData);
    }
}
