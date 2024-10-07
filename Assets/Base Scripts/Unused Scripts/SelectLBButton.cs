using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLBButton : MonoBehaviour
{

    public GameObject menu;
    public GameObject LBMenu;

    public void OnClick()
    {
        menu.SetActive(false);
        LBMenu.SetActive(true);
    }
}
