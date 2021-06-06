using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataReload : MonoBehaviour
{
    public GameData from;
    public GameData to;
    // Start is called before the first frame update
    void Start()
    {
        to.inventoryItems = from.inventoryItems;
        to.steps = from.steps;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
