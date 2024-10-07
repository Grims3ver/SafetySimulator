using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;

public class ContinueButton_CodeS : MonoBehaviour
{
    //slight variation of script for different level
    //control which canvases are enabled,
    public Canvas settingsScreen;
    public ControlPrebrief_NoTimer prebriefCheck;
    public CharacterMovement character;
    //enable user's HUD
    public GameObject HUD; //part of the engine system, disabled during menus in a seperate script



    public void OnContinueClick()
    {
        settingsScreen.enabled = false;
        prebriefCheck.prebriefRead = true;
        character.MovementForbidden = false;
        HUD.SetActive(true);
    }
}
