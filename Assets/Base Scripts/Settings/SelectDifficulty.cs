using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //for pointer handlers
using UnityEngine.SceneManagement; //for level loading

//this script sets the difficulty and loads the appropriate level
public class SelectDifficulty : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //which level did the user select
    public SelectLevel level;

    //which difficulty did they select
    public static string difficultySelected;

    //tooltips
    public GameObject easyTooltip;
    public GameObject mediumTooltip;
    public GameObject hardTooltip;

    //menu screens, used for back button
    public GameObject levelSelectionScreen;
    public GameObject difficultySelectionScreen;

    public void OnPointerEnter(PointerEventData eventData)
    {
        //the easy setting
        if (name.Equals("Intern"))
        {
            easyTooltip.SetActive(true);

        } //medium setting
        else if (name.Equals("Resident"))
        {
            mediumTooltip.SetActive(true);

        } //hard setting
        else if (name.Equals("Attending"))
        {
            hardTooltip.SetActive(true);
        }

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //the easy setting
        if (name.Equals("Intern"))
        {
            easyTooltip.SetActive(false);

        } //medium setting
        else if (name.Equals("Resident"))
        {
            mediumTooltip.SetActive(false);

        } //hard setting
        else if (name.Equals("Attending"))
        {
            hardTooltip.SetActive(false);
        }
    }

    public void OnSelectDifficultySetting()
    {

        //if by some miracle the user didn't select a proper level, just load the Code_Silver one 
        if (level.GetLevelSelected().Equals(""))
        {
            level.SetLevelSelected("Code_Silver");
        }

        if (!level.GetLevelSelected().Equals("Tutorial"))
        {
            //the easy setting
            if (name.Equals("Intern"))
            {
                difficultySelected = "Easy";

            } //medium setting
            else if (name.Equals("Resident"))
            {
                difficultySelected = "Medium";

            } //hard setting
            else if (name.Equals("Attending"))
            {
                difficultySelected = "Hard";
            }
        }

        SceneManager.LoadScene(level.GetLevelSelected());
    }

    public void OnSelectBackButton()
    {
        difficultySelectionScreen.SetActive(false);
        levelSelectionScreen.SetActive(true);
    }
}

