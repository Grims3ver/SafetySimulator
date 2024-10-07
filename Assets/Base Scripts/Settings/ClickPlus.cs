using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClickPlus : MonoBehaviour
{
    public UserEnterWindowDuration windowDuration;
    public void OnClickPlus()
    {
        windowDuration.IncrementWindowTime();
    }
  

}
