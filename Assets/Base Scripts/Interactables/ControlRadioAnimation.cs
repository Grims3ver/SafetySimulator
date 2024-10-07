using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlRadioAnimation : MonoBehaviour
{
    public ControlUpdateCanvas toolTip;
    public ControlSubtitles subtitles;

    //sounds
    private AudioSource radioSound;

    // private AudioSource[] radioSound;

    //animator
    private Animator radioAnimator;

    // private Animator[] radioAnimator;

    //player
    private Transform player;

    //sprites
    public Sprite radioOffSprite; //the default
    public Sprite radioOnSprite; //a sprite that is on
    private SpriteRenderer radioRenderer;

    //if a radio is near 
    private bool radioNearby;

    //if radio is off, defaults to false
    [HideInInspector]
    public bool radioOff;


    void Start()
    {

        radioAnimator = GetComponent<Animator>();
        radioRenderer = GetComponent<SpriteRenderer>();
        radioSound = GetComponent<AudioSource>();

        player = GameObject.FindWithTag("Player").transform;
        radioOff = false;
    }

    void Update()
    {

        radioNearby = CheckProximityOfLoudObjects();

        if (radioNearby && !radioSound.isPlaying && !radioOff)
        {
            if (SubtitleState.subtitlesOn)
            {
                string radioSoundDescription = "[guitar strumming]";
                subtitles.SetSubtitleText(radioSoundDescription);
            }
            radioSound.Play();

        }
        else if (radioNearby && Input.GetKeyDown(KeyCode.F) && !radioOff)
        {
            radioRenderer.sprite = radioOffSprite;
            radioAnimator.enabled = false;
            radioOff = true;
            radioSound.Stop();

            if (SubtitleState.subtitlesOn)
            {
                string radioSoundDescriptionOff = "[guitar strumming stops]";
                subtitles.SetSubtitleText(radioSoundDescriptionOff);
            }


        }
        else if (radioNearby && Input.GetKeyDown(KeyCode.F) && radioOff)
        {
            radioRenderer.sprite = radioOnSprite;
            radioAnimator.enabled = true;
            radioOff = false;

            if (SubtitleState.subtitlesOn)
            {
                string radioSoundDescriptionOff = "[guitar strumming]";
                subtitles.SetSubtitleText(radioSoundDescriptionOff);
            }
        }
    }

    public bool CheckProximityOfLoudObjects()
    {

        if (Vector3.Distance(player.position, this.transform.position) <= 2)
        {
            if (!toolTip.radioCoroutineRunning && !radioOff)
            {
                toolTip.SetRadioTip("Press 'F' to silence the radio.");
                toolTip.StartCoroutine("ShowRadioTip");
            } else if (!toolTip.radioCoroutineRunning && radioOff)
            {
                toolTip.SetRadioTip("Press 'F' to turn on the radio.");
                toolTip.StartCoroutine("ShowRadioTip");
            }

            return true;
        }



        return false;
    }

}
