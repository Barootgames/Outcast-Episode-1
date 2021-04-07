using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionOff : MonoBehaviour
{

    InteractionController [] all;

    public void OnEnable()
    {
        all = GameObject.FindObjectsOfType<InteractionController>();

        for (int i = 0; i < all.Length; i++)
        {
            all[i].enabled = false;
        }
    }

    public void OnDisable()
    {
        for (int i = 0; i < all.Length; i++)
        {
            all[i].enabled = true;
        }
    }
}
