using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectDrop : MonoBehaviour
{
    private InventoryManger _inventoryManger;
    private bool Used = false;

    void Start()
    {
        _inventoryManger = GameObject.FindObjectOfType<InventoryManger>();
    }



    void Update()
    {
        if(Used)
        {
            this.GetComponent<ObjectDrop>().enabled = false;
            return;
        }


        Vector2 origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero, 0f);

        if (hit)
        {

            if (hit.collider.gameObject.name == (gameObject.name))
            {
                if(GameObject.FindObjectOfType<InventoryManger>().item_drag_name == "KeyArtanRoom" &&
                    this.name == "Door4VIP")
                {

                    _inventoryManger.SpecialCombin(1);
                    _inventoryManger.item_drag_name = "";
                    _inventoryManger.item_drop_name = "";
                    Used = true;
                }

                if (GameObject.FindObjectOfType<InventoryManger>().item_drag_name == "Battery" &&
                      this.name == "Control TV")
                {
                    _inventoryManger.SpecialCombin(3);
                    _inventoryManger.item_drag_name = "";
                    _inventoryManger.item_drop_name = "";
                    Used = true;
                }

                if (GameObject.FindObjectOfType<InventoryManger>().item_drag_name == "Zero Key" &&
                           this.name == "Door0")
                {
                    print("ok");


                    _inventoryManger.SpecialCombin(4);
                    _inventoryManger.item_drag_name = "";
                    _inventoryManger.item_drop_name = "";
                    Used = true;
                }


            }
        }
    }
}
