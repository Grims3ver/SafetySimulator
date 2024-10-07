using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LBMenuButtons : MonoBehaviour
{
    public void OnClickExit()
    {
        Application.Quit();
    }

    public void OnClickReturn()
    {
        SceneManager.LoadScene("Menu");
    }
    
}
