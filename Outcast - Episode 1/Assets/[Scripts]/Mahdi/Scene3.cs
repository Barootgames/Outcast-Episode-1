using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Scene3 : MonoBehaviour
{
    private int RingTheBellTime;
    [SerializeField] private Animator Margin;

    [SerializeField] private GameObject _Player;
    [SerializeField] private GameObject _Jamshid;
    [SerializeField] private Transform JamshidPosTarget;
    [SerializeField] private float JamshidSpeed;


    private Animator JamshidAnimator;
    private bool JamshidWork = false;

    [SerializeField] private GameObject ControlsButton;
    [SerializeField] private GameObject Converstion;


    [SerializeField] private Light2D [] _lights;
    [SerializeField] private Light2D _mainLight;
    [SerializeField] private float [] IntensityValues;

    private Step _step;


    void Start()
    {
        _step = GetComponent<Step>();

        if(_step.Steps[7])
        {
            AllLightOff();
        }

        JamshidAnimator = _Jamshid.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if(JamshidWork && _Jamshid.transform.position != JamshidPosTarget.position)
        {
            _Jamshid.transform.position = Vector3.MoveTowards(_Jamshid.transform.position,
                JamshidPosTarget.position, JamshidSpeed * Time.fixedDeltaTime);
        }
        if (_Jamshid.transform.position == JamshidPosTarget.position && 
            JamshidAnimator.GetBool("Walk"))
        {
            
            JamshidAnimator.SetBool("Walk", false);
            Converstion.GetComponent<DialogueInteraction>().OnDialogueStarted(_Player);
        }
    }

    public void RingTheBell ()
    {
        RingTheBellTime++;

        if(RingTheBellTime == 2)
        {
            MarginOpen();
            JamshidWork = true;

            ControlsButton.SetActive(false);
        }


    }

    public void CheckTouch (string name)
    {
        if (name == "Interaction Bell")
        {
            RingTheBell();
        }
    }


    public void MarginOpen ()
    {
        Margin.gameObject.SetActive(true);
        Margin.SetBool("Show", true);
    }
    public void MarginClose ()
    {
        Margin.SetBool("Show", false);
    }

    public void AllLightOff ()
    {
        for (int i = 0; i < _lights.Length; i++)
        {
            _lights[i].intensity = 0f;
        }
        _mainLight.intensity = 0.1f;
    }
    public void AllLightOn ()
    {
        for (int i = 0; i < _lights.Length; i++)
        {
            _lights[i].intensity = IntensityValues[i];
        }
        _mainLight.intensity = 0.2f;
    }
}
