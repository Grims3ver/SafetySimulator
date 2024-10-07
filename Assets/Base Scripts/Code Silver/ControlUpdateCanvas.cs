using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

///<summary> A canvas intended to be briefly shown to indicate status changes </summary>
public class ControlUpdateCanvas : MonoBehaviour
{
    private Image[] icon;
    private TextMeshProUGUI textBox; 
   
    public Canvas updateCanvas;

    //sprites
    public Sprite codexSymbol;
    public Sprite emergencyPhone;
    public Sprite window;
    public Sprite door;
    public Sprite radio;
    public Sprite TV; 

    //bool flag
    [HideInInspector]
    public bool emergCoroutineRunning;
    [HideInInspector]
    public bool windowCoroutineRunning;
    [HideInInspector]
    public bool doorCoroutineRunning;
    [HideInInspector]
    public bool barricadedDoorCoroutineRunning;
    [HideInInspector]
    public bool radioCoroutineRunning;
    [HideInInspector]
    public bool tvCoroutineRunning;

    //set's the text for doors
    private string specificDoorTip; //give a slightly different tip depending if the door is locked or unlocked
    private string specificRadioTip; //different tip depending on radio state
    private string specificWindowTip; //covered vs uncovered

    //sets the text for barricaded doors 
    private string specificBarricadeTip; //gives different tip depending on settings
    void Start()
    {
        updateCanvas.enabled = false;
        icon = GetComponentsInChildren<Image>();
        textBox = GetComponentInChildren<TextMeshProUGUI>();
        specificDoorTip = "";
        specificBarricadeTip = "";
        specificRadioTip = "";
        specificWindowTip = "";
    }

    //control the codex update version of this canvas
    public IEnumerator ShowCodexUpdate()
    {
        icon[1].sprite = codexSymbol;
        textBox.text = "Codex updated.";
        updateCanvas.enabled = true; 
        yield return new WaitForSecondsRealtime(3);
        updateCanvas.enabled = false;
        StopCoroutine("ShowCodexUpdate");
    }

    public IEnumerator ShowEmergencyPhoneTip()
    {
        emergCoroutineRunning = true; 
        icon[1].sprite = emergencyPhone;
        textBox.text = "Press 'F' to use the emergency phone.";
        updateCanvas.enabled = true;
        yield return new WaitForSecondsRealtime(4);
        updateCanvas.enabled = false;
        emergCoroutineRunning = false; 
        StopCoroutine("ShowEmergencyPhoneTip");
    }

    public IEnumerator ShowWindowTip()
    {
        windowCoroutineRunning = true;
        icon[1].sprite = window;
        textBox.text = specificWindowTip;
        updateCanvas.enabled = true;
        yield return new WaitForSecondsRealtime(4);
        updateCanvas.enabled = false;
        windowCoroutineRunning = false;
        StopCoroutine("ShowWindowTip");
    }

    public IEnumerator ShowDoorTip()
    {
        doorCoroutineRunning = true;
        icon[1].sprite = door;
        //the tip changes slightly depending on if the door is locked/unlocked
        textBox.text = specificDoorTip;
        updateCanvas.enabled = true;
        yield return new WaitForSecondsRealtime(4);
        updateCanvas.enabled = false;
        doorCoroutineRunning = false;
        StopCoroutine("ShowDoorTip");
    }

    public IEnumerator ShowBarricadedDoorTip()
    {
        barricadedDoorCoroutineRunning = true;
        icon[1].sprite = door;
        textBox.text = specificBarricadeTip;
        updateCanvas.enabled = true;
        yield return new WaitForSecondsRealtime(4);
        updateCanvas.enabled = false;
        barricadedDoorCoroutineRunning = false;
        StopCoroutine("ShowBarricadedDoorTip");
    }
    public IEnumerator ShowRadioTip()
    {
        radioCoroutineRunning = true;
        icon[1].sprite = radio;
        textBox.text = specificRadioTip;
        updateCanvas.enabled = true;
        yield return new WaitForSecondsRealtime(4);
        updateCanvas.enabled = false;
        radioCoroutineRunning = false;
        StopCoroutine("ShowRadioTip");
    }

    public IEnumerator ShowTVTip()
    {
        tvCoroutineRunning = true;
        icon[1].sprite = TV;
        textBox.text = "Press 'F' to turn off the TV.";
        updateCanvas.enabled = true;
        yield return new WaitForSecondsRealtime(4);
        updateCanvas.enabled = false;
        tvCoroutineRunning = false;
        StopCoroutine("ShowTVTip");
    }

    public void SetDoorTip(string doorStateMessage)
    {
        specificDoorTip = doorStateMessage; 
    }

    public void SetRadioTip(string radioStateMessage)
    {
        specificRadioTip = radioStateMessage;
    }

    public void SetWindowTip(string windowStateMessage)
    {
        specificWindowTip = windowStateMessage;
    }

    public void SetBarricadeTip(string barricadeStateMessage)
    {
        specificBarricadeTip = barricadeStateMessage;
    }
}
