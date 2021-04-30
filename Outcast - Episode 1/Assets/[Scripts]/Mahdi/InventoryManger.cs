using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManger : MonoBehaviour
{
    [SerializeField] private AudioClip DocRotSound;
    [SerializeField] [Range(0, 1)] private float DocRotVolume;
    [SerializeField] private AudioClip PickNewItemSound;
    [SerializeField] [Range(0, 1)] private float PickNewItemVolume;


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
    private Document [] documents = new Document[100];
    private int PageInDoc = 0;
    [SerializeField] private Text NameDocShower;
    [SerializeField] private Text NameDocShower2;
    [SerializeField] private Text ShortInfoShower;
    [SerializeField] private Image ImageShower;
    [SerializeField] private Image ImageShower2;
    [SerializeField] private Text MainInfoShower;
    [SerializeField] private Text NumberShower;
    [SerializeField] private GameObject ReadButton;

    [SerializeField] private GameObject QI_Read;

    [SerializeField] private Documents[] AllDocument;
    [SerializeField] private AudioSource audioSource;


    private bool isFront = true;
    private bool isChangeImage = true;
    [SerializeField] private float Speed;
    [SerializeField] private GameObject NewItemPanel;
    [SerializeField] private Text NameItemNewShower;
    [SerializeField] private Image SpriteItemNewShower;
    [SerializeField] private Text ShortInfoNewShower;

    
    public void AddItem (string itemName,Sprite itemImage)
    {

        if (numberItemInInvenory >= 6)
        {
            // inventory is full
            return;
        }

        audioSource.clip = PickNewItemSound;
        audioSource.volume = PickNewItemVolume;
        audioSource.Play();

        PanelNew(itemName, itemImage, "", false);
       
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

    public void AddDocument(Sprite docImage, Sprite Backdoc , string docTitle , string docInfo,string MainInfo)
    {

        PanelNew(docTitle,docImage,docInfo,true);

        numberDocInInventory++;
        documents[numberDocInInventory].ShortInfo = docInfo;
        documents[numberDocInInventory].nameDocument = docTitle;
        documents[numberDocInInventory].ImageDocument = docImage;
        documents[numberDocInInventory].infoDocument = MainInfo;
        GameDataController.instance.gameData.AddItem(docTitle);
        documents[numberDocInInventory].BackDoc = Backdoc;

        if (numberDocInInventory == 1)
            PageInDoc = 1;

        DocumentShow();
    }

    public void AddDocumentFromLoad(Sprite docImage, Sprite Backdoc , string docTitle, string docInfo, string MainInfo)
    {
        numberDocInInventory++;
        documents[numberDocInInventory].infoDocument = docInfo;
        documents[numberDocInInventory].nameDocument = docTitle;
        documents[numberDocInInventory].ImageDocument = docImage;
        documents[numberDocInInventory].infoDocument = MainInfo;
        documents[numberDocInInventory].BackDoc = Backdoc;

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

            if (item_drag_name == "Tape" && item_drop_name == "BookR")
            {
                RemoveItem("Tape");
                RemoveItem("BookR");
                AddDocument(AllDocument[0].ImageDocument,AllDocument[0].BackDoc, "a", "a", "a");

            }

            #endregion
        }
    }

    public void SpecialCombin (int a)
    {

        if (a == 1)
        {
            RemoveItem("KeyArtanRoom");
            GameObject.FindObjectOfType<Step>().DoWork(12);
        }

        if (a == 2)
        {
            RemoveItem("BookR");
            RemoveItem("Tape");
            print("a");
        }

        if (a == 3)
        {
            RemoveItem("Battery");
            GameObject.FindObjectOfType<VIPDream>().ControlFully();   
        }

        if(a == 4)
        {
            RemoveItem("Zero Key");
            GameObject.FindObjectOfType<SFLong>().Door0OPen();
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
        if(info.activeInHierarchy)
        {
            info.SetActive(false);
        }
        else
        {
            info.SetActive(true);
            DocumentShow();
        }
    }

    public void Read ()
    {
        QI_Read.SetActive(true);
    }

    public void Flip ()
    {
        float Angle = ImageShower.transform.eulerAngles.y;

        if( (Angle <= 0.1f && Angle >= -0.1f ) || (Angle <= 180.1f && Angle >= 179.9f))
        {
            audioSource.clip = DocRotSound;
            audioSource.volume = DocRotVolume;
            audioSource.Play();
            isFront = !isFront;
            isChangeImage = false;
        }
    }

    public void CloseRead ()
    {
        QI_Read.SetActive(false);
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
        NumberShower.text = PageInDoc.ToString() + "/" + numberDocInInventory.ToString(); 

        if (numberDocInInventory == 0)
        {
            ImageShower2.color = empty_color;
            PageInDoc = 0;
            ImageShower.color = empty_color;
            ShortInfoShower.text = "هیچ مدرکی وجود ندارد";
            NameDocShower.text = "";
            NameDocShower2.text = "";
            ReadButton.SetActive(false);
        }
        else
        {
            ImageShower2.color = normal_color;
            ImageShower2.sprite = documents[PageInDoc].ImageDocument; 
            ReadButton.SetActive(true);
            ImageShower.color = normal_color;
            NameDocShower.text = documents[PageInDoc].nameDocument;
            NameDocShower2.text = documents[PageInDoc].nameDocument;
            ShortInfoShower.text = documents[PageInDoc].ShortInfo;
            MainInfoShower.text = documents[PageInDoc].infoDocument;
            ImageShower.sprite = documents[PageInDoc].ImageDocument;
        }
        
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Delete))
        {
            for (int i = 0; i < 6; i++)
            {
                RemoveItem(inventory[i]);
            }
        }

        if(!isFront && ImageShower.transform.eulerAngles.y < 180)
        {
            ImageShower.transform.Rotate(0, Speed, 0);
        }


        if (isFront && ImageShower.transform.eulerAngles.y > 0)
        {
            ImageShower.transform.Rotate(0, Speed * -1, 0);
        }


        if (ImageShower.transform.eulerAngles.y >= 90 && !isFront && !isChangeImage)
        {
            isChangeImage = true;
            ImageShower.sprite = documents[PageInDoc].BackDoc;
        }

        if (ImageShower.transform.eulerAngles.y <= 90 && isFront && !isChangeImage)
        {
            isChangeImage = true;
            ImageShower.sprite = documents[PageInDoc].ImageDocument;
        }

    }

    public void PanelNew (string nameItem , Sprite spriteItem , string Shortinfo , bool isDoc)
    {

        NewItemPanel.SetActive(true);

        NameItemNewShower.text = nameItem;
        SpriteItemNewShower.sprite = spriteItem;

        if (isDoc)
            ShortInfoNewShower.text = Shortinfo;
        else
            ShortInfoNewShower.text = "";
    }

}

[Serializable] public struct Documents
{
    public string nameDocument;
    public Sprite ImageDocument;
    public Sprite BackDoc;
    public string ShortInfo;
    [TextArea] public string infoDocument;
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
    public Sprite BackDoc;
    public string ShortInfo;
    [TextArea] public string infoDocument;
}

