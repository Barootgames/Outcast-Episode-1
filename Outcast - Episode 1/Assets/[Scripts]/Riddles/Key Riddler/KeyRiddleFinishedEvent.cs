using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyRiddleFinishedEvent : MonoBehaviour
{
    public GameObject keyRiddle;
    public GameObject SymbolicLock;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RiddleFinished()
    {
        keyRiddle.SetActive(false);
    }

    public void EnableNextRiddle()
    {
        gameObject.SetActive(false);
        SymbolicLock.SetActive(true);
    }
}
