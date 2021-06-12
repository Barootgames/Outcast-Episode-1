using UnityEngine;
using UnityEngine.EventSystems;


public class drop : MonoBehaviour, IDropHandler
{
    private InventoryManger manger;

    private void Start()
    {
        manger = GameObject.FindObjectOfType<InventoryManger>();
    }


    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        manger.transform.GetChild(1).GetChild(5).SetAsFirstSibling();

        manger.GetComponent<InventoryManger>().TryToCombin(this.name);

        manger.GetComponent<InventoryManger>().item_drag_name = "";
        manger.GetComponent<InventoryManger>().item_drop_name = "";
    }
}
