using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;

    float horizontalMove = 0f;

    public float runSpeed = 40f;

    // Update is called once per frame
    void Update()
    {

        //horizontalMove= Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        
    }

    void FixedUpdate ()
    {
        //character koskesho ba in tekon midim

        controller.Move(horizontalMove * Time.fixedDeltaTime, false, false);
    }

    public void MoveRight()
    {
        horizontalMove = 1 * runSpeed;

        
    }
    public void MoveLeft()
    {
        horizontalMove = -1 * runSpeed;
    }

    public void Stop()
    {
        horizontalMove = 0 * runSpeed;
    }
}
