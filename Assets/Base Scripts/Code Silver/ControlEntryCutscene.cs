using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using MoreMountains.TopDownEngine;
public class ControlEntryCutscene : MonoBehaviour
{
    private PlayableDirector director;

    //cutscene
    public ControlCutscenes controller;

    //enemy
    public GameObject cutsceneEnemy;

    public GameObject realEnemy; //this is the actual pathed enemy that takes the place of the cutscene one.

    [HideInInspector]
    public bool cutscenePlayed;
    private bool cutsceneIsPlaying; //is the cutscene playing right now?

    //control play time (after prebrief)
    public ControlPrebrief_NoTimer pBController;

    public GUIManager manager;

    public GameObject dialogParent; //parent for all dialogs

    private bool pauseActive = false; 

    // Start is called before the first frame update
    void Start()
    {
        director = GetComponent<PlayableDirector>();
        cutscenePlayed = false;
        cutsceneIsPlaying = false; 
    }

    void Update()
    {
        //if the prebrief is read
        if (pBController.GetPreBriefStatus() && !cutscenePlayed)
        {
            controller.PrepareCutsceneEnvironment();
            StartCoroutine("WaitForCutSceneToEnd");
            director.Play();
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseActive = !pauseActive;
        }


        if (cutscenePlayed)
        {
            StopCoroutine("WaitForCutSceneToEnd");
            cutsceneEnemy.SetActive(false); //disable cutscene enemy
            realEnemy.SetActive(true);

        }
    }

    private void FixedUpdate()
    {

       

        //pause screen is active
        if (cutsceneIsPlaying && dialogParent.gameObject.activeSelf && pauseActive)
        {
            dialogParent.SetActive(false); // no blocking dialog boxes
        }
        else if (cutsceneIsPlaying && !dialogParent.gameObject.activeSelf && !pauseActive)
        {
            dialogParent.SetActive(true); // no blocking dialog boxes
        }
    }

    public IEnumerator WaitForCutSceneToEnd()
    {
        cutsceneIsPlaying = true; 
        yield return new WaitForSeconds((float)director.duration);
        cutscenePlayed = true; 

        controller.PrepareGameEnvironment();
    }
}
