using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitleState : MonoBehaviour
{
    public static bool subtitlesOn = true;
    
    public void OnSubtitleClick()
    {
        if (subtitlesOn)
        {
            subtitlesOn = false; 
        } else
        {
            subtitlesOn = true;
        }
        
    }
}
