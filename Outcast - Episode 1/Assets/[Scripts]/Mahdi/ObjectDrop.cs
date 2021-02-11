using UnityEngine;

public class ObjectDrop : MonoBehaviour
{
    private GameObject _manger;
    
    void Start()
    {
        _manger = GameObject.Find("Player");
    }
    
    void OnMouseEnter ()
    {
        // in ja mal drop shodan roy in item hast.
        
    }
}
