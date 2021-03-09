using UnityEngine;

public class Scene3SF : MonoBehaviour
{
    private Step _step;
    private InventoryManger _inventoryManger;
    [SerializeField] private Sprite KeyRoom4;

    
    void Start()
    {
        #region Steps
        _step = GameObject.FindObjectOfType<Step>();
        _inventoryManger = GameObject.FindObjectOfType<InventoryManger>();

        if(_step.Steps[11] && !_step.Steps[12])
        {
            _inventoryManger.AddItem("KeyArtanRoom", KeyRoom4);
        }

        #endregion
    }

   
    void Update()
    {
        
    }
}
