using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMinusBarricade : MonoBehaviour
{
    public UserEnterBarricadeDuration barricade;
    public void OnClickMinusBarricade()
    {
        barricade.DecrementBarricadeTime();
    }
}
