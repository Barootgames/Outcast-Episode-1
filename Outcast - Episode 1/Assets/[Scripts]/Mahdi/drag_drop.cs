using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class drag_drop : MonoBehaviour , IBeginDragHandler, IEndDragHandler , IDragHandler 
{
    [SerializeField] private Canvas _can;
    private Vector2 _orgin_pos;
    private RectTransform _rec;
    private InventoryManger _player;

    private void Awake()
    {
        _rec = GetComponent<RectTransform>();
        _orgin_pos = _rec.anchoredPosition;
        _player = GameObject.FindObjectOfType<InventoryManger>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _player.GetComponent<InventoryManger>().item_drag_name = this.name;
        transform.parent.transform.SetAsLastSibling();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _rec.anchoredPosition = _orgin_pos;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(this.GetComponent<Image>().sprite != null)
        {
            _rec.anchoredPosition += eventData.delta / _can.scaleFactor;
        }
    }
}
