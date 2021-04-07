using UnityEngine;
using UnityEngine.EventSystems;


public class drop : MonoBehaviour, IDropHandler , IPointerEnterHandler
{
    private InventoryManger manger;

    private void Start()
    {
        manger = GameObject.FindObjectOfType<InventoryManger>();
    }


    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        manger.transform.GetChild(0).GetChild(5).SetAsFirstSibling();
        manger.GetComponent<InventoryManger>().item_drop_name = gameObject.name;
        manger.GetComponent<InventoryManger>().TryToCombin();
        manger.GetComponent<InventoryManger>().item_drag_name = "";
        manger.GetComponent<InventoryManger>().item_drop_name = "";
    }

    void IDropHandler.OnDrop(PointerEventData eventData)
    {
 

    }
}
