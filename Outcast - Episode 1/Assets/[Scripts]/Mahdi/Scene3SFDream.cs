using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene3SFDream : MonoBehaviour
{

    private Step _step;
    [SerializeField] private GameObject Trigger3;
    [SerializeField] private AudioSource soundPlayer;

    [SerializeField] private AudioClip sound1;
    [Range(0,1)] [SerializeField] private float Volume1 = 0.5f;

    void Start()
    {
        #region Step

        _step = GameObject.FindObjectOfType<Step>();

        if (_step.Steps[16] && !_step.Steps[17])
                Trigger3.SetActive(true);

        #endregion
    }


    void Update()
    {
        

    }

    public void CheckTrigger (string a)
    {
        if(a == "Trigger3" && !_step.Steps[17])
        {
            PlaySound(sound1, Volume1);
            _step.DoWork(17);
            FindObjectOfType<GameDataController>().gameData.SetGameEventAsFinished("DreamDoorSound");
        }
    }

    private void PlaySound(AudioClip clip, float volume)
    {
        soundPlayer.clip = clip;
        soundPlayer.volume = volume;
        soundPlayer.Play();
    }
}
