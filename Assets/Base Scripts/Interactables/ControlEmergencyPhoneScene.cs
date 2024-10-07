using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlEmergencyPhoneScene : MonoBehaviour
{
    public TMP_Text bodyText;
    public TMP_Text titleText;

    //activate during
    public Canvas codeCanvas;
    public AudioSource alarmSound;

    public ControlUpdateCanvas displayTooltip;
    public GameObject dialogTextField;
    public GameObject emergencyPhone;

    [HideInInspector]
    public bool isCodeSilverOver = false;

    public GameObject HUD; //to be disabled and renenabled 
    public GameObject playerSpeaking; //displays player icon when player is speaking 

    private GameObject player;

    private bool isPlayerNearby;
    private string[] titles;
    private string[] bodies;

    private string currentlyDisplayedText = "";

    int clickNumber = 0;
    private bool titleSwap = false;

    private bool isTitleCompleted;
    private bool isBodyCompleted;

    void Start()
    {
        player = GameObject.Find("Player");
        dialogTextField.SetActive(false);
        playerSpeaking.SetActive(false);

        titles = new string[2];
        bodies = new string[5];

        titles[0] = "Switchboard";
        titles[1] = "Player";

        bodies[0] = "... You have reached the Switchboard, how can I help?";
        bodies[1] = "Quickly, there is an armed assailant on this floor! There may be more than one individual with a gun. We need help, please!";
        bodies[2] = "Understood. I am contacting the authorities now. Can you give a description of the assailant?  Where are you located? Can you give further details on the situation?";
        bodies[3] = "We are located in the ICU. The suspect is armed with a gun and very dangerous. They are wearing pink scrubs, and appear to be female. We need police as soon as possible!";
        bodies[4] = "Information recorded; announcing Code Silver now. Police have been notified.  Please hang up and evacuate the building immediately.";

        isTitleCompleted = false;
        isBodyCompleted = false;

    }

    void Update()
    {
        isPlayerNearby = CheckPlayerProximity();
       

        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            dialogTextField.SetActive(true);
            SetFirstTitle();
            HUD.SetActive(false);
        }
    }

    private IEnumerator PlayCodeSilverAlarm()
    {
        codeCanvas.enabled = true;
        alarmSound.Play();
        yield return new WaitForSeconds(4f);
        HUD.SetActive(true);
        codeCanvas.enabled = false;
        isCodeSilverOver = true;
        StopAllCoroutines();

    }

    public IEnumerator RunOverTitle()
    {
        titleText.text = "";
        bodyText.text = "";
        isTitleCompleted = false;

        foreach (char c in currentlyDisplayedText.ToCharArray())
        {
            titleText.text += c;
            yield return new WaitForSecondsRealtime(0.1f);
        }

        isTitleCompleted = true;

        if (isTitleCompleted)
        {
            currentlyDisplayedText = bodies[clickNumber];
            StartCoroutine("RunOverBody");

        }

        yield return null;
        StopCoroutine("RunOverTitle");

    }

    public IEnumerator RunOverBody()
    {
        bodyText.text = "";
        isBodyCompleted = false;

        foreach (char c in currentlyDisplayedText.ToCharArray())
        {
            bodyText.text += c;
            yield return new WaitForSecondsRealtime(0.1f);
        }

        isBodyCompleted = true;
        yield return null;
    }

    private void SetFirstTitle()
    {
        currentlyDisplayedText = titles[0];

        StartCoroutine("RunOverTitle");


        if (isBodyCompleted)
        {
            StopCoroutine("RunOverBody");
        }


    }

    public void OnContinueButtonClicked()
    {

        if (!isTitleCompleted || !isBodyCompleted)
        {
            if (!isTitleCompleted)
            {
                FinishTitleDisplay();
                isTitleCompleted = true; 
                StopCoroutine("RunOverTitle");
            }

            if (!isBodyCompleted)
            {
                FinishBodyDisplay();
                isBodyCompleted = true;
                StopCoroutine("RunOverBody");
            }
        }
        else
        {

            if (clickNumber < 4)
            {

                titleSwap = !titleSwap; //swap 

                if (titleSwap)
                {
                    currentlyDisplayedText = titles[1];
                    playerSpeaking.SetActive(true);
                }
                else if (!titleSwap)
                {
                    currentlyDisplayedText = titles[0];
                    playerSpeaking.SetActive(false);
                }

                StartCoroutine("RunOverTitle");

                if (isTitleCompleted)
                {
                    StopCoroutine("RunOverTitle");
                    currentlyDisplayedText = bodies[clickNumber];
                    StartCoroutine("RunOverBody");
                }


                if (isBodyCompleted)
                {
                    StopCoroutine("RunOverBody");
                }

                ++clickNumber;

            }
            else if (clickNumber >= 4)
            {
                dialogTextField.SetActive(false);
                playerSpeaking.SetActive(false);
              
                if (!isCodeSilverOver)
                {
                    StartCoroutine("PlayCodeSilverAlarm");
                } 
            }

           

        }


    }


    private void FinishTitleDisplay()
    {
        titleText.text = currentlyDisplayedText;
    }

    private void FinishBodyDisplay()
    {
        bodyText.text = currentlyDisplayedText;

    }


    private bool CheckPlayerProximity()
    {
        //if player distance from emergency phone is less than = one
        //transform.position = position of the phone 
        if (Vector3.Distance(emergencyPhone.transform.position, player.transform.position) <= 2)
        {
            if (!displayTooltip.emergCoroutineRunning)
            {
                displayTooltip.StartCoroutine("ShowEmergencyPhoneTip");
            }
            return true;
        }

        return false;
    }
}
