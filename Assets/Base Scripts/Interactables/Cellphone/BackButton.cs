using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    public GameObject menu;
    public GameObject lbMenu;

    public void OnClick()
    {
        lbMenu.SetActive(false);
        menu.SetActive(true);
    }

    

   
}
