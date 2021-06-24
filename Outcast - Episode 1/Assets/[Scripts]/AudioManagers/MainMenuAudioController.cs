using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAudioController : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip[] AudioClips; //0 whoosh, 1 back button, 2 click other buttons, 3 new game
    public AudioSource MusicAudioSource;
    public AudioSource SFXAudioSource;
    public AudioSource SFXAudioSource2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMenuWhoosh()
    {
        PlaySFX(0);
    }

    public void PlayMenuBackButton()
    {
        PlaySFX(1);
    }

    public void PlayStartStoryClick()
    {
        PlaySFX(3);
        MusicAudioSource.Stop();
    }

    public void PlayClickSound1()
    {
        PlaySFX2(2);
    }

    void PlaySFX(int clipId)
    {
        SFXAudioSource.Stop();
        SFXAudioSource.clip = AudioClips[clipId];
        SFXAudioSource.Play();
    }
    void PlaySFX2(int clipId)
    {
        SFXAudioSource2.Stop();
        SFXAudioSource2.clip = AudioClips[clipId];
        SFXAudioSource2.Play();
    }
}
