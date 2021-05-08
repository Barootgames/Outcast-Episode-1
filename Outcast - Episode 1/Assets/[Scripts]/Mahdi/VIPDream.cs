using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VIPDream : MonoBehaviour
{
    [SerializeField] private AudioClip SoundRefrigeratorOpen;
    [SerializeField] [Range(0, 1)] private float VolumeRefrigeratorOpen;
    [SerializeField] private AudioClip SoundRefrigeratorClose;
    [SerializeField] [Range(0, 1)] private float VolumeRefrigeratorClose;


    [SerializeField] private GameObject Noise;
    [SerializeField] private GameObject Noise2;
    [SerializeField] private GameObject Page1Tv;
    [SerializeField] private GameObject Page2Tv;
    [SerializeField] private GameObject T_persian;
    [SerializeField] private GameObject Mogaseri;

    [SerializeField] private GameObject PanelFade;
    [SerializeField] private AudioSource soundPlayer;
    [SerializeField] private AudioClip SoundBrokenGlass;
    [Range(0, 1)] [SerializeField] private float VolumeBrokenGlass = 0.8f;
    [SerializeField] private AudioClip SoundRoomThunder;
    [Range(0, 1)] [SerializeField] private float VolumeThunder = 0.7f;

    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource music2;
    [SerializeField] private float SpeedVolumeUp;
    [SerializeField] private AudioSource NoisePlayer;

    [SerializeField] private Animator RoomThunder;
    [SerializeField] private LoadLevelInteraction _load;

    [SerializeField] private GameObject NewsPaper;
    [SerializeField] private GameObject PanelTV;
    [SerializeField] private GameObject InteractianControl;
    [SerializeField] private GameObject ControlTv;
    [SerializeField] private GameObject PanelControl;

    private Step _step;

    [SerializeField] private GameObject RefrigeratorOpen;
    [SerializeField] private GameObject RefrigeratorClose;

    [SerializeField] private GameObject RefrigeratorOpen2;
    [SerializeField] private GameObject RefrigeratorClose2;

    void Start()
    {
        _step = GameObject.FindObjectOfType<Step>();
      
        if(!_step.Steps[14])
        {
            #region AfterSleep
            GameObject.FindObjectOfType<Step>().DoWork(13);
            PanelFade.SetActive(true);
            PanelFade.GetComponent<Image>().color = new Color(0, 0, 0, 1);
            PanelFade.GetComponent<Animator>().enabled = false;
            StartCoroutine(GlassBrokenEvent(3f));
            music.volume = 0;
            music2.volume = 0;
            #endregion
        }

        if (!_step.Steps[14])
               _step.DoWork(14);

        if (_step.Steps[17])
            T_persian.SetActive(true);

        if (_step.Steps[19])
            Mogaseri.SetActive(true);

        if(_step.Steps[20])
        {
            Noise.SetActive(true);
            Noise2.SetActive(true);
            NoisePlayer.Play();
            _load.nextSceneName = "Scene 3-2 SF - LongDream";
           StartCoroutine(ChangePage(2.5f));
        }

        if(_step.Steps[26])
        {
            InteractianControl.SetActive(false);
            PanelControl.SetActive(false);
            Page1Tv.SetActive(true);
            Page2Tv.SetActive(true);
        }

        if (_step.Steps[28])
        {
            ControlTv.GetComponent<ObjectDrop>().enabled = false;
            InteractianControl.SetActive(true);
        }

        if (!_step.Steps[29])
        {
            NewsPaper.SetActive(true);
        }


    }

    private void PlaySound(AudioClip clip, float volume)
    {
        soundPlayer.clip = clip;
        soundPlayer.volume = volume;
        soundPlayer.Play();
    }

    IEnumerator WakeUpEvent (float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        PanelFade.GetComponent<Animator>().enabled = true;
        PanelFade.GetComponent<Animator>().SetBool("Show", false);
        StartCoroutine(FadeEnd(6f));
    }

    IEnumerator GlassBrokenEvent (float WaitTime)
    {
        yield return new WaitForSeconds(WaitTime);
        PlaySound(SoundBrokenGlass, VolumeBrokenGlass);
        StartCoroutine(WakeUpEvent(5f));
    }

    IEnumerator FadeEnd (float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        PanelFade.SetActive(false);
    }

    IEnumerator ChangePage (float _time)
    {
        yield return new WaitForSeconds(_time);


        if(Page1Tv.transform.GetChild(0).gameObject.activeInHierarchy)
        {
            Page1Tv.transform.GetChild(0).gameObject.SetActive(false);
            Page1Tv.transform.GetChild(1).gameObject.SetActive(true);
            Page2Tv.transform.GetChild(0).gameObject.SetActive(false);
            Page2Tv.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            Page1Tv.transform.GetChild(0).gameObject.SetActive(true);
            Page1Tv.transform.GetChild(1).gameObject.SetActive(false);
            Page2Tv.transform.GetChild(0).gameObject.SetActive(true);
            Page2Tv.transform.GetChild(1).gameObject.SetActive(false);
        }

        StartCoroutine(ChangePage(_time));
    }

    public void HalControl ()
    {
        _step.DoWork(26);
        InteractianControl.SetActive(false);
        PanelControl.SetActive(false);
        Page1Tv.SetActive(true);
        Page2Tv.SetActive(true);
    }

    public void ControlFully ()
    {
        ControlTv.GetComponent<ObjectDrop>().enabled = false;
        InteractianControl.SetActive(true);
        _step.DoWork(28);
    }

    void RefrigeratorIntraction ()
    {
        if(RefrigeratorOpen.activeInHierarchy)
        {
            RefrigeratorOpen.SetActive(false);
            RefrigeratorClose.SetActive(true);
        }
        else
        {
            RefrigeratorOpen.SetActive(true);
            RefrigeratorClose.SetActive(false);
        }

    }

    void RefrigeratorIntraction2()
    {
        if (RefrigeratorOpen2.activeInHierarchy)
        {
            RefrigeratorOpen2.SetActive(false);
            RefrigeratorClose2.SetActive(true);
        }
        else
        {
            RefrigeratorOpen2.SetActive(true);
            RefrigeratorClose2.SetActive(false);
        }

    }

    public void CheckTouch(string a)
    {
        if(a == "Interaction T" && !_step.Steps[18])
        {
            RoomThunder.SetBool("Show", true);
            PlaySound(SoundRoomThunder,VolumeThunder);
            _step.DoWork(18);
        }

        if(a == "Interaction Mogaseri" && !_step.Steps[20])
        {
            _step.DoWork(20);
            Noise.SetActive(true);
            Noise2.SetActive(true);
            NoisePlayer.Play();
            _load.nextSceneName = "Scene 3-2 SF - LongDream";
        }

        if(a == "NewsPaper" && !_step.Steps[29])
        {
            _step.DoWork(29);
        }

        if(a == "Interaction Tv" && _step.Steps[26])
        {
            PanelTV.SetActive(true);
        }

        if (a == "Interaction Control")
        {
            PanelControl.SetActive(true);
        }

        if(a == "Refrigerator")
        {
            RefrigeratorIntraction();
        }

        if (a == "Refrigerator2")
        {
            RefrigeratorIntraction2();
        }
    }

    private void Update()
    {
        VolumeUp();
    }

    void VolumeUp()
    {
        if (music.volume <= 0.3f)
        {
            music.volume += SpeedVolumeUp * Time.deltaTime;
        }

        if(music2.volume <= 0.4f)
        {
            music2.volume += SpeedVolumeUp * Time.deltaTime;
        }
    }
}
