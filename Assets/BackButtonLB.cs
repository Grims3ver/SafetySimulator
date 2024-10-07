using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButtonLB : MonoBehaviour
{
    public GameObject LBMenu;
    public GameObject MainMenu;
   public void OnClickBackLB()
    {
        LBMenu.SetActive(false);
        MainMenu.SetActive(true);
    }
}
