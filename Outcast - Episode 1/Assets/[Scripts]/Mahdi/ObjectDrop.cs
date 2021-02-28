using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectDrop : MonoBehaviour
{
    public LayerMask itemlayer;
    [SerializeField] private InventoryManger _inventoryManger;
    [SerializeField] private GameObject _MangerScene;

    void Start()
    {
       
        
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            _MangerScene = GameObject.Find("GameController");
        }

    }



    void Update ()
    {
        Vector2 origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero, 0f,itemlayer );

        if(hit)
        {
            if (hit.collider.gameObject.name == (gameObject.name))
            {
                // events
                
            }
        }
     
    }
}
