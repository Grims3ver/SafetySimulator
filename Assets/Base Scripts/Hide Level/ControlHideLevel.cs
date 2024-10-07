using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MoreMountains.TopDownEngine;
using System.IO;


public class ControlHideLevel : MonoBehaviour
{

    /*A note on Scoring: 
     * Scores were determined using the original Code Silver handbook. Upon reading the section regarding hiding,
     * which this level focuses on, scores were based on the relative importance of each action. There are some actions,
     * such as locking doors or silencing personal devices, which are considered higher 'value' actions than simply 
     * shutting off loud electronics. 
     * 
     * A base score of 10 was assigned to 'simple' actions, with increments of 10 being added depending on the level
     * of task importance. 
     * 
     */

    //variables used in score, calculated based on item tags
    private int tvsInLevel;
    private int doorsInLevel;
    private int windowsInLevel;
    private int radiosInLevel;

    //the main score variables
    private static int score;
    private List<int> scoreArray = new List<int>();

    //misc. level monitoring 
    public ControlPrebrief prebriefChecker;

    //used for monitoring doors & door score
    private GameObject[] doorsArray;
    private ControlSimpleDoor[] doors;
    private int doorsWereBarricaded = 0; 

    //monitoring windows
    private GameObject[] windowsArray;
    private CoverWindow[] windows;

    // monitoring radios
    private GameObject[] radiosArray;
    private ControlRadioAnimation[] radios;

    //monitoring tvs
    private GameObject[] tvsArray;
    private ControlTV[] tvs;

    //monitor cellphone volume status
    public PressVolume volumeButton;

    //monitor player hidden status
    public SpriteRenderer playerSprite;

    //the timer text
    private TextMeshProUGUI timerText;

    //used for ending win scene
    public ControlCodes codeController;
    public CharacterMovement character;

    public float timeRemaining = 121; //two minutes is 120 seconds, give 1 second of "breathing room"

    private bool timerStart;
    private int[] allTasksCompleted;
    private bool checkAllTasksDone = false;
    private bool gameLoss = false;
    private int oneCheck = 0;

    //keep track of number of elements missed
    private int missedPhone = 0;
    private int missedRadios = 0;
    private int missedWindows = 0;
    private int missedDoors = 0;
    private int missedTVs = 0;

    //used for ending of level
    public Canvas codeCanvas;
    private Canvas timerCanvas;

    //used for win/lose messages
    public Canvas endLevelCanvas;
    public TextMeshProUGUI bodyText;

    void Start()
    {
        print(Application.persistentDataPath);

        //set up level item amounts
        tvsInLevel = GameObject.FindGameObjectsWithTag("TV").Length;
        doorsInLevel = GameObject.FindGameObjectsWithTag("LockableDoor").Length;
        windowsInLevel = GameObject.FindGameObjectsWithTag("Window").Length;
        radiosInLevel = GameObject.FindGameObjectsWithTag("Radio").Length;

        //set up level, timer and task monitoring
        timerCanvas = this.GetComponent<Canvas>();
        timerText = timerCanvas.GetComponentInChildren<TextMeshProUGUI>();

        timerStart = true;
        allTasksCompleted = new int[6];

        for (int i = 0; i < allTasksCompleted.Length; ++i)
        {
            allTasksCompleted[i] = 0;
        }

        endLevelCanvas.enabled = false;

        //set up items to monitor
        //first set up doors
        doorsArray = GameObject.FindGameObjectsWithTag("LockableDoor");
        doors = new ControlSimpleDoor[doorsArray.Length];

        for (int i = 0; i < doorsArray.Length; ++i)
        {
            doors[i] = doorsArray[i].GetComponent<ControlSimpleDoor>();
        }

        //set up windows
        windowsArray = GameObject.FindGameObjectsWithTag("Window");
        windows = new CoverWindow[windowsArray.Length];

        for (int i = 0; i < windowsArray.Length; ++i)
        {
            windows[i] = windowsArray[i].GetComponent<CoverWindow>();
        }

        //set up radios
        radiosArray = GameObject.FindGameObjectsWithTag("Radio");
        radios = new ControlRadioAnimation[radiosArray.Length];

        for (int i = 0; i < radiosArray.Length; ++i)
        {
            radios[i] = radiosArray[i].GetComponent<ControlRadioAnimation>();
        }

        //set up tvs
        tvsArray = GameObject.FindGameObjectsWithTag("TV");
        tvs = new ControlTV[tvsArray.Length];

        for (int i = 0; i < tvsArray.Length; ++i)
        {
            tvs[i] = tvsArray[i].GetComponent<ControlTV>();
        }

        //reset score upon restart
        score = 0;

    }


