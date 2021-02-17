﻿using UnityEngine;
using UnityEngine.UI;
public class Tutorail : MonoBehaviour
{

    [SerializeField] private GameObject Controller;
    [SerializeField] [TextArea] string [] infoTutorail;
    [SerializeField] private GameObject TutorailPanel;
    [SerializeField] private Text TutorailText;
    [SerializeField] private Button[] MoveButtons;
    [SerializeField] private Button RunButton;

    void Start()
    {
        TutorailShow(1);
    }

    void Update()
    {
        
    }

    public void TutorailShow (int a)
    {

        TutorailPanel.SetActive(true);
        if (a == 1)
        {
            MoveButtons[0].image.color = new Color(255, 255, 255, 255);
            MoveButtons[1].image.color = new Color(255, 255, 255, 255);

            TutorailText.text = infoTutorail[0];
        }

        if(a == 2)
        {
            RunButton.image.color = new Color(255, 255, 255, 255);
            TutorailText.text = infoTutorail[1];
            RunButton.transform.parent.gameObject.SetActive(true);
        }

        if(a == 3)
        {
            TutorailText.text = infoTutorail[2];
        }

        if (a == 4)
        {
            TutorailText.text = infoTutorail[3];

        }

    }

    public void TutorailShowOff (int a)
    {
        if (TutorailPanel.activeInHierarchy && a == 1)
        {
            TutorailPanel.SetActive(false);
            MoveButtons[0].image.color = new Color(255, 255, 255, 0.823f);
            MoveButtons[1].image.color = new Color(255, 255, 255, 0.823f);
        }

        if(TutorailPanel.activeInHierarchy && a == 2)
        {
            TutorailPanel.SetActive(false);
            RunButton.image.color = new Color(255, 255, 255, 0.823f);
        }

    }
}
