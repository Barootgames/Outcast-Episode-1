using UnityEngine;

public class Scene3 : MonoBehaviour
{
    private int RingTheBellTime;
    [SerializeField] private Animator Margin;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            MarginOpen();
        }

        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            MarginClose();
        }
    }

    public void RingTheBell ()
    {
        RingTheBellTime++;
        print("ringThebellTime : " + RingTheBellTime);
    }

    public void CheckTouch (string name)
    {
        if (name == "Interaction Bell")
        {
            RingTheBell();
        }
    }

    public void MarginOpen ()
    {
        Margin.gameObject.SetActive(true);
        Margin.SetBool("Show", true);
    }
    public void MarginClose ()
    {
        Margin.SetBool("Show", false);
    }
}
