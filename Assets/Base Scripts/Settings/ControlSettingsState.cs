using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //get scene

public class ControlSettingsState : MonoBehaviour
{
    public GameObject timerSettings;
    private Scene currentScene;
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentScene.name.Equals("Code_Silver") && timerSettings.activeSelf)
        {
            timerSettings.SetActive(false);
        }
    }
}
