using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using MoreMountains.TopDownEngine;


public class ControlPrebrief_NoTimer : MonoBehaviour
{
    //prebrief is responsible for disabling the appropriate canvases, so it has many references
    private Canvas preBriefCanvas;
    public ControlNotebook notebookController;
    public Canvas cellPhone;
    public Canvas codex;
    public Canvas noteBook;
    public Canvas settings;
    public Canvas infectedCanvas;

    public GameObject HUD; //part of the engine system, disabled during menus

    public TextMeshProUGUI prebriefTextbox;

    //this bool flags whether the prebrief has been read or not, does not need to appear in inspector
    [HideInInspector]
    public bool prebriefRead = false;
    private Scene currentScene;
    public CharacterMovement controller;

    private int clickCount;
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        preBriefCanvas = GetComponent<Canvas>();

        preBriefCanvas.enabled = true;

        // notebook.DisableNotebookInterface();


        controller.MovementForbidden = true;
        //disable all unneeded canvases

        noteBook.enabled = false;
        cellPhone.enabled = false;
        codex.enabled = false;
        settings.enabled = false;
        infectedCanvas.enabled = false;
        clickCount = 0;
        HUD.SetActive(false); //set HUD inactive
    }

    public void OnContinueClick()
    {
        if (currentScene.name.Equals("Code_Silver"))
        {
            preBriefCanvas.enabled = false;
            settings.enabled = true; 

        }
        else if (currentScene.name.Equals("Code_S_Hide") && clickCount == 0)
        {

            controller.MovementForbidden = true;
            ++clickCount;
            prebriefTextbox.text = "You will have a limited amount of time to successfully perform all the actions needed to HIDE. This means silencing loud object, locking doors, covering windows, and hiding the player. " +
                "The notebook will have tips if you get stuck.";
        }
        else if (currentScene.name.Equals("Code_S_Hide") && clickCount == 1)
        {
            preBriefCanvas.enabled = false;
            settings.enabled = true;

        }
    }

    public bool GetPreBriefStatus() { return prebriefRead; }



}
