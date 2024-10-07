using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMinusTimer : MonoBehaviour
{
    public UserEnterLevelDuration level;
    public void OnClickMinusTimer()
    {
        level.DecrementLevelTime();
    }
}
