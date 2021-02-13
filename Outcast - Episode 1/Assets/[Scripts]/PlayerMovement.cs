using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;

    float horizontalMove = 0f;

    [SerializeField] private float WalkSpeed = 30f;
    [SerializeField] private float RunSpeed = 50f;
    [SerializeField] private float TimeCanRun = 10f;
    [SerializeField] private float EnergyBackSpeed = 0.5f;
    [SerializeField] private float TimeRest = 5;
    
    private float timeRest;
    private float timeEnergy; 
    private MoveMode moveMode;
    

    void Start()
    {
        timeRest = TimeRest;
        timeEnergy = TimeCanRun;
        moveMode = MoveMode.idle;
    }
    
    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
    }

    void FixedUpdate ()
    {
        // for pc
        
        if (Input.GetKeyDown(KeyCode.LeftShift) && moveMode != MoveMode.noEnergy)
        {
            moveMode = MoveMode.run;
            animator.SetBool("Run",true);
        }
 
        if (Input.GetKeyUp(KeyCode.LeftShift) && moveMode != MoveMode.noEnergy)
        {
            moveMode = MoveMode.walk;
            animator.SetBool("Run",false);
        }

        if (moveMode == MoveMode.idle)
        {
            if (timeEnergy < TimeCanRun)
            {
                timeEnergy += (EnergyBackSpeed * 2 * Time.fixedDeltaTime);
            }
        }
        
        else if (moveMode == MoveMode.walk)
        {
            controller.Move(horizontalMove * WalkSpeed * Time.fixedDeltaTime, false, false);
            if (timeEnergy < TimeCanRun)
            {
                timeEnergy += EnergyBackSpeed * Time.fixedDeltaTime;
            }
        }
        
        else if (moveMode == MoveMode.run)
        {
            controller.Move(horizontalMove * RunSpeed * Time.fixedDeltaTime, false, false);
            if (timeEnergy > 0)
            {
                timeEnergy -= Time.fixedDeltaTime;
            }
            else
            {
                animator.SetBool("NoEnergy",true);
                moveMode = MoveMode.noEnergy;
                timeRest = TimeRest;
                timeEnergy = 0.5f;
            }
        }
        
        else if (moveMode == MoveMode.noEnergy)
        {
            if (timeRest > 0)
            {
                timeRest -= Time.fixedDeltaTime;
            }
            else
            {
                horizontalMove = 0;
                animator.SetBool("Run",false);
                animator.SetBool("NoEnergy",false);
                moveMode = MoveMode.idle;
            }
        }
        
        
        
    }
    
    public void MoveRight()
    {
        if (moveMode != MoveMode.noEnergy)
        {
            moveMode = MoveMode.walk;
            horizontalMove = 1;
        }

    }
    
    public void MoveLeft()
    {
        if (moveMode != MoveMode.noEnergy)
        {
            moveMode = MoveMode.walk;
            horizontalMove = -1;
        }
    }

    public void Stop()
    {
        if (moveMode != MoveMode.noEnergy)
        {     
            moveMode = MoveMode.idle;
            horizontalMove = 0;
        }
    }

    public void Run()
    {
        if (moveMode != MoveMode.noEnergy)
        {
            moveMode = MoveMode.run;
            animator.SetBool("Run",true);
        }
    }

    public void RunStop()
    {
        if (moveMode != MoveMode.noEnergy)
        {
            moveMode = MoveMode.walk;
            animator.SetBool("Run",false);
        }
    }
    
}






[Serializable]
public enum MoveMode
{
    idle , walk , run , noEnergy
}
