using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject levelSelectMenu;
    
    public AdjustVolume adjuster;

    private void Awake()
    {
        if (adjuster.sliderValue != adjuster.volSlider.value)
        {
            adjuster.SetVolumeLevel(adjuster.sliderValue);
        }
    }
    private void Start()
    {
        levelSelectMenu.SetActive(false);
    }
    public void PlayGame()
    {
        //move to the next scene in queue
        levelSelectMenu.SetActive(true);
        this.gameObject.SetActive(false);

    }

    public void QuitGame()
    {
        //tracer code to ensure that this is being called - quit does not work inside unity testing
        //print("Quit");
        //quit the game 
        Application.Quit(); 
    }
}
