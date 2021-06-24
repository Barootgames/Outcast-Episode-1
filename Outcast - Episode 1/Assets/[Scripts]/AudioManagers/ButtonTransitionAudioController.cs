using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTransitionAudioController : MonoBehaviour
{
    public MainMenuAudioController mainMenuAudioController;
    public AudioSource[] audioSources;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayStoryStartTransition()
    {
        PlaySFX(0);
    }

    public void PlayNewGameTransition()
    {
        PlaySFX(1);
    }

    public void PlayContinueTransition()
    {
        PlaySFX(2);
    }

    public void PlaySummaryTransition()
    {
        PlaySFX(3);
    }

    public void PlayGalleryTutorialTransition()
    {
        PlaySFX(4);
    }

    public void PlayCreditsTransition()
    {
        PlaySFX(5);
    }

    public void PlaySettingsTransition()
    {
        PlaySFX(6);
    }

    public void PlayQuitTransition()
    {
        PlaySFX(7);
    }

    void PlaySFX(int Id)
    {
        if(audioSources[Id].gameObject.activeInHierarchy)
            audioSources[Id].Play();
    }
}
