using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Scene2 : MonoBehaviour
{

    [SerializeField] private GameObject PanelTutorail;
    [SerializeField] private Button [] MoveButtons;
    
    private bool enterT1; // zir tablo
    private int enterTimeT2;   // gabl dar asli
    private bool enterT3;  // telephone
    
    public bool doTouch1; //telephone
    
    private bool [] DoWork = new bool[4];
    // 0= tutorail   1= first thunder   2= secend thunder  3= telephone and bird

    void Start()
    {
        PanelTutorailShow();
    }
    
    void Update()
    {
        
    }
    
    void PanelTutorailShow()
    {
        PanelTutorail.SetActive(true);
        MoveButtons[0].image.color = new Color(255,255,255,255);
        MoveButtons[1].image.color = new Color(255,255,255,255);
    }
    
    public void PanelTutorailShowOff()
    {
        if (PanelTutorail.activeInHierarchy)
        {
            PanelTutorail.SetActive(false);
            MoveButtons[0].image.color = new Color(255,255,255,0.823f);
            MoveButtons[1].image.color = new Color(255,255,255,0.823f);
        }
    }
    
    public void CheckTouch(string name)
    {
        if (name == "Interaction Telephone")
        {
            doTouch1 = true;
        }
        
        CheckEvent();
    }

    public void CheckTrigger(string name)
    {
        if (name == "T1" && !enterT1)
        {
            enterT1 = true;
        }
        else if (name == "T2" && enterTimeT2 < 5)
        {
            enterTimeT2++;
        }
        else if (name == "T3" && !enterT3)
        {
            enterT3 = true;
        }
        
        CheckEvent();
    }
    
    public void CheckEvent()
    {
        if (enterT1 && !DoWork[1])
        {
            // thunder1
            print("// thunder1");
            DoWork[1] = true;
        }

        if ((enterTimeT2 == 1 || enterTimeT2 == 5) && !DoWork[2])
        {
            // thunder2   2time
            print("// thunder2");
            if (enterTimeT2 == 5)
            {
                DoWork[2] = true;
            }
        }

        if (enterT3 && doTouch1 && !DoWork[3])
        {
            // bird come
            print("// bird");
            DoWork[3] = true;
        }
    }
}
