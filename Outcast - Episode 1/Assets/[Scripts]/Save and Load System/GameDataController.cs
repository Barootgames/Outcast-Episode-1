using UnityEngine;

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
        DontDestroyOnLoad(gameObject);

        GameDataBinary data = SaveAndLoadSystem.LoadGame();

        if (data != null)
        {
            foreach (string id in data.InventoryItemIds)
            {
                instance.gameData.AddItem(id);
                foreach (InventoryItemDataClass item in gameData.inventoryItems)
                {
                    if (item.ItemId.Equals(id) && inventoryManger)
                    {
                        if (!item.isDocument)
                            FindObjectOfType<InventoryManger>().AddItemFromLoad(item.ItemName, item.ItemSprite);
                        else
                            FindObjectOfType<InventoryManger>().AddDocumentFromLoad(item.ItemSprite, item.DocumentName, item.Info);
                    }
                }
            }
        }
       

        

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
