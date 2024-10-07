using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System; 

public class ControlSubtitles : MonoBehaviour
{
    public TextMeshProUGUI subtitleText;
    public Canvas subtitleCanvas;
    private bool subtitleIsActive;

    private void Awake()
    {
        subtitleCanvas.enabled = false;
        subtitleIsActive = false; 
    }
    public void SetSubtitleText(string newText)
    {
        if (!newText.Equals(null))
        {
            if (subtitleIsActive)
            {
                StopCoroutine("ShowSubtitleBriefly");
                subtitleText.text = newText;
                StartCoroutine("ShowSubtitleBriefly");

            }
            else if (!subtitleIsActive)
            {
                subtitleText.text = newText;
                StartCoroutine("ShowSubtitleBriefly");
            }
        }

        
        
    }

    IEnumerator ShowSubtitleBriefly()
    {
        subtitleCanvas.enabled = true;
        subtitleIsActive = true; 
        yield return new WaitForSecondsRealtime(3);
        subtitleCanvas.enabled = false;
        subtitleIsActive = false;
        StopCoroutine("ShowSubtitleBriefly");

    }
}
