using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;


public class ControlCodes : MonoBehaviour
{
    public Canvas codeCanvas;
    private AudioSource alarmSound;
    public CharacterMovement characterController;

    //flag 
    private bool isCoroutineRunning = false; 
    void Start()
    {
        codeCanvas.enabled = false;
        alarmSound = codeCanvas.GetComponent<AudioSource>();
    }

    public void EnabledCodeCanvas()
    {
        StartCoroutine("WaitForCanvas");
        isCoroutineRunning = true; 
        codeCanvas.enabled = true;
        alarmSound.Play();
        characterController.MovementForbidden = true; 

    }

    IEnumerator WaitForCanvas()
    {
        yield return new WaitForSecondsRealtime(3f);
        codeCanvas.enabled = false;
        characterController.MovementForbidden = false;
        isCoroutineRunning = false;
        StopCoroutine("WaitForCanvas");
    }

    public bool CodeCanvasRunning() { return isCoroutineRunning; }
}
