using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ControlTutorialButton : MonoBehaviour
{
    public Image tutorialBackground;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI bodyText;
 
   public void OnClick()
    {
        tutorialBackground.enabled = !tutorialBackground.enabled;
        titleText.enabled = !titleText.enabled;
        bodyText.enabled = !bodyText.enabled;
    }
}
