using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMinus : MonoBehaviour
{
    public UserEnterWindowDuration windowDuration;
    public void OnClickMinus()
    {
        windowDuration.DecrementWindowTime();
    }
}
