using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HighlightLockedDoors : MonoBehaviour
{
    //false by default, doesn't need to be editable in inspector
    [HideInInspector]
    private bool highlightLockedDoors = false;

    //default text that indicates when the default setting is selected (off)
    public TextMeshProUGUI defaultText;


    //controls appearance of image to user
    public Sprite highlightedDoor;
    public Sprite noHighlightDoor;

    //image for the door
    public Image displayCorrectDoor;


    private void Start()
    {
        displayCorrectDoor.sprite = noHighlightDoor;
        //toggle begins at false
        defaultText.enabled = true; 
    }

    public void ToggleLockedDoors()
    {
        highlightLockedDoors = !highlightLockedDoors; //toggle functionality
       
        //by default doors are NOT highlighted
        if (highlightLockedDoors)
        {
            defaultText.enabled = false; 
        } else
        {
            defaultText.enabled = true; 
        }

        //setting the correct image 
        if (highlightLockedDoors)
        {
            displayCorrectDoor.sprite = highlightedDoor;
        }
        else if (!highlightLockedDoors)
        {
            displayCorrectDoor.sprite = noHighlightDoor;
        }
    }

    public bool GetHighlightDoorStatus()
    {
        return highlightLockedDoors; 
    }

}
