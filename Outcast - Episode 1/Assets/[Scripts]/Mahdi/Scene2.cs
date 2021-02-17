using UnityEngine;
using UnityEngine.UI;


public class Scene2 : MonoBehaviour
{

    private int timeEnter;

    [SerializeField] private GameObject _Tutorail;
    private bool [] DoWork = new bool[10];
    // 0 = tutorail_walk  // 1 = tutorail_run
    // 2 = tutorail_interaction  // 3 = tutorail_rest
    // 4 = First thunder    // 5 = secend thunder

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

    void Start()
    {
        MainThunder.GetComponent<Animator>().Play("Thunder_Light_Long");
        Soundplayer = GetComponent<AudioSource>();
        _Tutorail.GetComponent<Tutorail>().TutorailShow(1);
        DoWork[0] = true;
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
    }

    public void CheckTrigger(string name)
    {
        if (name == "tutorail_Run" && !DoWork[1])
        {
            DoWork[1] = true;
            _Tutorail.GetComponent<Tutorail>().TutorailShow(2);
        }

        if(name == "tutorail_Interaction" && !DoWork[2])
        {
            DoWork[2] = true;
            _Tutorail.GetComponent<Tutorail>().TutorailShow(3);
        }

        if(name == "Thunder1" && !DoWork[4])
        {
            MainThunder.GetComponent<Animator>().Play("Thunder_Light_Long");
            PlaySound(Soundthunder1, false, 1f);
            DoWork[4] = true;
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
                }
            }
        }

        if(name == "Exit_Telephone" && DoTouch)
        {
            CanbirdRun = true;
            PlaySound(birdSound, false, 1f);
            DoWork[5] = true;
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
}
