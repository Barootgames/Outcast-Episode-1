﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;

public class GameDataController : MonoBehaviour
{
    public GameData gameData;
    public static GameDataController instance = null;

    InventoryManger inventoryManger;
    
    void Start()
    {
        inventoryManger = FindObjectOfType<InventoryManger>();
        if(instance == null)
        {
            instance = this;
        }

        if (gameData != null)
        {
            foreach (string id in gameData.itemIds)
            {
                instance.gameData.AddItem(id);
                foreach (InventoryItemDataClass item in gameData.inventoryItems)
                {
                    if (item.ItemId.Equals(id) && inventoryManger)
                    {
                        if (!item.isDocument)
                            FindObjectOfType<InventoryManger>().AddItemFromLoad(item.ItemName, item.ItemSprite);
                        else
                            FindObjectOfType<InventoryManger>().AddDocumentFromLoad(item.ItemSprite,item.BackDocument,item.DocumentName, item.ShortInfo,item.MainInfo);
                    }
                }
            }
        }

        if (!SceneManager.GetActiveScene().name.Contains("Menu"))
        {
            if (PlayerPrefs.HasKey("bright"))
            {
                LightController.GlobalIntesity = PlayerPrefs.GetFloat("bright");
            }

            Light2D[] lights = FindObjectsOfType<Light2D>();
            float ratio = LightController.GlobalIntesity / LightController.DefaultGlobalIntesity;

            foreach (Light2D light in lights)
            {
                if (light.lightType == Light2D.LightType.Global)
                {
                    light.intensity *= ratio;
                }
            }

            foreach (Light2D light in lights)
            {
                if (light.lightType == Light2D.LightType.Point)
                {
                    light.intensity *= ratio;
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
    }
}