    void FixedUpdate()
    {

        timerStart = CheckPrebriefComplete();

        if (timeRemaining > 0 && timerStart)
        {
            timeRemaining -= Time.deltaTime;
            DisplayCountDownTime();

        }
    }

    private void Update()
    {
        if (gameLoss && oneCheck == 0)
        {
            CheckFailureState();
            oneCheck = 1;
            DisplayFailureMessage();
        }

        checkAllTasksDone = CheckAllTasksComplete();

        //level successfully completed
        if (checkAllTasksDone && timeRemaining > 0 && oneCheck == 0)
        {
            codeController.EnabledCodeCanvas();
            oneCheck = 1;
            character.MovementForbidden = true;
            timerStart = false;
            StartCoroutine("Wait");
           


        }
        else if (timeRemaining < 0 && !checkAllTasksDone)
        {
            gameLoss = true;
            character.MovementForbidden = true;
        }

    }

    private void DisplayFailureMessage()
    {
        string failureMsg = "";
        string[] failureMsgArray = new string[6];

        //these if statements just ensure the proper grammar is used in the output to the user
        if (missedDoors == 1)
        {
            failureMsgArray[0] = "You left " + missedDoors + " door unlocked.\n";
        }
        else
        {
            failureMsgArray[0] = "You left " + missedDoors + " doors unlocked.\n";
        }

        if (missedWindows == 1)
        {
            failureMsgArray[1] = "You left " + missedWindows + " window uncovered.\n";
        }
        else
        {
            failureMsgArray[1] = "You left " + missedWindows + " windows uncovered. \n";
        }

        if (missedRadios == 1)
        {
            failureMsgArray[2] = "You left " + missedRadios + " radio on.\n";
        }
        else
        {
            failureMsgArray[2] = "You left " + missedRadios + " radios on.\n";
        }

        if (missedTVs == 1)
        {
            failureMsgArray[3] = "You left " + missedTVs + " television on.\n";
        }
        else
        {
            failureMsgArray[3] = "You left " + missedTVs + " televisions on.\n";
        }


        failureMsgArray[4] = "You left your cellphone's volume on loud.\n";

        failureMsgArray[5] = "You forgot to hide! Don't forget to hide in one of the orange areas after completing all tasks.";


        if (gameLoss)
        {
            allTasksCompleted[0] = CheckDoorsLocked();
            allTasksCompleted[1] = CheckWindowsCovered();
            allTasksCompleted[2] = CheckRadiosOff();
            allTasksCompleted[3] = CheckTVsOff();
            allTasksCompleted[4] = missedPhone;
            allTasksCompleted[5] = CheckProperlyHidden();

            for (int i = 0; i < allTasksCompleted.Length; ++i)
            {
                if (allTasksCompleted[i] > 0)
                {
                    failureMsg += failureMsgArray[i];
                }
            }

            endLevelCanvas.enabled = true;
            

            score = CalculateFinalScore();

            if (doorsWereBarricaded > 0)
            {
                failureMsg += "\nYou got " + doorsWereBarricaded + " extra points for barricading doors.\n";
            }

            failureMsg += "\nFinal Score: " + score;
            bodyText.text = failureMsg;

        }

    }
    int CheckProperlyHidden()
    {
        if (!playerSprite.enabled)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }

