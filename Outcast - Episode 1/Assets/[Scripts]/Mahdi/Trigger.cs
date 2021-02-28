using UnityEngine;
using UnityEngine.SceneManagement;

public class Trigger : MonoBehaviour
{
    
    [SerializeField] private GameObject Controller;
    
   
    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.gameObject.tag != "Player" || hit.isTrigger)
        {
            return;
        }

        if(SceneManager.GetActiveScene().buildIndex == 1)
             Controller.GetComponent<Scene2>().CheckTrigger(this.name);
    }

    private void OnTriggerExit2D(Collider2D hit)
    {
        if (hit.gameObject.tag != "Player" || hit.isTrigger)
        {
            return;
        }
        if (SceneManager.GetActiveScene().buildIndex == 1)
            Controller.GetComponent<Scene2>().CheckTriggerExit(this.name);
    }
}
