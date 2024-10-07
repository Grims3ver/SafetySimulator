using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //slider
using MoreMountains.TopDownEngine; //character movement
using TMPro;

public class Gurney : MonoBehaviour
{

    //checks for settings  - if door can be unbarricaded, sprites to use
    public AllowBarricadeRemoval barricadeCheck;
    public HighlightLockedDoors doorsHighlighted;

    //necessary gameobjects
    public GameObject player;
    private GameObject[] doors;

    //door specific 
    private ControlSimpleDoor[] doorCheck;
    private SpriteRenderer[] doorRenderer;

    //gurney specific
    private SpriteRenderer gurneyRenderer;
    private BoxCollider2D gurneyCollider;

    //control door sprite changes
    public Sprite barricadedDoor;
    public Sprite unlockedDoor;
    public Sprite lockedDoorHighlighted; //depending on settings

    //create brief pause animation while barricading
    public Canvas pauseCanvas;
    public Slider barricadePauseSlider;
    public AudioSource doorBarricadeSound;
    public TextMeshProUGUI sliderText;
    private CharacterMovement playerMoveController;

    private int closestDoor = -1;
    private int playerNearby = -1; 

    //flags to set timining of actions
    private bool doorBarricadeComplete = false;
    private bool flipOnce = false; 

    //subtitles
    public ControlSubtitles subtitles;

    private static float barricadeTime = 0.2f; //defaults to 3 seconds total of time

    void Start()
    {
        //get necessary components
        gurneyRenderer = GetComponent<SpriteRenderer>();
        //collider is inactivated when object is used to barricade
        gurneyCollider = GetComponent<BoxCollider2D>();

        //necessary to control movement during pause
        playerMoveController = player.GetComponent<CharacterMovement>();

        doors = GameObject.FindGameObjectsWithTag("LockableDoor");
        doorCheck = new ControlSimpleDoor[doors.Length];
        doorRenderer = new SpriteRenderer[doors.Length];

        for (int i = 0; i < doors.Length; ++i)
        {
            doorCheck[i] = doors[i].GetComponent<ControlSimpleDoor>();
            doorRenderer[i] = doors[i].GetComponent<SpriteRenderer>();
        }

    }

   private void Update()
    {
        closestDoor = GurneyNearbyDoor();
        playerNearby = PlayerNearbyDoor();

        if (closestDoor != -1 && doorCheck[closestDoor].IsDoorLocked() && !doorCheck[closestDoor].doorIsBarricaded && !doorBarricadeComplete)
        {
            doorRenderer[closestDoor].sprite = barricadedDoor;
            //this sets the door to be inactive - once barricaded the door 
            //cannot be unbarricaded or interacted with 
            doorCheck[closestDoor].doorIsBarricaded = true;
            StartCoroutine("PauseWhileBarricadingDoor");
            gurneyRenderer.enabled = false;
            gurneyCollider.enabled = false;
            flipOnce = false;

        }
        //unbarricade
        else if (playerNearby != -1 && doorCheck[playerNearby].doorIsBarricaded && barricadeCheck.GetBarricadeDoorStatus() && doorBarricadeComplete && Input.GetKey(KeyCode.F))
        {
            //doors should be highlighted
            if (doorsHighlighted.GetHighlightDoorStatus())
            {
                doorRenderer[playerNearby].sprite = lockedDoorHighlighted;
            }
            else
            {
                doorRenderer[playerNearby].sprite = unlockedDoor;
            }
            doorCheck[playerNearby].ResetDoorToUnlocked(); //unlocked after barricade removal
            print(doorCheck[playerNearby].doorIsLocked);
            doorCheck[playerNearby].doorIsBarricaded = false;
            flipOnce = false; 
            StartCoroutine("PauseWhileUnBarricadingDoor");

               gurneyRenderer.enabled = true;
               gurneyCollider.enabled = true;
        } 



    }

    //provides a small pause and a visual loading bar to the player while they barricade the door
    IEnumerator PauseWhileBarricadingDoor()
    {
        //disable movement
        playerMoveController.MovementForbidden = true;
        sliderText.text = "Barricading Door";
        float increaseSlider = 0;
        pauseCanvas.enabled = true;

        doorBarricadeSound.Play();
        //subtitles
        if (SubtitleState.subtitlesOn)
        {
            subtitles.SetSubtitleText("[hammering sounds]");
        }

        //update the slider
        while (increaseSlider <= 1)
        {
            barricadePauseSlider.value = increaseSlider;
            increaseSlider += 0.07f;
            yield return new WaitForSeconds(barricadeTime);
        }

        pauseCanvas.enabled = false;
        //renable movement
        playerMoveController.MovementForbidden = false;

        StartCoroutine("PauseBeforeUnbarricadeOption");
        StopCoroutine("PauseWhileBarricadingDoor");

    }

    //a brief pause between barricading, also resets boolean flag
    IEnumerator PauseBeforeUnbarricadeOption()
    {
        yield return new WaitForSecondsRealtime(2f);
        if (!flipOnce)
        {
            doorBarricadeComplete = !doorBarricadeComplete;
            flipOnce = true; 
        }
        StopCoroutine("PauseBeforeUnbarricadeOption");
    }

    IEnumerator PauseWhileUnBarricadingDoor()
    {
        playerMoveController.MovementForbidden = true;
        sliderText.text = "Unbarricading Door";
        float increaseSlider = 1f;
        pauseCanvas.enabled = true;

        doorBarricadeSound.Play();
        //subtitles
        if (SubtitleState.subtitlesOn)
        {
            subtitles.SetSubtitleText("[scraping sounds]");
        }

        //update the slider
        while (increaseSlider >= 0)
        {
            barricadePauseSlider.value = increaseSlider;
            increaseSlider -= 0.07f;
            yield return new WaitForSeconds(barricadeTime);
        }

        pauseCanvas.enabled = false;
        //renable movement
        playerMoveController.MovementForbidden = false;
        StartCoroutine("PauseBeforeUnbarricadeOption");
        StopCoroutine("PauseWhileUnBarricadingDoor");
    }

    //
    public void SetBarricadeTime(float userTime)
    {
        if (userTime >= 0 && userTime != barricadeTime)
        {
            barricadeTime = userTime;
        }
        //convert to per-iteration value
        barricadeTime = barricadeTime / 15; // why 15? because the 0.07f provides 15 intervals for the bar to move between 0-1
        //why 0.07? because I felt it looks the prettiest and most even when the bar is moving 
    }


    //determines if a gurney is nearby a door
    private int GurneyNearbyDoor()
    {
        for (int i = 0; i < doors.Length; ++i)
        {
            //if doors are near a gurney
            if ((Mathf.Abs(doors[i].transform.position.x - this.transform.position.x) < 1) && (Mathf.Abs(doors[i].transform.position.y - this.transform.position.y) < 1))
            {
                return i;
            }

        }

        return -1;

    }

    private int PlayerNearbyDoor()
    {
        for (int i = 0; i < doors.Length; ++i)
        {
            //if player near door
            if ((Mathf.Abs(doors[i].transform.position.x - player.transform.position.x) < 2) && (Mathf.Abs(doors[i].transform.position.y - player.transform.position.y) < 2))
            {
                return i;
            }

        }

        return -1;
    }
}
