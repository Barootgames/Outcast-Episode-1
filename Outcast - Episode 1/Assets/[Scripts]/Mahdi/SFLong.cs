using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SFLong : MonoBehaviour
{
    [SerializeField] private AudioClip SoundOutDream;
    [SerializeField] [Range(0, 1)] private float VolumeOutDream = 0.5f;

    [SerializeField] private AudioSource SoundPlayer;
    [SerializeField] private GameObject BookR;
    private Step _step;
    [SerializeField] private GameObject PanelFadeWhite;
    private Animator Fade2;

    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource music2;
    [SerializeField] private float SpeedVolumeDown;
    private bool CanVolumeDown;

    [SerializeField] private LoadLevelInteraction load;
   
    void Start()
    {
        Fade2 = PanelFadeWhite.GetComponent<Animator>();
        _step = GameObject.FindObjectOfType<Step>();

        if (_step.Steps[22])
            BookR.SetActive(false);
    }

    public void CheckTouch (string a)
    {
        if(a == "BookR")
        {
            _step.DoWork(22);
        }

    }

    public void Door0OPen()
    {
        PlaySound(SoundOutDream, VolumeOutDream);
        CanVolumeDown = true;
        PanelFadeWhite.SetActive(true);
        Fade2.SetBool("Show", true);
        StartCoroutine(OutDream(6.5f));
    }

    private void Update()
    {
        if (CanVolumeDown)
            VolumeDown();
    }

    IEnumerator OutDream (float wait)
    {
        yield return new WaitForSeconds(wait);
        _step.DoWork(36);
        load.gameObject.SetActive(true);
        load.Interact();
    }

    public void PlaySound(AudioClip clip, float volume)
    {
        SoundPlayer.clip = clip;
        SoundPlayer.volume = volume;
        SoundPlayer.Play();
    }

    void VolumeDown()
    {
        if (music.volume >= 0)
        {
            music.volume -= SpeedVolumeDown * Time.deltaTime;
        }

        if (music2.volume >= 0)
        {
            music2.volume -= SpeedVolumeDown * Time.deltaTime;
        }
    }

}
