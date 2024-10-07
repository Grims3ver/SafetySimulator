using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using MoreMountains.TopDownEngine;

public class PhoneCutsceneBase : MonoBehaviour
{
    //timeline
    public Canvas codeCanvas;
    public ControlUpdateCanvas displayTooltip;

    public ControlHealth playerHealth; //used to determine if the player is attacked mid-cutscene, in which case the cutscene should end (but need repeating)
    private float playerHealthValue;
    private bool cutsceneCurrentlyPlaying = false; 

    private PlayableDirector director;
    private AudioSource alarmSound;
    private GameObject player;

    //simple flag
    bool check = false;
    private bool cutscenePlayed = false;
    private bool cutsceneOver = false;

    public ControlCutscenes cutscene;
    public GameObject endLevelDoor; //only active after player finishes cutscene

    
    private CharacterMovement mover; 

    //Cutscene controllers

    void Awake()
    {
        director = GameObject.Find("PhoneCutsceneController").GetComponent<PlayableDirector>();
        player = GameObject.Find("Player");
        mover = player.GetComponent<CharacterMovement>();
        endLevelDoor.SetActive(false);

        alarmSound = codeCanvas.GetComponent<AudioSource>();
        codeCanvas.enabled = false;

        if (director)
        {
            check = true;
        }

        playerHealthValue = playerHealth.GetHealth();
        
    }
    private void Update()
    {
        bool playerNearby = CheckPlayerProximity();

        if (playerNearby && Input.GetKeyDown(KeyCode.F) && check && !cutscenePlayed)
        {
            //disable UI items + enable bars 
            director.Play();
            cutsceneCurrentlyPlaying = true;
            cutscene.PrepareCutsceneEnvironment();
            
            StartCoroutine("WaitForPhoneCutSceneToEnd");

            cutscenePlayed = true;
        }
        //if player takes damage during cutscene
        if (cutsceneCurrentlyPlaying && playerHealthValue != playerHealth.GetHealth())
        {
            StopCoroutine("WaitForPhoneCutSceneToEnd");
            cutsceneCurrentlyPlaying = false;
            cutsceneOver = false;
            cutscene.PrepareGameEnvironment();
        }

        if (cutsceneOver)
        {
            StopCoroutine("WaitForPhoneCutSceneToEnd");
            mover.MovementForbidden = false;
            cutsceneCurrentlyPlaying = false;
            endLevelDoor.SetActive(true);
            cutscene.PrepareGameEnvironment();
        }
    }

    public IEnumerator WaitForPhoneCutSceneToEnd()
    {
        yield return new WaitForSeconds((float)director.duration); //cutscene is 25.666 seconds
        codeCanvas.enabled = true;
        alarmSound.Play();
        yield return new WaitForSeconds(4f);
        codeCanvas.enabled = false;
        print("HERE!");
        cutsceneOver = true;
    }

    private bool CheckPlayerProximity()
    {
        //if player distance from emergency phone is less than = one
        //transform.position = position of the phone 
        if (Vector3.Distance(transform.position, player.transform.position) <= 2)
        {
            if (!displayTooltip.emergCoroutineRunning)
            {
                displayTooltip.StartCoroutine("ShowEmergencyPhoneTip");
            }
            return true;
        }

        return false;
    }

    public bool CheckPhoneCutsceneOver() { return cutsceneOver; }

}