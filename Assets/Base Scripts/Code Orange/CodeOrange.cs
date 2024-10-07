using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MoreMountains.TopDownEngine;
using UnityEngine.UI;

public class CodeOrange : MonoBehaviour
{
    [HideInInspector]
    public bool codeOrangeAnnounced = false;

    public ControlEmergencyPhoneScene emergPhone;
    private bool isCodeSilverActive;

    public AudioSource alarmSound;

    public CharacterMovement player;

    public Canvas codeOrangeWarning;
    public TextMeshProUGUI codeOrangeTitle;
    public Image codeOrangeBackground;

    public TextMeshProUGUI warningText;
    public TextMeshProUGUI warningTitle;


    public GameObject HUD;
    public GameObject warning;


    private void Update()
    {
        isCodeSilverActive = emergPhone.isCodeSilverOver;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && this.gameObject.name == "CodeOrangeTrigger" && !codeOrangeAnnounced && isCodeSilverActive)
        {
            StartCoroutine("AnnounceCodeOrange");
        }
    }

    private IEnumerator AnnounceCodeOrange()
    {
        warning.SetActive(true);
        
        warningTitle.text = "ANNOUNCER";
        warningText.text = "ALERT: Code Orange initiated due to multiple patient injuries.";

        HUD.SetActive(false);

        codeOrangeBackground.color = new Color32(255, 161, 0, 255);

        codeOrangeTitle.text = "Code Orange";
        player.MovementForbidden = true;

        alarmSound.Play();
        codeOrangeWarning.enabled = true;

        yield return new WaitForSecondsRealtime(4f);

        warningText.text = "Security, initiate emergency department lockdown procedures for crowd management. ";

        yield return new WaitForSecondsRealtime(4f);

        warningText.text = "Emergency department, decanting procedures are now initiated.";

        yield return new WaitForSecondsRealtime(4f);

        warningText.text = "Operating Room’s proceed to standby. All staff prepare for influx of patients.";

        yield return new WaitForSecondsRealtime(3f);

        HUD.SetActive(true);
        player.MovementForbidden = false;
        warning.SetActive(false);
        codeOrangeWarning.enabled = false;
        codeOrangeAnnounced = true; 
        StopCoroutine("AnnounceCodeOrange");
    }
}
