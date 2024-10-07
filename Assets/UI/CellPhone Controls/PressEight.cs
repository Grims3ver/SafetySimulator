using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PressEight : MonoBehaviour
{

    public TextMeshProUGUI textBox;

    public void OnClick()
    {
        if (textBox.text.ToString().Length < 10)
        {
            textBox.text += "8";
        }
    }
}
