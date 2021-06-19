using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text_Bubble : MonoBehaviour
{
    PlayerMovement playerMovement;
    void Awake()
    {
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
    }

    private void OnEnable()
    {
        playerMovement.RunStop();
        playerMovement.Stop();
        playerMovement.enabled = false;
    }

    private void OnDisable()
    {
        playerMovement.enabled = true;
    }
}
