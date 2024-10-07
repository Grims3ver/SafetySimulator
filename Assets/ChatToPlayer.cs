using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MoreMountains.TopDownEngine;

public class ChatToPlayer : MonoBehaviour
{
    public GameObject speechBox;
    public TextMeshProUGUI tTextbox; //title
    public TextMeshProUGUI textBox;

    public CodeOrange codeOrange; 

    public GameObject player;
    private CharacterMovement pMove;

    private CharacterMovement npcMove;
    private string[] chatSayings;

   
    //ensure coroutine is only started if off 
    private static bool talkingAlready = false; 

    void Start()
    {
        pMove = player.GetComponent<CharacterMovement>();
        chatSayings = new string[11];

        chatSayings[0] = "I'm glad we found this safe spot to regroup.";
        chatSayings[1] = "This is awful, we need the police to arrive soon!";
        chatSayings[2] = "Is it over? Do you know where the doctors are? Where is the attending?";
        chatSayings[3] = "You made it! We need to regroup.";
        chatSayings[4] = "There are two of them!";
        chatSayings[5] = "Was that a gunshot? Do you know where we need to go?";
        chatSayings[6] = "Run!";
        chatSayings[7] = "Ahh! Oh, it's just you. You scared me.";
        chatSayings[8] = "What is going on?!";
        chatSayings[9] = "I wish I had a weapon! I guess I should go hide until this is over.";
        chatSayings[10] = "Head for the West hallway, quickly!";

    }

    // Update is called once per frame
    void Update()
    {
        bool nearby = CheckIfPlayerNearby();

        if (nearby && !talkingAlready)
        {
            StartCoroutine("Talk");
        }
    }

    private bool CheckIfPlayerNearby()
    {

        if ((Mathf.Abs(player.transform.position.x - this.transform.position.x) < 2) && (Mathf.Abs(player.transform.position.y - this.transform.position.y) < 2))
        {
            npcMove = this.GetComponent<CharacterMovement>();
            return true;
        }

        return false;
    }


    private IEnumerator Talk()
    {
        talkingAlready = true; 
        speechBox.SetActive(true);
        pMove.MovementForbidden = true;
        npcMove.MovementForbidden = true;
        int randDialog = 2;

        //min inclusive max exclusive
        if (codeOrange.codeOrangeAnnounced)
        {
            randDialog = Random.Range(0, 4);
        } else
        {
            randDialog = Random.Range(4, 11);
        }

        tTextbox.text = "Staff Member";

        textBox.text = chatSayings[randDialog];

        yield return new WaitForSecondsRealtime(2.5f);

        pMove.MovementForbidden = false;
        speechBox.SetActive(false);

        //pause before resuming
        yield return new WaitForSecondsRealtime(3f);

        npcMove.MovementForbidden = false;
       
        talkingAlready = false; 
        StopCoroutine("Talk");
    }
}
