using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSink : MonoBehaviour
{
    SpreadGerms germSpreader;
    private AudioSource sinkSound;
    public ControlSubtitles subtitles; 
    

    private void Start()
    {
        germSpreader = GameObject.FindGameObjectWithTag("Player").GetComponent<SpreadGerms>();
        sinkSound = GetComponent<AudioSource>();
    }
    private void Update()
    {
        CleanSelf();
    }
    void CleanSelf()
    {
        if (germSpreader.GetSinkStatus() && germSpreader.GetInfectedStatus() && Input.GetKeyDown(KeyCode.C) && !sinkSound.isPlaying)
        {
           
          sinkSound.Play();
          if (SubtitleState.subtitlesOn)
            {
                string sinkAudioDescription = "[water runnning]";
                subtitles.SetSubtitleText(sinkAudioDescription);
            }
            //no longer spreading germs 
            germSpreader.SetInfectedStatus(false);
        }
    }

}
