﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Experimental.Rendering.Universal;

public class SettingsController : MonoBehaviour
{

    public AudioMixer MusicMixer;
    public AudioMixer SFXMixer;

    public Slider MusicSlider;
    public Slider SFXSlider;

    public Slider LightingSlider;

    public float MusicVolumeValue;
    public float SFXVolumeValue;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("mvolume"))
            MusicVolumeValue = PlayerPrefs.GetInt("mvolume");
        else
        {
            MusicVolumeValue = 1000;
            PlayerPrefs.SetInt("mvolume", (int) (MusicVolumeValue * 1000));
        }

        if (PlayerPrefs.HasKey("svolume"))
            SFXVolumeValue = PlayerPrefs.GetInt("svolume");
        else
        {
            SFXVolumeValue = 1000;
            PlayerPrefs.SetInt("svolume", (int) (SFXVolumeValue * 1000));
        }

        if (PlayerPrefs.HasKey("bright"))
        {
            LightController.GlobalIntesity = PlayerPrefs.GetFloat("bright");
            LightingSlider.value = LightController.GlobalIntesity;
        }
        else
        {
            LightingSlider.value = LightController.GlobalIntesity;
            PlayerPrefs.SetFloat("bright", LightController.GlobalIntesity);
        }

        MusicVolumeValue /= 1000;
        SFXVolumeValue /= 1000;
        OnVolumeChanged(MusicVolumeValue);
        OnVolumeChangedSFX(SFXVolumeValue);
        OnVolumeChangedLighting();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnVolumeChangedLighting()
    {
        LightController.GlobalIntesity = LightingSlider.value;
        PlayerPrefs.SetFloat("bright", LightController.GlobalIntesity);
        /*
        Light2D[] lights = FindObjectsOfType<Light2D>();

        foreach (Light2D light in lights)
        {
            if (light.lightType == Light2D.LightType.Global)
            {
                light.intensity = LightController.GlobalIntesity;
            }
        }
        */
    }

    public void OnVolumeChanged()
    {
        float sliderValue = MusicSlider.value;
        MusicMixer.SetFloat("volume", Mathf.Log(sliderValue) * 20);
        PlayerPrefs.SetInt("mvolume", (int) (sliderValue * 1000));
    }

    public void OnVolumeChangedSFX()
    {
        float sliderValue = SFXSlider.value;
        SFXMixer.SetFloat("volume", Mathf.Log(sliderValue) * 20);
        PlayerPrefs.SetInt("svolume", (int)(sliderValue * 1000));
    }

    public void OnVolumeChanged(float value)
    {
        MusicMixer.SetFloat("volume", Mathf.Log(value) * 20);
        MusicSlider.value = value;
        PlayerPrefs.SetInt("mvolume", (int) (value * 1000));
    }

    public void OnVolumeChangedSFX(float value)
    {
        SFXMixer.SetFloat("volume", Mathf.Log(value) * 20);
        SFXSlider.value = value;
        PlayerPrefs.SetInt("svolume", (int)(value * 1000));
    }
}
