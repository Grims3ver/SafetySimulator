using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressVolume : MonoBehaviour
{

    private Image[] volumeOnImage;

    public bool volumeOn;
    private Sprite temp;

    public Sprite volumeOff;


    void Start()
    {
        volumeOnImage = GetComponentsInChildren<Image>();
        volumeOn = true; //volume defaults to on 
        temp = volumeOnImage[1].sprite; //store the original volume on as a sprite
    }

    public void OnClick()
    {
        if (volumeOn)
        {
            //reset the image
            volumeOnImage[1].sprite = volumeOff;
            volumeOn = false;
        } else if (!volumeOn)
        {
            //reset the image
            volumeOnImage[1].sprite = temp;
            volumeOn = true;
        }
    }

    //for later functionality
    public bool GetSoundStatus() { return volumeOn; }
}
