using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AdjustVolume : MonoBehaviour
{
    public AudioMixer audioController;
    public Slider volSlider;

    [HideInInspector]
    public float sliderValue;

    private void Start()
    {
        SetVolumeLevel(0.5f);
    }


    public void SetVolumeLevel(float sliderAdjust)
    {
        sliderValue = sliderAdjust;
        audioController.SetFloat("musicVol", Mathf.Log10(sliderValue) * 20);
        //decibels is a logarithmic value, slider technically goes between -80 and 0, on a log scale,
        //not a linear one 
        volSlider.value = sliderValue;
    }

    public float GetVolumeLevel()
    {
        return sliderValue;
    }
}
