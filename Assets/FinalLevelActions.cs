using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class FinalLevelActions : MonoBehaviour
{
    public MoveToCutscene checkIfMoved;

    //speech
    public GameObject speechBox;
    public GameObject playerAvatar;
    public TextMeshProUGUI tTextbox; //title
    public TextMeshProUGUI textBox;

    public Canvas blackScreen;
    public Image blackImage;

    //prebrief display
    public GameObject endBrief;

    private bool atEndLocation = false;
    private bool spoken = false; 
    private float checkOnce = 0;
    private float timer = 20; //45 seconds 


    private void Update()
    {
        atEndLocation = checkIfMoved.moved;

        if (atEndLocation && checkOnce == 0)
        {
            checkOnce = 1;
            PlayerTalk();
        }

        if (spoken)
        {
            if (timer > 0 && spoken)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    StartCoroutine(checkIfMoved.FadeToBlack());
                    StartCoroutine("Pause");
                } 
            }
        }
    }

    public void OnContinueClicked()
    {
        SceneManager.LoadScene("Menu");
    }

    private void PlayerTalk()
    {
        speechBox.SetActive(true);
        playerAvatar.SetActive(true);
        tTextbox.text = "Player";
        textBox.text = "What's going on? There's so much happening at once.";
        spoken = true; 
    }
    
    private IEnumerator Pause()
    {
        yield return new WaitForSecondsRealtime(3f);
     
            

        endBrief.SetActive(true);
    }

    

}
