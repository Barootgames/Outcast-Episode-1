using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;
using System.Collections;


public class Scene2 : MonoBehaviour
{

    private int timeEnter;

    [SerializeField] private GameObject _Tutorail;
    private bool [] DoWork = new bool[10];
    // 0 = tutorail_walk  // 1 = tutorail_run
    // 2 = tutorail_interaction  // 3 = tutorail_rest
    // 4 = First thunder    // 5 = secend thunder

    private Step _step;

    [SerializeField] private GameObject bird;
    [SerializeField] private Transform targetBird;
    [SerializeField] private AudioClip birdSound;
    [SerializeField] private float birdSpeed;
    private bool CanbirdRun;


    private bool DoTouch;

    [SerializeField] private GameObject MainThunder;

    [SerializeField] private AudioClip Soundthunder1;
    [SerializeField] private AudioClip Soundthunder2;
    private AudioSource Soundplayer;


    //
    [SerializeField] private Light2D [] _lights;
    [SerializeField] private float [] IntensityValues;
    [SerializeField] private Light2D MainLight;

    // fuse box
    [SerializeField] private GameObject FuseBox;
    [SerializeField] private GameObject FusePlace;
    [SerializeField] private Sprite CloseFuseBox;
    [SerializeField] private Sprite OpenFuseBox;
    [SerializeField] private GameObject PanelFuseBox;
    [SerializeField] private GameObject [] fuseBoxObjs;
    [SerializeField] private Sprite greenLight;
    public bool fuseInside = false;


    void Start()
    {

        AllLightOff();

        _step = GetComponent<Step>();
        MainThunder.GetComponent<Animator>().Play("Thunder_Light_Long");
        Soundplayer = GetComponent<AudioSource>();

        for (int i = 0; i < DoWork.Length; i++)
        {
            DoWork[i] = _step.Steps[i];
        }

        if(!DoWork[0])
        {
            _Tutorail.GetComponent<Tutorail>().TutorailShow(1);
        }
    }
    
    void FixedUpdate()
    {
        BirdRun();
    }
    
    
    public void CheckTouch(string name)
    {
        if (name == "Interaction Telephone" && !DoTouch)
        {
            DoTouch = true;
        }



        if (name == "Interaction FuseBox" && FuseBox.transform.GetChild(0).gameObject.activeInHierarchy)
        {
            PanelFuseBox.SetActive(true);
            FusePlace.SetActive(true);
        }

        if (name == "Interaction FuseBox" && !FuseBox.transform.GetChild(0).gameObject.activeInHierarchy)
        {
            FuseBox.transform.GetChild(0).gameObject.SetActive(true);
            FuseBox.transform.GetChild(1).gameObject.SetActive(false);
        }

    }

    public void CheckTrigger(string name)
    {
        if (name == "tutorail_Run" && !DoWork[1])
        {
            DoWork[1] = true;
            _step.Steps[1] = true;
            _Tutorail.GetComponent<Tutorail>().TutorailShow(2);
        }

        if(name == "tutorail_Interaction" && !DoWork[2])
        {
            DoWork[2] = true;
            _step.Steps[2] = true;
            _Tutorail.GetComponent<Tutorail>().TutorailShow(3);
        }

        if(name == "Thunder1" && !DoWork[4])
        {
            MainThunder.GetComponent<Animator>().Play("Thunder_Light_Long");
            PlaySound(Soundthunder1, false, 1f);
            DoWork[4] = true;
            _step.Steps[4] = true;
        }

        if (name == "Thunder2" && !DoWork[5])
        {
            if(timeEnter >= 5)
                     return;
           
            timeEnter++;

            if (timeEnter == 1 || timeEnter == 5)
            {
                MainThunder.GetComponent<Animator>().Play("Thunder_Light_Long");
                PlaySound(Soundthunder2, false, 1f);
                if(timeEnter == 5)
                {
                    DoWork[5] = true;
                    _step.Steps[5] = true;
                }
            }
        }

        if(name == "Exit_Telephone" && DoTouch)
        {
            CanbirdRun = true;
            PlaySound(birdSound, false, 1f);
            DoWork[5] = true;
            _step.Steps[5] = true;
        }

    }

    public void CheckTriggerExit (string name)
    {
        if(name == "FuseBoxExit")
        {
            PanelFuseBox.SetActive(false);
        }
    }


    private void BirdRun ()
    {
        if(!CanbirdRun)
        {
            return;
        }

        if(bird.transform.position != targetBird.position)
        {
            bird.transform.position = Vector3.MoveTowards(bird.transform.position, targetBird.position,
            birdSpeed * Time.fixedDeltaTime);
        }
    }

    public void CheckEvent(int a)
    {
        if(a == 1 && !DoWork[3])
        {
            _step.Steps[3] = true;
            DoWork[3] = true;
            _Tutorail.GetComponent<Tutorail>().TutorailShow(4);
        }

    }

    private void PlaySound (AudioClip sound , bool isLoop , float volume)
    {
        Soundplayer.clip = sound;
        Soundplayer.loop = isLoop;
        Soundplayer.volume = volume;

        Soundplayer.Play();
    }

    public void FuseCheck ()
    {
        if(fuseInside)
        {
            fuseBoxObjs[0].SetActive(true);
            fuseBoxObjs[1].GetComponent<Image>().sprite = greenLight;
        }
    }

    public void TryHandleFuse ()
    {
        if(fuseInside)
        {
            fuseBoxObjs[2].gameObject.SetActive(true);
            fuseBoxObjs[3].gameObject.SetActive(false);
            StartCoroutine(waitToClose(0.5f));
           StartCoroutine(waitToLightCome(1f));
        }
    }

    IEnumerator waitToClose (float WaitTime)
    {
        yield return new WaitForSeconds(WaitTime);
        PanelFuseBox.SetActive(false);
    }

    IEnumerator waitToLightCome (float Waittime)
    {
        yield return new WaitForSeconds(Waittime);
        AllLightOn();
    }

    public void AllLightOff ()
    {
        for (int i = 0; i < _lights.Length; i++)
        {
            _lights[i].intensity = 0f;
        }

        MainLight.intensity = 0.2f;
    }

    public void AllLightOn ()
    {
        for (int i = 0; i < _lights.Length; i++)
        {
            _lights[i].intensity = IntensityValues[i];
        }

        MainLight.intensity = 0.5f;
    } 

}
