using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KeyRiddle : MonoBehaviour, IDragHandler, IEndDragHandler
{
    // Start is called before the first frame update
    bool canPlace = false;
    Vector3 place;
    Vector3 defaultPos;
    KeyRiddleController keyRiddleController;
    RiddleControllerFlow riddleControllerFlow;
    int index;
    void Start()
    {
        defaultPos = GetComponent<RectTransform>().position;
        riddleControllerFlow = FindObjectOfType<RiddleControllerFlow>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateDrag()
    {
        
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<KeyRiddleController>())
        {
            if (!collision.gameObject.GetComponent<KeyRiddleController>().hasKey)
            {
                canPlace = true;
                place = collision.gameObject.transform.position;
                index = int.Parse(collision.gameObject.name);
                keyRiddleController = collision.gameObject.GetComponent<KeyRiddleController>();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canPlace = false;
        if (keyRiddleController && collision.GetComponent<KeyRiddleController>())
        {
            keyRiddleController.hasKey = false;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (keyRiddleController && !keyRiddleController.hasKey)
        {
            keyRiddleController.hasKey = true;
            transform.position = place;
            riddleControllerFlow.addToMap(gameObject.name.ToLower(), index);
        }
        else
        {
            transform.position = defaultPos;
        }
    }
}
