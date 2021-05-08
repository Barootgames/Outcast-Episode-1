using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.U2D.Animation;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    [SerializeField] private GameObject _Tutorail;
    [SerializeField] private GameObject _controller;
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

    private Step _step;


    void Start()
    {
        _step = GameObject.FindObjectOfType<Step>();

        if(_step.Steps[13] && !_step.Steps[39])
        {
            ChangeClothes(1);
        }

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
            if(SceneManager.GetActiveScene().buildIndex == 1)
            {
                _Tutorail.GetComponent<Tutorail>().TutorailShowOff(2);
            }
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

                if(SceneManager.GetActiveScene().name == "Scene 2")
                       _controller.GetComponent<Scene2>().CheckEvent(1);
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

    public void ChangeClothes (int a)
    {
        if (a == 0)
        {
            // orginal

            transform.GetChild(2)
                .GetComponent<SpriteResolver>().SetCategoryAndLabel("BaseBody", "Original");

            transform.GetChild(10)
               .GetComponent<SpriteResolver>().SetCategoryAndLabel("LeftArm", "Original");

            transform.GetChild(13)
              .GetComponent<SpriteResolver>().SetCategoryAndLabel("LeftUpArm", "Original");

            transform.GetChild(18)
              .GetComponent<SpriteResolver>().SetCategoryAndLabel("RightArm", "Original");

            transform.GetChild(21)
              .GetComponent<SpriteResolver>().SetCategoryAndLabel("RightUpArm", "Original");
        }

        if (a == 1)
        {
            // Lebas Zir

            transform.GetChild(2)
                .GetComponent<SpriteResolver>().SetCategoryAndLabel("BaseBody", "BlackRekabi");


            transform.GetChild(10)
               .GetComponent<SpriteResolver>().SetCategoryAndLabel("LeftArm", "BlackRekabi");

            transform.GetChild(13)
              .GetComponent<SpriteResolver>().SetCategoryAndLabel("LeftUpArm", "BlackRekabi");

            transform.GetChild(18)
              .GetComponent<SpriteResolver>().SetCategoryAndLabel("RightArm", "BlackRekabi");

            transform.GetChild(21)
              .GetComponent<SpriteResolver>().SetCategoryAndLabel("RightUpArm", "BlackRekabi");
        }
    }
    
}


[Serializable]
public enum MoveMode
{
    idle , walk , run , noEnergy
}
