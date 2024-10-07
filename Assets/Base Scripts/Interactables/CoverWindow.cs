using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.TopDownEngine;
using TMPro;
public class CoverWindow : MonoBehaviour
{
    //for window pause - action takes a few seconds
    //and fills a slider while the player waits
    public Canvas pauseCanvas;
    public Slider windowPauseSlider;
    public AudioSource windowBlindSound;
    public TextMeshProUGUI pauseText;
    private CharacterMovement playerMoveController;

    //subtitles & tooltips
    public ControlSubtitles subtitles;
    public ControlUpdateCanvas toolTip;

    //new sprite for covered window
    public Sprite coveredWindow;
    public Sprite uncoveredWindow;

    //get renderer
    private SpriteRenderer windowSprite;

    //player
    private Transform player;

    //determines if window is covered
    [HideInInspector]
    public bool windowCovered;

    //set's time of window covering, static to apply to all instances of script automatically when changed
    static private float windowCoverTime = 0.1f; //defaults to 0.1f

    //controls pauses between actions
    private bool pauseActive = false;
    private bool flipOnce = false; 


    private void Start()
    {
        windowSprite = GetComponent<SpriteRenderer>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerMoveController = player.GetComponent<CharacterMovement>();
        pauseCanvas.enabled = false;

        //by default windows are not covered
        windowCovered = false;
    }
    private bool CheckProximityOfWindows()
    {
        if (Vector3.Distance(player.position, this.transform.position) <= 3)
        {
            if (!toolTip.windowCoroutineRunning && !windowCovered)
            {
                toolTip.SetWindowTip("Press 'F' to cover the window.");
                toolTip.StartCoroutine("ShowWindowTip");
            } else if (!toolTip.windowCoroutineRunning && windowCovered)
            {
                toolTip.SetWindowTip("Press 'F' to uncover the window.");
                toolTip.StartCoroutine("ShowWindowTip");
            }
            
            return true;
        }
        return false;
    }


    IEnumerator PauseWhileCoveringWindow()
    {
        //set up
         playerMoveController.MovementForbidden = true;
        float increaseSlider = 0;
        pauseCanvas.enabled = true;
        pauseText.text = "Covering Window";

        //play sound
        windowBlindSound.Play();

        if (SubtitleState.subtitlesOn)
        {
            subtitles.SetSubtitleText("[window blinds closing]");
        }
        
       //fill slider while we wait
        while (increaseSlider <= 1)
        {
            windowPauseSlider.value = increaseSlider;
            increaseSlider += 0.07f;
            yield return new WaitForSecondsRealtime(windowCoverTime);
        }

        //yield return new WaitForSeconds(2.5f);
        pauseCanvas.enabled = false;

        playerMoveController.MovementForbidden = false;
        windowCovered = true; 
        StartCoroutine("PauseBetweenWindowActions");
        StopCoroutine("PauseWhileCoveringWindow");

    }

    IEnumerator PauseWhileUncoveringWindow()
    {
        //set up
        playerMoveController.MovementForbidden = true;
        float increaseSlider = 1;
        pauseCanvas.enabled = true;
        pauseText.text = "Uncovering Window";

        //play sound
        windowBlindSound.Play();

        if (SubtitleState.subtitlesOn)
        {
            subtitles.SetSubtitleText("[window blinds opening]");
        }

        //fill slider while we wait
        while (increaseSlider >= 0)
        {
            windowPauseSlider.value = increaseSlider;
            increaseSlider -= 0.07f;
            yield return new WaitForSecondsRealtime(windowCoverTime);
        }

        //yield return new WaitForSeconds(2.5f);
        pauseCanvas.enabled = false;
        windowCovered = false; 
        playerMoveController.MovementForbidden = false;
       
        StartCoroutine("PauseBetweenWindowActions");
    
        StopCoroutine("PauseWhileUncoveringWindow");
    }

    IEnumerator PauseBetweenWindowActions()
    {
        yield return new WaitForSecondsRealtime(2f);
       
        if (!flipOnce)
        {
            pauseActive = !pauseActive;
            flipOnce = true; 
        }
        StopCoroutine("PauseBetweenWindowActions");
    }
    public void SetCoverWindowTime(float userTime)
    {
        if (userTime >= 0 && userTime != windowCoverTime)
        {
            windowCoverTime = userTime;
        }
        windowCoverTime = windowCoverTime / 15; // why 15? because the 0.07f provides 15 intervals for the bar to move between 0-1
    }

    private void Update()
    {

        bool windowNearby = CheckProximityOfWindows();
       

        if (Input.GetKey(KeyCode.F) && windowNearby && !windowCovered && !pauseActive)
        {
            StartCoroutine("PauseWhileCoveringWindow");
            windowSprite.sprite = coveredWindow; //cover the window for safety!  
            flipOnce = false; 

            //uncover option, was added later - users can also uncover the window
        } else if (Input.GetKey(KeyCode.F) && windowNearby && windowCovered && pauseActive)
        {
            flipOnce = false;
            StartCoroutine("PauseWhileUncoveringWindow");
            windowSprite.sprite = uncoveredWindow;
        }
    }




}
