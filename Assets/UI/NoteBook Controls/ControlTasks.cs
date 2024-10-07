using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ControlTasks : MonoBehaviour
{
    private TextMeshProUGUI[] textBoxes;
    private bool[] tasksCompleted;
    private Scene currentScene;
    public Image currentItemToFind;
    public Sprite emergencyPhone;
    public Sprite emergencyExit;

    public TextMeshProUGUI taskOne;
    public TextMeshProUGUI taskTwo;
    public TextMeshProUGUI taskThree;
    public TextMeshProUGUI taskFour;
    public TextMeshProUGUI taskFive;
    public TextMeshProUGUI promptSix;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene();

        /*
         * Cannot use GetComponentsInChildren, doesn't work. 
         * Cannot make the object proper children, messes up transform. 
         * Each text object then has a public setter. 
         */

        textBoxes = new TextMeshProUGUI[6];

        tasksCompleted = new bool[6];
        //manually setting the references, unfortunately 
        textBoxes[0] = taskOne;
        textBoxes[1] = taskTwo;
        textBoxes[2] = taskThree;
        textBoxes[3] = taskFour;
        textBoxes[4] = taskFive;
        textBoxes[5] = promptSix; 

        //this for loop sets all tasks as incomplete
        for (int i = 0; i < tasksCompleted.Length; ++i)
        {
            tasksCompleted[i] = false;
           
        }
    }

    private void Update()
    {
        //this entire if is just for setting things properly in the code-silver room
        if (currentScene.name.Equals("Code_Silver"))
        {
            if (textBoxes[5].text.Equals(""))
            {
                textBoxes[5].text = "Item to Locate: ";
            }

            if (!tasksCompleted[0])
            {
                currentItemToFind.sprite = emergencyPhone;
            }
            else if (tasksCompleted[0] && !tasksCompleted[1])
            {
                textBoxes[1].text = "Find the exit and escape!";
                currentItemToFind.sprite = emergencyExit; 
            }
        } else if (currentScene.name.Equals("Code_S_Hide"))
        {
            textBoxes[0].text = "Turn off any noise making objects.";
            textBoxes[1].text = "Silence your cellphone.";
            textBoxes[2].text = "Cover windows.";
            textBoxes[3].text = "Lock all available doors.";
            textBoxes[4].text = "Hide!";
        }
    }

    private void CheckTaskCompletion()
    {
        for (int i = 0; i < tasksCompleted.Length; ++i)
        {
            if (tasksCompleted[i] == true) //for clarity
            {
                textBoxes[i].fontStyle = FontStyles.Strikethrough; //cross off the completed task
            }
        }
    }

    public void SetTaskCompletion(int index)
    {
       // print("Setting task " + index + " as complete.");
        tasksCompleted[index] = true;

        CheckTaskCompletion();
    }
  

}
