using System.Collections;
using System.Collections.Generic;
using MoreMountains.TopDownEngine;
using UnityEngine;
using TMPro;

public class EndLevelAction : MonoBehaviour
{
    //relevant gameobjects
    public GameObject player;
    //controlling player movement
    private CharacterMovement playerMovement;

    public ControlEmergencyPhoneScene phoneCutscene; // has the player warned the switch board?

    //dialog
    public GameObject dialogPanel;
    public TextMeshProUGUI dialogTitle;
    public TextMeshProUGUI dialog;

    //controlling NPC movement
    private CharacterMovement npcMovement;

    //controlling NPC visibility
    private SpriteRenderer npcSprite;
    private BoxCollider2D npcCollider;
    
    
    private bool nearbyPlayer;
    private bool dialogSaid = false;
    private bool waiting = false; 


    private void Start()
    {
       
        playerMovement = player.GetComponent<CharacterMovement>();
        npcMovement = this.GetComponent<CharacterMovement>();
        //NPC movement set to false initially
        npcMovement.MovementForbidden = true; //NPC should be stationary 
        npcSprite = this.GetComponentInChildren<SpriteRenderer>();
        npcCollider = this.GetComponent<BoxCollider2D>();
        waiting = false; 
    }

    // Update is called once per frame
    void Update()
    {
        nearbyPlayer = CheckIfPlayerNearby();

        if (nearbyPlayer && !dialogSaid && phoneCutscene.isCodeSilverOver && !waiting)
        {
            StartCoroutine("LeadPlayer");
        } else if (nearbyPlayer && !dialogSaid && !phoneCutscene.isCodeSilverOver && !waiting)
        {
            StartCoroutine("WarnPlayer");
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Doors"))
        {
            npcSprite.enabled = false; //invisble NPC
            npcCollider.enabled = false; //including collider
        }
    }


    private bool CheckIfPlayerNearby()
    {

        if ((Mathf.Abs(player.transform.position.x - this.transform.position.x) < 2) && (Mathf.Abs(player.transform.position.y - this.transform.position.y) < 2))
        {
            return true; 
        }

        return false; 
    }

    private IEnumerator WarnPlayer()
    {
        playerMovement.MovementForbidden = true; //player can't move
        dialogTitle.text = "Nurse";
        dialog.fontSize = 60; 
        dialog.text = "You have to warn everyone about the Code Silver before we can escape!";
        dialogPanel.SetActive(true);
        yield return new WaitForSeconds(4f);
        playerMovement.MovementForbidden = false;
        dialogPanel.SetActive(false);
        StartCoroutine("WaitForPlayer");
        StopCoroutine("WarnPlayer");
    }

    private IEnumerator WaitForPlayer()
    {
        waiting = true;
        yield return new WaitForSecondsRealtime(5f);
        waiting = false;
        StopCoroutine("WaitForPlayer");

    }
    private IEnumerator LeadPlayer()
    {
        playerMovement.MovementForbidden = true; //player can't move
        dialogTitle.text = "Nurse";
        dialog.text = "Oh great, you made it! They need our help, we don't know who is still alive, and we need to regroup. Let's get out of here. ";
        dialogPanel.SetActive(true);
        yield return new WaitForSeconds(4f);
        playerMovement.MovementForbidden = false;
        npcMovement.MovementForbidden = false; 
        dialogPanel.SetActive(false);
        dialogSaid = true; 
        StopCoroutine("LeadPlayer");
       

    }
}
