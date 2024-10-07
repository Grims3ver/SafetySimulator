using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTV : MonoBehaviour
{
    //tooltip and subtitles
    public ControlUpdateCanvas toolTip;
    public ControlSubtitles subtitles;

    //the sprite when the tv is off
    public Sprite tvOffSprite;
    public Sprite tvOnSprite;

    //whether tv is on or off, on by default
    [HideInInspector]
    public bool isOn = true;

    //audio
    private AudioSource tvAudio;

    //animator
    private Animator tvAnimator;

    //sprite renderer
    private SpriteRenderer tvSprite;

    //player
    private Transform player;

    void Start()
    {
        //get our components
        tvAudio = GetComponent<AudioSource>();
        tvAnimator = GetComponent<Animator>();
        tvSprite = GetComponent<SpriteRenderer>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    private bool CheckPlayerProximity()
    {
        //if player distance from doors is less than = to one (closer than germs)
        if (Vector3.Distance(player.position, this.transform.position) <= 2)
        {
            //show the tooltip if the player gets close
            if (!toolTip.tvCoroutineRunning && isOn)
            {
                toolTip.StartCoroutine("ShowTVTip");
            }

            //play audio when player approaches
            if (!tvAudio.isPlaying && isOn)
            {
                tvAudio.Play();
                //if subtitles are on 
                if (SubtitleState.subtitlesOn)
                {
                    subtitles.SetSubtitleText("[tv noise playing]");
                }
            }

            return true; 
        }

        return false;

    }

    void Update()
    {
        bool nearbyTV = CheckPlayerProximity();

        if (nearbyTV && Input.GetKeyDown(KeyCode.F) && isOn)
        {
            tvSprite.sprite = tvOffSprite;
            tvAnimator.enabled = false;
            isOn = false;
          
            tvAudio.Stop();

            if (SubtitleState.subtitlesOn)
            {
                subtitles.SetSubtitleText("[tv noise stops]");
            }
        } else if (nearbyTV && Input.GetKeyDown(KeyCode.F) && !isOn)
        {
            tvSprite.sprite = tvOnSprite;
            tvAnimator.enabled = true;
            isOn = true;

            tvAudio.Play();

            if (SubtitleState.subtitlesOn)
            {
                subtitles.SetSubtitleText("[tv noise playing]");
            }
        }

    }
}
