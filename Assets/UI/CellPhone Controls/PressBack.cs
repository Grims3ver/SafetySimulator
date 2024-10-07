using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PressBack : MonoBehaviour
{
    public TextMeshProUGUI textBox;

    public void OnClick()
    {
        
        string sampleText = textBox.text.ToString();
        if (sampleText.Length > 0)
        {
            textBox.text = sampleText.Substring(0, sampleText.Length - 1);
        }


    }
}
