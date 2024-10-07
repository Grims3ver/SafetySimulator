using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPlusBarricade : MonoBehaviour
{
   public UserEnterBarricadeDuration barricade;
    public void OnClickPlusBarricade()
    {
        barricade.IncrementBarricadeTime();
    }
}
