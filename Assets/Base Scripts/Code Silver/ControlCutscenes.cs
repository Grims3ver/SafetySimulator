using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;

public class ControlCutscenes : MonoBehaviour
{
    //control what is present in UI screen
    public ControlNotebook notebook;
    public Canvas infectedCanvas;
    public Canvas updateCanvas;
    public GameObject HUD;
    public CharacterMovement character;
   
    //control player status 
   // private PlayerMovement playerMovement;
    private SpreadGerms germStatus; 

    void Start()
    {
        germStatus = GameObject.Find("Player").GetComponent<SpreadGerms>();
    }
    public void PrepareCutsceneEnvironment()
    {
        notebook.DisableNotebookInterface();
        infectedCanvas.enabled = false;
        HUD.SetActive(false);
        updateCanvas.enabled = false;
        character.MovementForbidden = true;
        
    }

    public void PrepareGameEnvironment()
    {
        notebook.EnableNotebookInterface();
        //if infected
        if (germStatus.GetInfectedStatus())
        {
            infectedCanvas.enabled = true;
       } else
        {
            infectedCanvas.enabled = false; 
        }

        character.MovementForbidden = false; 

        HUD.SetActive(true);
      
    }
    
}
