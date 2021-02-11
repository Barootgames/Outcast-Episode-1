using UnityEngine;
using UnityEngine.EventSystems;

public class drop : MonoBehaviour , IPointerEnterHandler
{
    private GameObject manger;
    
    private void Start()
    {
        manger = GameObject.Find("inventoryManger");
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        manger.GetComponent<InventoryManger>().item_drop_name = gameObject.name;
        manger.GetComponent<InventoryManger>().TryToCombin();
        manger.GetComponent<InventoryManger>().item_drag_name = null;
        manger.GetComponent<InventoryManger>().item_drop_name = null;
    }
}
