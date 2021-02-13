using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataTest : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite spritetest;
    public bool add = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (add)
        {
            GameDataController.instance.gameData.AddItem("Item1");
            add = false;
        }
    }
}
