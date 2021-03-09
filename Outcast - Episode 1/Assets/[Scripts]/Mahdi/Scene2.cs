using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;
using System.Collections;


public class Scene2 : MonoBehaviour
{

    private int timeEnter;

    [SerializeField] private GameObject _Tutorail;

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
    [SerializeField] private GameObject fuse2Item;

 

    void Start()
    {

        #region Steps

        _step = GetComponent<Step>();

        // tutorail
        if (!_step.Steps[0])
        {
            _Tutorail.GetComponent<Tutorail>().TutorailShow(1);
            _step.DoWork(0);
        }

        //dialog light
        if(_step.Steps[7])
        {
            AllLightOff();

            if(!_step.Steps[8])
            {
                fuse2Item.SetActive(true);
            }
        }

        //fuse in place
        if(_step.Steps[9])
        {
            fuseBoxObjs[0].SetActive(true);
            fuseBoxObjs[1].GetComponent<Image>().sprite = greenLight;
            FuseBox.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(true);
            FuseBox.transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = greenLight;
        }

        //button_fuse_on
        if(_step.Steps[10])
        {
            AllLightOn();
            fuseBoxObjs[2].SetActive(true);
            fuseBoxObjs[3].SetActive(false);
            FuseBox.transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = greenLight;
            FuseBox.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
            FuseBox.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true);
            FuseBox.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(true);
        }

        #endregion

        MainThunder.GetComponent<Animator>().Play("Thunder_Light_Long");
        Soundplayer = GetComponent<AudioSource>();

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

        if (name == "Interaction FuseBox" && !FuseBox.transform.GetChild(0).gameObject.activeInHierarchy && _step.Steps[7] && !_step.Steps[10])
        {
            FuseBox.transform.GetChild(0).gameObject.SetActive(true);
            FuseBox.transform.GetChild(1).gameObject.SetActive(false);
        }

        if(name == "Fuse2")
        {
            _step.DoWork(8);
        }

    }

    public void CheckTrigger(string name)
    {
        if (name == "tutorail_Run" && !_step.Steps[1])
        {
            _step.DoWork(1);
            _Tutorail.GetComponent<Tutorail>().TutorailShow(2);
        }

        if(name == "tutorail_Interaction" && !_step.Steps[2])
        {

            _step.DoWork(2);
            _Tutorail.GetComponent<Tutorail>().TutorailShow(3);
        }

        if(name == "Thunder1" && !_step.Steps[4])
        {
            MainThunder.GetComponent<Animator>().Play("Thunder_Light_Long");
            PlaySound(Soundthunder1, false, 1f);

            _step.DoWork(4);
        }

        if (name == "Thunder2" && !_step.Steps[5])
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

                    _step.DoWork(5);
                }
            }
        }

        if(name == "Exit_Telephone" && DoTouch && !_step.Steps[6])
        {
            CanbirdRun = true;
            PlaySound(birdSound, false, 1f);

            _step.DoWork(6);
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
        if(a == 1 && !_step.Steps[3])
        {
            _step.DoWork(3);
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
        if(_step.Steps[9])
        {
            fuseBoxObjs[0].SetActive(true);
            fuseBoxObjs[1].GetComponent<Image>().sprite = greenLight;

            FuseBox.transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = greenLight;
            FuseBox.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(true);

            _step.DoWork(9);
        }
    }

    public void TryHandleFuse ()
    {
        if(_step.Steps[9])
        {
            fuseBoxObjs[2].gameObject.SetActive(true);
            fuseBoxObjs[3].gameObject.SetActive(false);

            FuseBox.transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = greenLight;
            FuseBox.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
            FuseBox.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true);
            FuseBox.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(true);


            _step.DoWork(10);
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
