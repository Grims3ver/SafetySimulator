using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuLB : MonoBehaviour
{
    public Canvas pauseMenu;

    private void Awake()
    {
        pauseMenu.enabled = false;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pauseMenu.enabled)
        {
            pauseMenu.enabled = true;
        } else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.enabled)
        {
            pauseMenu.enabled = false;
        }
    }
}