    //this method is responsible for converting the time to something easier to view - minutes and seconds
    void DisplayCountDownTime()
    {
        float minutes = 0;
        float seconds = 0;

        if (timeRemaining > 0 && !CheckAllTasksComplete())
        {
            minutes = Mathf.FloorToInt(timeRemaining / 60);
            seconds = Mathf.FloorToInt(timeRemaining % 60);
        }
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private bool CheckAllTasksComplete()
    {
        bool tempCheck = false;

        if (CheckWindowsCovered() == 0 && CheckRadiosOff() == 0 && CheckDoorsLocked() == 0 && CheckProperlyHidden() == 0 && CheckTVsOff() == 0 && !IsCellphoneOnLoud())
        {
            tempCheck = true;
        }

        return tempCheck;
    }

    private int CheckWindowsCovered()
    {
        int temp = 0;
        for (int i = 0; i < windows.Length; ++i)
        {
            if (!windows[i].windowCovered)
            {
                ++temp;
            }
        }

        return temp;
    }


    private int CheckTVsOff()
    {
        int temp = 0;
        for (int i = 0; i < tvs.Length; ++i)
        {
            if (tvs[i].isOn)
            {
                ++temp;
            }
        }

        return temp;
    }


    private int CheckRadiosOff()
    {
        int temp = 0;
        for (int i = 0; i < radios.Length; ++i)
        {
            if (!radios[i].radioOff)
            {
                ++temp;
            }
        }

        return temp;
    }

    private int CheckDoorsBarricaded()
    {
        int temp = 0;
        for (int i = 0; i < doors.Length; ++i)
        {
            if (doors[i].doorIsBarricaded)
            {
                ++temp;
            }

        }
        return temp;
    }
    private int CheckDoorsLocked()
    {
        int temp = 0;
        for (int i = 0; i < doors.Length; ++i)
        {
            if (doors[i].IsDoorLocked())
            {
                continue;
                //current behaviour - barricading only adds to the score, it is NOT necessary to beat the level
            }
            else
            {
                ++temp;
            }
        }

        return temp;

    }

    private bool CheckPrebriefComplete()
    {
        return prebriefChecker.GetPreBriefStatus();
    }

    private bool IsCellphoneOnLoud()
    {
        return volumeButton.volumeOn;
    }

    private void CheckFailureState()
    {
        missedRadios = CheckRadiosOff();
        missedWindows = CheckWindowsCovered();
        missedDoors = CheckDoorsLocked();
        missedTVs = CheckTVsOff();

        if (volumeButton.volumeOn)
        {
            missedPhone = 1;
        }

    }

    //calculates final score. Note score is based on Code Silver documentation. 
    private int CalculateFinalScore()
    {
        //score for silencing phone and hiding 
        int phoneScore = 0;
        int hideScore = 0;

        //each window is worth 20 points
        int windowScore = (windowsInLevel - missedWindows) * 20;
       // print("Windows " + windowScore);

        //each tv is worth 20 points
        int tvScore = (tvsInLevel - missedTVs) * 20;
      //  print("TVs" + tvScore);

        //each radio is worth 20 points 
        int radioScore = (radiosInLevel - missedRadios) * 20;
       // print("Radios " + radioScore);

        int barricadedDoorScore = CheckDoorsBarricaded() * 100; //each barricaded door is worth 100 points
        doorsWereBarricaded = barricadedDoorScore;

        if (missedPhone != 1)
        {
            //30 points for silencing phone
            phoneScore = 30;
        }
      //  print("Phone! " + phoneScore);

        //hiding in a closet is worth 50 points
        if (CheckProperlyHidden() == 0)
        {
            hideScore += 50;
        }

      //  print("Hide " + hideScore);
        //each door is worth 10 points
        int doorScore = (doorsInLevel - missedDoors) * 10;
        //  print("Doors " + doorScore);
        //by casting time remaining to an int, if it is less than 1, it is not added to the score. Important if level is failed.


        score = phoneScore + hideScore + windowScore + tvScore + radioScore + doorScore + barricadedDoorScore + (int)timeRemaining;

        ReadScoreFromFile();
        WriteScoreToFile();

        return score;

    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        endLevelCanvas.enabled = true;
        score = CalculateFinalScore();
      //  WriteNewScoreToFile();
        bodyText.text = "Well done, you have safely secured the building. You were able to sucessfully HIDE, and now can now safely contact the Switchboard." +
        "\nFinal Score: " + score;

        if (doorsWereBarricaded > 0)
        {
            bodyText.text += "\nYou got " + doorsWereBarricaded + " extra points for barricading doors.\n";
        }

        StopCoroutine("Wait");


    }


    public void SetTimerDuration(float userTimer)
    {
        if (userTimer != timeRemaining && userTimer > 0)
        {
            timeRemaining = userTimer; //add one second so that time does not begin at 1 second off - ostensibly, 1 second to allow level to load properly
        }
       
    }

    private void ReadScoreFromFile()
    {
        string scorePath = Application.persistentDataPath + "/scores.txt";

        if (!File.Exists(scorePath))
        {
            File.WriteAllText(scorePath, "");
        }

        StreamReader reader = new StreamReader(scorePath);
        string[] lines = File.ReadAllLines(scorePath);
        reader.Close();

        for (int i = 0; i < lines.Length; ++i)
        {
            int holder;
            int.TryParse(lines[i], out holder);
            scoreArray.Add(holder);
        }

       
        //add the new score
        scoreArray.Add(score);
        //sort the array in descending order
        scoreArray.Sort();
        scoreArray.Reverse();


    }

    
    //write the new score to a file to grab it later
    private void WriteScoreToFile()
    {
        string scorePath = Application.persistentDataPath + "/scores.txt";

        //the file should definitely exist by the time we get here, but just in case...
        if (!File.Exists(scorePath))
        {
            File.WriteAllText(scorePath, "");
        }

        //do not append, overwrite each time
        StreamWriter writer = new StreamWriter(scorePath, false);
        for (int i = 0; i < scoreArray.Count; ++i)
        {
            writer.WriteLine(scoreArray[i]);
        }
        writer.Close();
       
    } 
}
