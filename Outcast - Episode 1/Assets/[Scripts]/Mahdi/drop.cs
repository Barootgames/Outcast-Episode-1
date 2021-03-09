using UnityEngine;
using UnityEngine.EventSystems;


public class drop : MonoBehaviour , IDropHandler
{
    private InventoryManger manger;
    
    private void Start()
    {
        manger = GameObject.FindObjectOfType<InventoryManger>();
    }

    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        manger.GetComponent<InventoryManger>().item_drop_name = gameObject.name;
        manger.GetComponent<InventoryManger>().TryToCombin();
        manger.GetComponent<InventoryManger>().item_drag_name = "";
        manger.GetComponent<InventoryManger>().item_drop_name = "";
    }
}
