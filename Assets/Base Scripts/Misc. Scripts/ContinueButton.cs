using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;

public class ContinueButton : MonoBehaviour
{
    //control which canvases are enabled,
    public Canvas settingsScreen;
    public ControlPrebrief prebriefCheck;
    public CharacterMovement character;
    //enable user's HUD
    public GameObject HUD; //part of the engine system, disabled during menus in a seperate script
    public Canvas timerScreen;



    public void OnContinueClick()
    {
        settingsScreen.enabled = false;
        prebriefCheck.prebriefRead = true;
        character.MovementForbidden = false;
        HUD.SetActive(true);
        timerScreen.enabled = true; 
    }
}
