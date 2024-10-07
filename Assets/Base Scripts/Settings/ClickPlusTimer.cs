using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPlusTimer : MonoBehaviour
{
    public UserEnterLevelDuration level;
    public void OnClickPlusTimer()
    {
        level.IncrementLevelTime();
    }
}
