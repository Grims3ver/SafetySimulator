using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlSimpleDoor : MonoBehaviour
{

    //check for settings with door barricade
    public AllowBarricadeRemoval barricadeSettings;

    //audio 
    public AudioSource doorLock;
    public AudioSource doorUnlock;
    public ControlSubtitles subtitles;
    //tooltip
    public ControlUpdateCanvas toolTip;
    //sprites & sprite-change checker
    public HighlightLockedDoors shouldDoorHighlight; //should the door be highlighted?
    public Sprite lockedDoor; //different sprite when the door is locked AND appropriate setting is selected
    private SpriteRenderer doorImage; //the default image of the unlocked door
    private Sprite temp; //holder

    //flags
    [HideInInspector]
    public bool doorIsLocked = false;
    private bool colliding = false;

    //this is set when the door is barricaded
    [HideInInspector]
    public bool doorIsBarricaded;

    private void Start()
    {
        doorImage = GetComponent<SpriteRenderer>();
        temp = doorImage.sprite; //holder
        doorIsBarricaded = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            colliding = true;
            //  print("colliding!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            colliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            colliding = false;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            colliding = false;
        }
        //  print("not colliding anymore");


    }

    private void Update()
    {
        //door is nearby, not barricaded, and locked
        if (colliding && !toolTip.doorCoroutineRunning && !doorIsBarricaded && doorIsLocked)
        {
            toolTip.SetDoorTip("Press 'F' to unlock the door.");
            toolTip.StartCoroutine("ShowDoorTip");
            // door is nearby, not barricaded, and unlocked
        }
        else if (colliding && !toolTip.doorCoroutineRunning && !doorIsBarricaded && !doorIsLocked)
        {
            toolTip.SetDoorTip("Press 'F' to lock the door.");
            toolTip.StartCoroutine("ShowDoorTip");

            //door is barricaded
        }
        //door is allowed to be unbarricaded
        else if (colliding && !toolTip.barricadedDoorCoroutineRunning && doorIsBarricaded && barricadeSettings.GetBarricadeDoorStatus())
        {
            toolTip.SetBarricadeTip("Press 'F' to unbarricade the door.");
            toolTip.StartCoroutine("ShowBarricadedDoorTip");
        }
        //door may not be unbarricaded
        else if (colliding && !toolTip.barricadedDoorCoroutineRunning && doorIsBarricaded && !barricadeSettings.GetBarricadeDoorStatus())
        {
            toolTip.SetBarricadeTip("The door is barricaded.");
            toolTip.StartCoroutine("ShowBarricadedDoorTip");
        }

        //controls locking / unlock mechanisms 

        if (colliding && Input.GetKeyDown(KeyCode.F) && !doorIsLocked && !doorIsBarricaded) //door is currently unlocked
        {
            //only change the sprite if the highlight option is selected
            if (shouldDoorHighlight.GetHighlightDoorStatus())
            {
                doorImage.sprite = lockedDoor; //change sprite to show locked status
            }
            doorIsLocked = true; //door is now locked
            GetComponent<BoxCollider2D>().isTrigger = false; //door is locked, can't walk through
            doorLock.Play();
            this.gameObject.layer = 8; //the door is now in thte obstacles layer and should act as a wall for enemies

            if (SubtitleState.subtitlesOn)
            {
                string doorLockDescrip = "[door locking]";
                subtitles.SetSubtitleText(doorLockDescrip);
            }

            //the check for the barricaded door is to enusre that barricaded doors cannot be unbarricaded
        }
        else if (colliding && Input.GetKeyDown(KeyCode.F) && doorIsLocked && !doorIsBarricaded)
        {
            doorImage.sprite = temp;
            doorIsLocked = false; //door is now unlocked
            GetComponent<BoxCollider2D>().isTrigger = true; //door is unlocked, can walk through
            doorUnlock.Play();

            this.gameObject.layer = 0; //return to default layer

            if (SubtitleState.subtitlesOn)
            {
                string doorUnlockDescrip = "[door unlocking]";
                subtitles.SetSubtitleText(doorUnlockDescrip);
            }

        }

    }

    public void ResetDoorToUnlocked()
    {
        doorIsLocked = false; //door is now unlocked
        doorIsBarricaded = false;
        GetComponent<BoxCollider2D>().isTrigger = true; //door is unlocked, can walk through
        doorImage.sprite = temp;
    }

    public bool IsDoorLocked() { return doorIsLocked; }


}
