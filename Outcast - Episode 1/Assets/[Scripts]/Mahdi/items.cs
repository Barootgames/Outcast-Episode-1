using UnityEngine;

public class items : MonoBehaviour
{
    private string _itemName;
    [SerializeField] private GameObject inventoryManger;

    [Space] [Header("if document tick hre and typing there")]
    [SerializeField] private bool isDocument;

    [SerializeField] private string docName;
    [SerializeField] [TextArea]
    private string Info;
    
    private Sprite _itemSprite;

    void Start()
    {
        _itemName = this.GetComponent<SpriteRenderer>().name;
        _itemSprite = this.GetComponent<SpriteRenderer>().sprite;
    }
    private void OnMouseDown()
    {
        if (!isDocument)
        {
            inventoryManger.GetComponent<InventoryManger>().AddItem(_itemName,_itemSprite);
            Destroy(gameObject);
        }
        else
        {
            inventoryManger.GetComponent<InventoryManger>().AddDocument(_itemSprite,docName,Info);
            Destroy(gameObject);
        }
    }
}
