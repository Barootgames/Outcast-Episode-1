using UnityEngine;

public class Trigger : MonoBehaviour
{
    
    private GameObject Controller;
    
    void Start()
    {
        Controller = GameObject.Find("GameController");
    }
    
    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.gameObject.tag != "Player")
        {
            return;
        }
        
        if (this.name == "T1")
        {
            Controller.GetComponent<Scene2>().CheckTrigger(this.name);
        }
        
        else if (this.name == "T2")
        {
            Controller.GetComponent<Scene2>().CheckTrigger(this.name);
        }
        
        else if (this.name == "T3" && Controller.GetComponent<Scene2>().doTouch1)
        {
            Controller.GetComponent<Scene2>().CheckTrigger(this.name);
        }
    }
}
