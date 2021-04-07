using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WC : MonoBehaviour
{
    [SerializeField] private AudioClip SoundOpenCabinet;
    [SerializeField] [Range(0, 1)] private float VolumeOpenCabinet = 0.5f;

    [SerializeField] private GameObject Tape;
    [SerializeField] private GameObject Cabinet;
    [SerializeField] private GameObject Cabinet2;

    [SerializeField] private GameObject openCabinet;
    [SerializeField] private GameObject CloseCabinet;

    [SerializeField] private GameObject openCabinet2;
    [SerializeField] private GameObject CloseCabinet2;

    [SerializeField] private AudioSource SoundPlayer;

    private Step _step;

    void Start()
    {
        _step = GameObject.FindObjectOfType<Step>();

        if(_step.Steps[21])
        {
            Tape.SetActive(false);
        }

        if(_step.Steps[35])
        {
            openCabinet.SetActive(true);
            CloseCabinet.SetActive(false);
            Cabinet.SetActive(false);
        }

        if(_step.Steps[34])
        {
            openCabinet2.SetActive(true);
            CloseCabinet2.SetActive(false);
            Cabinet2.SetActive(false);
        }

        if(openCabinet.activeInHierarchy && !_step.Steps[21])
        {
            Tape.SetActive(true);
        }
    }

    public void CheckTouch(string a)
    {
        if(a == "Cabinet" && !openCabinet.activeInHierarchy)
        {
            OpenCabinet();
            _step.DoWork(35);
        }

        if (a == "Cabinet2" && !openCabinet2.activeInHierarchy && !_step.Steps[34])
        {
            OpenCabinet2();
            _step.DoWork(34);
        }

        if (a == "Tape")
        {
            _step.DoWork(21);
        }
    }

    private void OpenCabinet ()
    {
        openCabinet.SetActive(true);
        CloseCabinet.SetActive(false);
        Tape.SetActive(true);
        PlaySound(SoundOpenCabinet, VolumeOpenCabinet);
    }

    private void OpenCabinet2()
    {
        openCabinet2.SetActive(true);
        CloseCabinet2.SetActive(false);
        PlaySound(SoundOpenCabinet, VolumeOpenCabinet);
    }

    public void PlaySound(AudioClip clip, float volume)
    {
        SoundPlayer.clip = clip;
        SoundPlayer.volume = volume;
        SoundPlayer.Play();
    }
}
