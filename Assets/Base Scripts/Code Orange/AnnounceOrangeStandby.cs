using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MoreMountains.TopDownEngine;
using UnityEngine.UI;

public class AnnounceOrangeStandby : MonoBehaviour
{

    public ControlEmergencyPhoneScene emergPhone;
    private bool isCodeSilverActive;

    private bool codeOrangeAnnounced = false;

    public AudioSource alarmSound; 

    public CharacterMovement player; 

    public Canvas codeOrangeWarning;
    public TextMeshProUGUI codeOrangeTitle;
    public Image codeOrangeBackground;

    public GameObject HUD; 
    public GameObject warning;


    private void Update()
    {
        isCodeSilverActive = emergPhone.isCodeSilverOver;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && this.gameObject.name == "CodeOrangeStandByTrigger" && !codeOrangeAnnounced && isCodeSilverActive)
        {
            StartCoroutine("AnnounceCodeOrangeStandby");
        } 
    }

    private IEnumerator AnnounceCodeOrangeStandby()
    {
        warning.SetActive(true);

        HUD.SetActive(false);

        codeOrangeBackground.color = new Color32(255, 161, 0, 255);

        codeOrangeTitle.text = "Code Orange Standby";
        player.MovementForbidden = true;
      
        alarmSound.Play(); 
        codeOrangeWarning.enabled = true;

        yield return new WaitForSecondsRealtime(4.5f);

        HUD.SetActive(true);
        player.MovementForbidden = false;
        warning.SetActive(false);
        codeOrangeWarning.enabled = false;
        codeOrangeAnnounced = true; 
        StopCoroutine("AnnounceCodeOrangeStandby");
    }
}
