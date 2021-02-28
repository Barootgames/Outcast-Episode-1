using UnityEngine;
using UnityEngine.EventSystems;


public class drop : MonoBehaviour , IDropHandler
{
    private GameObject manger;
    
    private void Start()
    {
        manger = GameObject.Find("InventoryManger");
    }

    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        manger.GetComponent<InventoryManger>().item_drop_name = gameObject.name;
        manger.GetComponent<InventoryManger>().TryToCombin();
        manger.GetComponent<InventoryManger>().item_drag_name = "";
        manger.GetComponent<InventoryManger>().item_drop_name = "";
    }
}
