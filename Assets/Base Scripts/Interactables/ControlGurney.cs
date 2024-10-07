using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //slider
using MoreMountains.TopDownEngine; //character movement
using TMPro;

public class ControlGurney : MonoBehaviour
{

    //necessary gameobjects
    public GameObject[] gurneys;
    public GameObject player;
    private GameObject[] doors;
    
    //door specific 
    private ControlSimpleDoor[] doorCheck;
    private SpriteRenderer[] doorRenderer;

    //gurney specific
    private SpriteRenderer[] gurneyRenderer;
    private BoxCollider2D[] gurneyCollider;

    //control sprite changes
    public Sprite barricadedDoor;
    //current behaviour: once a door is barricaded it cannot be unbarricaded
    [HideInInspector]
    public bool[] isBarricaded;

    //create brief pause animation while barricading
    public Canvas pauseCanvas;
    public Slider barricadePauseSlider;
    public AudioSource doorBarricadeSound;
    public TextMeshProUGUI sliderText; 
    private CharacterMovement playerMoveController;

    //subtitles
    public ControlSubtitles subtitles;


    // Start is called before the first frame update
    void Start()
    {
        gurneys = GameObject.FindGameObjectsWithTag("Gurney");
        gurneyRenderer = new SpriteRenderer[gurneys.Length];
        gurneyCollider = new BoxCollider2D[gurneys.Length];

        for (int i = 0; i < gurneys.Length; ++i)
        {
            gurneyRenderer[i] = gurneys[i].GetComponent<SpriteRenderer>();
            gurneyCollider[i] = gurneys[i].GetComponent<BoxCollider2D>();
        }
        
        playerMoveController = player.GetComponent<CharacterMovement>();

        doors = GameObject.FindGameObjectsWithTag("LockableDoor");
        doorCheck = new ControlSimpleDoor[doors.Length];
        doorRenderer = new SpriteRenderer[doors.Length];
        isBarricaded = new bool[doors.Length];

        for (int i = 0; i < doors.Length; ++i)
        {
            doorCheck[i] = doors[i].GetComponent<ControlSimpleDoor>();
            doorRenderer[i] = doors[i].GetComponent<SpriteRenderer>();
            isBarricaded[i] = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        int[] itemsNearby = NearbyDoor();

        if (itemsNearby[0] != -1 && doorCheck[itemsNearby[0]].IsDoorLocked() && !isBarricaded[itemsNearby[0]])
        {
            doorRenderer[itemsNearby[0]].sprite = barricadedDoor; 
            isBarricaded[itemsNearby[0]] = true;
          //  doorCheck[itemsNearby[0]].SetDoorInactive(); //this sets the door to be inactive - once barricaded the door 
            //cannot be unbarricaded or interacted with 
            StartCoroutine("PauseWhileBarricadingDoor");
            gurneyRenderer[itemsNearby[1]].enabled = false;
            gurneyCollider[itemsNearby[1]].enabled = false; 
        }
    }

    IEnumerator PauseWhileBarricadingDoor()
    {
        playerMoveController.MovementForbidden = true;
        sliderText.text = "Barricading Door";
        float increaseSlider = 0;
        pauseCanvas.enabled = true;

        doorBarricadeSound.Play();

        if (SubtitleState.subtitlesOn)
        {
            subtitles.SetSubtitleText("[hammering sounds]");
        }

        while (increaseSlider <= 1)
        {
            barricadePauseSlider.value = increaseSlider;
            increaseSlider += 0.07f;
            yield return new WaitForSeconds(0.2f);
        }

        //yield return new WaitForSeconds(2.5f);
        pauseCanvas.enabled = false;
        playerMoveController.MovementForbidden = false;

        StopCoroutine("PauseWhileBarricadingDoor");

    }

    //determines if a gurney is nearby a door
    private int[] NearbyDoor()
    {
        int[] doorAndGurneyNearby = new int[2];
        for (int i = 0; i < doors.Length; ++i)
        {
            for (int j = 0; j < gurneys.Length; ++j)
            {
                //if the player is nearby the gurney
                if ((Mathf.Abs(doors[i].transform.position.x - gurneys[j].transform.position.x) < 1) && (Mathf.Abs(doors[i].transform.position.y - gurneys[j].transform.position.y) < 1))
                {
                    //door first, then gurney
                    doorAndGurneyNearby[0] = i;
                    doorAndGurneyNearby[1] = j;
                    return doorAndGurneyNearby;
                }
            }
            
        }

        doorAndGurneyNearby[0] = -1;
        doorAndGurneyNearby[1] = -1;
      
        return doorAndGurneyNearby;

    }
}
