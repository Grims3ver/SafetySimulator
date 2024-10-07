using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlTutorial : MonoBehaviour
{
    //text
    private TextMeshProUGUI[] textBoxes;
    private int tutorialCounter; //keeps track of what text is needed

    //audio
    public AudioSource popSound;

    //create tutorial germ 
    private SpriteRenderer spriteRenderer;
    private Sprite germSprite;
    public Texture2D texture; //set in Unity to germ sprite

    //controllers
    CheckIfDestroyed cleanerDestroyed;
    CheckIfDestroyed healthDestroyed;
    CheckProximity germProximity;
    ControlTasks tasker;
    HideInCloset closetHider;
    ControlTutorialDoor doorController;
    ControlRadioAnimation radio;
    CoverWindow window;
    SpreadGerms germSpreader;
    ControlHealth healthController;
    ControlNotebook notebookController;

    //relevant gameobjects
    GameObject tutorialCleaner;
    GameObject tutorialCloset;
    GameObject tutorialSink;
    GameObject tutorialHealthKit;
    GameObject endLevelDoor;
    public GameObject gurney; 

    //ensure one call 
    int playSoundOnce = 0;
    //flags
    private bool move = false;
    private bool callOnce = false;
    private bool notebookEnabled = false;
    private bool cellEnabled = false;
    private bool oneShot = false;

    //other
    public Canvas cellPhoneInterface;
    public Canvas inventoryInterface;
    public Canvas notebookInterface;
    public Canvas tutorialInterface;
    public Canvas codexInterface;

    void Start()
    {
        tutorialCounter = 0;
        //textboxes[0] corresponds to panel title, textboxes[1] corresponds to tutorial text
        textBoxes = GetComponentsInChildren<TextMeshProUGUI>();

        germProximity = GameObject.FindGameObjectWithTag("Player").GetComponent<CheckProximity>();
       
        notebookController = GameObject.Find("NoteBook Interface").GetComponent<ControlNotebook>();
        notebookController.DisableNotebookInterface();

        tutorialHealthKit = GameObject.Find("Tutorial_Healthkit");
        tutorialHealthKit.SetActive(false);

        tutorialCleaner = GameObject.Find("Tutorial_Cleaner");
        tutorialCleaner.SetActive(false); //inactive until needed
        healthDestroyed = tutorialHealthKit.GetComponent<CheckIfDestroyed>();

        tutorialCloset = GameObject.Find("Tutorial_Closet");
        closetHider = tutorialCloset.GetComponent<HideInCloset>();
        tutorialCloset.SetActive(false);

        doorController = GameObject.Find("Tutorial_Door").GetComponent<ControlTutorialDoor>();

        tasker = notebookInterface.GetComponentInChildren<ControlTasks>();

        radio = GameObject.Find("Tutorial_Radio").GetComponent<ControlRadioAnimation>();
        window = GameObject.Find("Window").GetComponent<CoverWindow>();
        germProximity = GameObject.FindGameObjectWithTag("Player").GetComponent<CheckProximity>();
        germSpreader = GameObject.FindGameObjectWithTag("Player").GetComponent<SpreadGerms>();

        tutorialSink = GameObject.Find("Tutorial_Sink");
        tutorialSink.SetActive(false);

        healthController = GameObject.FindGameObjectWithTag("Player").GetComponent<ControlHealth>();

        endLevelDoor = GameObject.Find("End_Level_Door");
        endLevelDoor.SetActive(false);

        cellPhoneInterface.enabled = false;
        notebookInterface.enabled = false;
        codexInterface.enabled = false;

        cleanerDestroyed = tutorialCleaner.GetComponent<CheckIfDestroyed>();

        gurney.SetActive(false);
        

        StartCoroutine("ControlTutorialFunctions");
    }


    private void Update()
    {
       

        //certain checks/functionalities need to be in Update
        //most Inventory steps need to be here, because opening the inventory
        //automatically pauses all coroutines bc that's the way the engine works


        if (((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            move = true;
        }

        if ((Input.GetKeyDown(KeyCode.F) && germProximity.CheckProximityOfGerms() && tutorialCounter == 1) || (Input.GetKeyDown(KeyCode.F) && closetHider.GetNearbyStatus() && tutorialCounter == 9))
        {
            ++tutorialCounter;
           // print("triggered");
        }

        if (tutorialCounter == 1 && !callOnce)
        {
            CreateTutorialGerm();
            callOnce = true;
        }

        if (tutorialCounter == 3 && cellEnabled)
        {
            ++tutorialCounter;
        }

        if(tutorialCounter == 4 && playSoundOnce == 5)
        {
            if (playSoundOnce == 5)
            {
                popSound.Play();
                ++playSoundOnce;
                textBoxes[1].text = "This is your cellphone. You will need it in case of emergencies. Feel free to push some buttons and get familiar with the interface." +
                " Click the cell phone icon again to close your cellphone when you are ready.";
            }
        }

        if (cellPhoneInterface.enabled)
        {
            cellEnabled = true;

        }
        else if (!cellPhoneInterface.enabled)
        {
            cellEnabled = false;

            if (playSoundOnce == 6)
            {
                popSound.Play();
                ++playSoundOnce;
                textBoxes[1].text = "Click the book icon to open your Codex.";
            } 
        }

        if (playSoundOnce == 7 && codexInterface.enabled)
        {
            popSound.Play();
            ++playSoundOnce;
            textBoxes[1].text = "Here, you can find useful background information. Remember to check this periodically as you play. Click the icon again to close your Codex. Close your Inventory when you are ready by" +
                " pressing the 'I' key again.";
        }

        if (playSoundOnce == 8 && !codexInterface.enabled)
        {
            tutorialCounter = 7;
            ++playSoundOnce;
        }


        if (notebookInterface.enabled)
        {
            notebookEnabled = true;

        }
        else if (!notebookInterface.enabled)
        {
            notebookEnabled = false;
        }

        if (tutorialCounter == 12 && radio.radioOff)
        {
                ++tutorialCounter;
        }
    }


    IEnumerator ControlTutorialFunctions()
    {


        while (tutorialCounter <= 20)
        {
           

            if (tutorialCounter == 0)
            {
                if (move)
                {
                    yield return new WaitForSeconds(5f);

                    if (playSoundOnce == 0)
                    {
                        popSound.Play();
                        ++playSoundOnce;
                    }

                    ++tutorialCounter;
                }

            }
            else if (tutorialCounter == 1)
            {

                textBoxes[0].text = "Tutorial 2/12";
                textBoxes[1].text = "Good job! Looks like some germs appeared on the floor. Why not check them out?";


                if (germProximity.CheckProximityOfGerms())
                {
                    if (playSoundOnce == 1)
                    {
                        popSound.Play();
                        ++playSoundOnce;
                    }
                    textBoxes[1].text = "When nearby germs, press 'F' to clean them up.";

                }
            }
            else if (tutorialCounter == 2)
            {
                if (tutorialCleaner != null)
                {
                    tutorialCleaner.SetActive(true);
                }
                textBoxes[0].text = "Tutorial 3/12";
                textBoxes[1].text = "Good job! Looks like this place needs cleaning. That bottle of cleaning solution could be useful. To use items, simply run over them. Cleaner resets your germ-spreading state.";
                if (playSoundOnce == 2)
                {
                    popSound.Play();
                    ++playSoundOnce;
                }

                if (cleanerDestroyed.IsDestroyed())
                {
                    if (playSoundOnce == 3)
                    {
                        ++tutorialCounter;
                        ++playSoundOnce;
                    }
                }
            }
            else if (tutorialCounter == 3)
            {
                textBoxes[0].text = "Tutorial 4/12";
                textBoxes[1].text = "Great. Now, let's check out your inventory. Press I, then click the phone icon to check out your cellphone. Click the cellphone icon to open your phone now.";

                if (playSoundOnce == 4)
                {
                    popSound.Play();
                    ++playSoundOnce;
                }


            }
            else if (tutorialCounter == 5)
            {
                if (playSoundOnce == 6)
                {
                    popSound.Play();
                    ++playSoundOnce;
                    textBoxes[1].text = "Click the book icon to open your Codex.";
                }
                if (codexInterface.enabled)
                {
                    ++tutorialCounter;
                }
            }
            else if (tutorialCounter == 6)
            {
                if (playSoundOnce == 7)
                {
                    popSound.Play();
                    ++playSoundOnce;
                    textBoxes[1].text = "Here, you can find useful background information. Remember to check this periodically as you play. Click the icon again to close your Codex.";
                }

            }
            else if (tutorialCounter == 7)
            {
                bool doFirst = false;
                //both cellphone and inventory screen closed
                notebookController.EnableNotebookInterface();
                if (playSoundOnce == 9)
                {
                    popSound.Play();
                    ++playSoundOnce;
                }
                textBoxes[0].text = "Tutorial 5/12";
                textBoxes[1].text = "Great! The last interface you need to get familiar with is the Notebook. Press 'N' to open your Notebook.";

                if (notebookEnabled && !doFirst)
                {
                    if (playSoundOnce == 10)
                    {
                        popSound.Play();
                        ++playSoundOnce;
                    }
                    textBoxes[1].text = "Great job! Here, you will find all the tasks that you need to complete to finish missions. Don't forget to check here if you get stuck! As you complete tasks, they will be marked off in the Notebook."
                        + " Press 'N' to close your Notebook.";
                    yield return new WaitForSeconds(3f);
                    tasker.SetTaskCompletion(0); //strikeout task
                    doFirst = true;

                }

                if (!notebookEnabled && doFirst)
                {
                    ++tutorialCounter;
                }


            }
            else if (tutorialCounter == 8)
            {
                if (playSoundOnce == 11)
                {
                    popSound.Play();
                    ++playSoundOnce;
                }

                textBoxes[0].text = "Tutorial 6/12";
                textBoxes[1].text = "Now, we will learn about some of the skills that you will need to complete your missions.";
                yield return new WaitForSeconds(5f);

                if (playSoundOnce == 12)
                {
                    
                    popSound.Play();
                    ++playSoundOnce;

                    if (tutorialCloset != null)
                    {
                        tutorialCloset.SetActive(true);
                        ++tutorialCounter;
                    }
                }

                //check is in update

            }
            else if (tutorialCounter == 9)
            {
                textBoxes[1].text = "The first thing we learn about is hiding places. They are marked by their orange colouring. Remember to look out for them! Approach the orange closet above, and press 'F' to hide in it.";

            }
            else if (tutorialCounter == 10)
            {
                if (playSoundOnce == 13)
                {
                    popSound.Play();
                    ++playSoundOnce;
                }
                textBoxes[1].text = "Great! The blue ring around the closet indicates you are hidden. When you are ready, simple exit the closet by moving away from it.";

                if (!closetHider.GetNearbyStatus())
                {
                    ++tutorialCounter;
                }

            }
            else if (tutorialCounter == 11)
            {
                if (playSoundOnce == 14)
                {
                    popSound.Play();
                    ++playSoundOnce;
                }

                textBoxes[0].text = "Tutorial 7/12";
                textBoxes[1].text = "You can also interact with doors in a similar way. Follow the rocky floor to the room above, and lock the door behind you. When nearby the door, press 'F' to lock (or unlock) it.";

                //if door is locked
                if (doorController.IsDoorLocked())
                {
                    if (playSoundOnce == 15)
                    {
                        popSound.Play();
                        ++playSoundOnce;
                    }
                    textBoxes[0].text = "Tutorial 8/12";
                    textBoxes[1].text = "Excellent. This will help protect you if you need to hide. \n Darn! Looks like someone left the radio on. You should be on the look out for NOISY objects - they produce sound, and must be dealt with in some emergency cases!";
                    yield return new WaitForSeconds(6f);
                    ++tutorialCounter;
                }
            }
            else if (tutorialCounter == 12)
            {
                if (playSoundOnce == 16)
                {
                    popSound.Play();
                    ++playSoundOnce;
                }
                textBoxes[1].text = "Approach the radio and press 'F' to turn it off.";
                //check in update  
            }
            else if (tutorialCounter == 13)
            {
                if (playSoundOnce == 17)
                {
                    popSound.Play();
                    ++playSoundOnce;
                }

                textBoxes[0].text = "Tutorial 9/12";
                textBoxes[1].text = "Looks like someone forgot to cover the windows too... In certain survival situations, your best defense will be to remain hidden. Covering windows can be helpful for blocking intruder vision.";
                yield return new WaitForSeconds(9f);
                ++tutorialCounter;

            }
            else if (tutorialCounter == 14)
            {

                if (playSoundOnce == 18)
                {
                    popSound.Play();
                    ++playSoundOnce;
                }

                textBoxes[1].text = "Approach each window, and press 'F' to cover them.";

                bool check = CheckIfWindowsAreCovered();

                if (check)
                {
                    ++tutorialCounter;
                }

            }
            else if (tutorialCounter == 15)
            {
                germSpreader.SetInfectedStatus(true); //player is now infected

                if (playSoundOnce == 19)
                {
                    popSound.Play();
                    ++playSoundOnce;
                }

                textBoxes[0].text = "Tutorial 10/12";
                textBoxes[1].text = "Yikes! Looks like you are infected. This happens when you come into contact with other infected items." +
                     "When infected, you will spread germs to the environment around you. If you are infected, you will notice a green germ next to your health bar.";
                yield return new WaitForSeconds(9f);
                ++tutorialCounter;

            }
            else if (tutorialCounter == 16)
            {
                if (tutorialSink != null)
                {
                    tutorialSink.SetActive(true);
                }

                if (playSoundOnce == 20)
                {
                    popSound.Play();
                    ++playSoundOnce;
                }
                textBoxes[1].text = "To remove the infected status, you will need to wash up in a sink. Be careful, germs will spread more quickly in wet environments. There is a sink at the bottom of this room - approach it, and press 'C' to wash up.";

                // if the player is no longer infected
                if (!germSpreader.GetInfectedStatus())
                {
                    ++tutorialCounter;
                }

            }
            else if (tutorialCounter == 17)
            {
                if (playSoundOnce == 21)
                {
                    popSound.Play();
                    ++playSoundOnce;
                }
                textBoxes[1].text = "Perfect. You are all clean! Notice that the germ next to your health bar is gone, indicating you are no longer infected or spreading germs.";
                yield return new WaitForSeconds(5f);
                ++tutorialCounter;
            }
            else if (tutorialCounter == 18)
            {
                if (!oneShot)
                {
                    healthController.TakeDamage(15); //make the player take damage 
                    oneShot = true;
                }

                if (tutorialHealthKit != null)
                {
                    tutorialHealthKit.SetActive(true);
                }
                if (playSoundOnce == 22)
                {
                    popSound.Play();
                    ++playSoundOnce;
                }

                textBoxes[0].text = "Tutorial 11/12";

                textBoxes[1].text = "Ouch! You just took some damage. This will happen when you are hit by enemies. Let's heal you up before you move on to your first mission! Pick-up the health kit to heal yourself.";

                if (healthDestroyed.IsDestroyed())
                {
                    ++tutorialCounter;
                }
                //gurney
            } else if (tutorialCounter == 19)
            {
                
                gurney.SetActive(true);
       

                textBoxes[0].text = "Tutorial 12/12";
                textBoxes[1].text = "The last thing you need to learn is how to barricade doors. Lock the door above, and push the gurney into it. This will barricade the door.";

                if (!gurney.GetComponent<BoxCollider2D>().enabled)
                {
                    textBoxes[1].text = "Great. Be mindful, in some circumstances, once barricaded, doors cannot be unbarricaded. This can be adjusted using the level settings. ";
                    yield return new WaitForSecondsRealtime(6f);
                    ++tutorialCounter;
                }

            }  else if (tutorialCounter == 20)
            {
                if (endLevelDoor != null)
                {
                    endLevelDoor.SetActive(true);
                }

                if (playSoundOnce == 24)
                {
                    popSound.Play();
                    ++playSoundOnce;
                }
                textBoxes[0].text = "Tutorial 12/12";
                textBoxes[1].text = "Awesome job. You are all done the tutorial! Feel free to explore this room further if you need more practice. When you're ready to begin, head out the door on the left.";
                yield return new WaitForSeconds(8f);

                if (playSoundOnce == 25)
                {
                    popSound.Play();
                    ++playSoundOnce;
                }

                tutorialInterface.enabled = false; 

            } 
            yield return new WaitForSeconds(0.1f);
        }
        
    }

    public int GetTutorialCounter() { return tutorialCounter; }
            

    private bool CheckIfWindowsAreCovered()
    {
        bool check = true;

        GameObject[] windowsArray = GameObject.FindGameObjectsWithTag("Window");
        CoverWindow[] windows = new CoverWindow[windowsArray.Length];

        for (int i = 0; i < windows.Length; ++i)
        {
            windows[i] = windowsArray[i].GetComponent<CoverWindow>();
        }

        for (int i = 0; i < windows.Length; ++i)
        {
            //if the windows are still uncovered
            if (windows[i].windowCovered)
            {
                check = true; 
            } else
            {
                check = false;
                break;
            }
        }

        return check;

    }
          
    void CreateTutorialGerm()
    {
        germSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 40);
        GameObject newSprite = new GameObject();
        newSprite.AddComponent<SpriteRenderer>();
        //tag it so it can actually be cleaned up after 
        newSprite.tag = "Germs";
        spriteRenderer = newSprite.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = germSprite;
        //so that it is above the floor tiles
        spriteRenderer.sortingOrder = 3;
        //spawns near left side of room 
        spriteRenderer.transform.position = new Vector2(-5f, -2.8f);
       

    }
}
