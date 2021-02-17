using UnityEngine;

public class Trigger : MonoBehaviour
{
    
    [SerializeField] private GameObject Controller;
    
   
    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.gameObject.tag != "Player" || hit.isTrigger)
        {
            return;
        }
        Controller.GetComponent<Scene2>().CheckTrigger(this.name);
    }
}
