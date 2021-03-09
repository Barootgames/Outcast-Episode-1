using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManger : MonoBehaviour
{ 
    private int numberItemInInvenory = 0;
    private int numberDocInInventory = 0;
    private string [] inventory = new string[6];
    [SerializeField] private Image [] slots;
    [SerializeField] public string item_drag_name;
    [SerializeField] public string item_drop_name;
    [SerializeField] private Color empty_color;
    [SerializeField] private Color normal_color;
    [SerializeField] private CombinItems[] _combinItems;
    [SerializeField] private GameObject info;

    // document
    private Document [] documents = new Document[200];
    private int PageInDoc = 0;
   
    
    public void AddItem (string itemName,Sprite itemImage)
    {

        if (numberItemInInvenory >= 6)
        {
            // inventory is full
            return;
        }
        
        // add to inventory
        
        inventory[numberItemInInvenory] = itemName;
        slots[numberItemInInvenory].name = itemName;
        slots[numberItemInInvenory].sprite = itemImage;
        slots[numberItemInInvenory].transform.parent.name = itemName;
        slots[numberItemInInvenory].color = normal_color;

        numberItemInInvenory++;

        GameDataController.instance.gameData.AddItem(itemName);
    }

    public void AddItemFromLoad(string itemName, Sprite itemImage)
    {

        if (numberItemInInvenory >= 6)
        {
            // inventory is full
            return;
        }

        // add to inventory

        inventory[numberItemInInvenory] = itemName;
        slots[numberItemInInvenory].name = itemName;
        slots[numberItemInInvenory].sprite = itemImage;
        slots[numberItemInInvenory].transform.parent.name = itemName;
        slots[numberItemInInvenory].color = normal_color;

        numberItemInInvenory++;
    }

    public void RemoveItem (string itemName)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == itemName)
            {
                inventory[i] = null;
                slots[i].sprite = null;
                slots[i].color = empty_color;
                slots[i].transform.parent.name = null;
                slots[i].name = null;
            }
        }
        GameDataController.instance.gameData.RemoveItem(itemName);
    }

    public void AddDocument(Sprite docImage, string docTitle , string docInfo)
    {
        numberDocInInventory++;
        documents[numberDocInInventory].infoDocument = docInfo;
        documents[numberDocInInventory].nameDocument = docTitle;
        documents[numberDocInInventory].ImageDocument = docImage;

        GameDataController.instance.gameData.AddItem(docTitle);

        DocumentShow();
    }

    public void AddDocumentFromLoad(Sprite docImage, string docTitle, string docInfo)
    {
        numberDocInInventory++;
        documents[numberDocInInventory].infoDocument = docInfo;
        documents[numberDocInInventory].nameDocument = docTitle;
        documents[numberDocInInventory].ImageDocument = docImage;

        DocumentShow();
    }

    public void TryToCombin()
    {
        for (int i = 0; i < _combinItems.Length; i++)
        {
            if (item_drag_name == _combinItems[i].item1.name && item_drop_name == _combinItems[i].item2.name)
            {
                RemoveItem(_combinItems[i].item1.name);
                RemoveItem(_combinItems[i].item2.name);
                AddItem(_combinItems[i].result.name,_combinItems[i].result);
                GameDataController.instance.gameData.Combine(_combinItems[i].item1.name, _combinItems[i].item2.name, _combinItems[i].result.name);
            }
            
            if (item_drag_name == _combinItems[i].item2.name && item_drop_name == _combinItems[i].item1.name)
            {
                RemoveItem(_combinItems[i].item1.name);
                RemoveItem(_combinItems[i].item2.name);
                AddItem(_combinItems[i].result.name,_combinItems[i].result);
                GameDataController.instance.gameData.Combine(_combinItems[i].item1.name, _combinItems[i].item2.name, _combinItems[i].result.name);
            }

            #region Special

            if(item_drag_name == "Fuse2" && item_drop_name == "FusePlace")
            {
                RemoveItem("Fuse2");
                GameObject.FindObjectOfType<Step>().DoWork(9);
                GameObject.FindObjectOfType<Scene2>().FuseCheck();
            }

            if(item_drag_name == "KeyArtanRoom" && item_drop_name == "Door4VIP")
            {
                RemoveItem("KeyArtanRoom");
                GameObject.FindObjectOfType<Step>().DoWork(12);
            }
  
            #endregion
        }
    }
    
    public void inventoryBtn()
    {
        transform.GetChild(0).gameObject.SetActive(!transform.GetChild(0).gameObject.activeInHierarchy);
        transform.GetChild(2).gameObject.SetActive(!transform.GetChild(2).gameObject.activeInHierarchy);
        if (!transform.GetChild(0).gameObject.activeInHierarchy)
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }
    
    public void infoBtn()
    {
        transform.GetChild(1).gameObject.SetActive(!transform.GetChild(1).gameObject.activeInHierarchy);
    }

    public void nextBtn()
    {
        if (PageInDoc < numberDocInInventory)
        {
            PageInDoc++;
        }
        
        DocumentShow();
    }

    public void backBtn()
    {
        if (PageInDoc > 1)
        {
            PageInDoc--;
        }
        
        DocumentShow();
    }

    private void DocumentShow()
    {
        if (numberDocInInventory == 0)
        {
            
        }
        else if (numberDocInInventory == 1)
        {
            PageInDoc = 1;
            info.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = documents[PageInDoc].infoDocument;
            info.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = documents[PageInDoc].ImageDocument;
            info.transform.GetChild(4).GetComponent<Text>().text = documents[PageInDoc].nameDocument;
            info.transform.GetChild(3).GetComponent<Text>().text = PageInDoc + "/" + numberDocInInventory;
        }
        else
        {
            info.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = documents[PageInDoc].infoDocument;
            info.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = documents[PageInDoc].ImageDocument;
            info.transform.GetChild(4).GetComponent<Text>().text = documents[PageInDoc].nameDocument;
            info.transform.GetChild(3).GetComponent<Text>().text = PageInDoc + "/" + numberDocInInventory;
        } 
        
        
    }
    
}

[Serializable] public struct CombinItems
{
    public Sprite item1;
    public Sprite item2;
    public Sprite result;
}

[Serializable] public  struct Document
{
    public string nameDocument;
    public Sprite ImageDocument;
    [TextArea] public string infoDocument;
}

