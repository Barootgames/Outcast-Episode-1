using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoints : MonoBehaviour
{
    public Transform[] respawnLocations;

    CharacterController2D playerController;
    CinemachineVirtualCamera cvcam;

    Transform head;

    private void Start()
    {
        if(respawnLocations.Length > 0)
        {
            playerController = FindObjectOfType<CharacterController2D>();
            playerController.PlacePlayerOnRespawnLocation();

            head = playerController.transform.GetChild(2);

            cvcam = FindObjectOfType<CinemachineVirtualCamera>();
            cvcam.Follow = head;
        }
    }

    public Vector3 getRespawnLocation(int index)
    {
        return respawnLocations[index].position;
    }
}
