using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour
{
    //this script is responsible for the level select 
    private static string levelSelected = "";

    public GameObject levelSelectionScreen;
    public GameObject difficultySelectionScreen;
    public GameObject mainMenuScreen;

    public void OnClickTutorialLevel()
    {
        //tutorial does not have difficulty select because... tutorial?
        levelSelected = "Tutorial";
        SceneManager.LoadScene(levelSelected);
    }

    public void OnClickCodeSilverLevel()
    {
        levelSelected = "Code_Silver";
        levelSelectionScreen.SetActive(false);
        difficultySelectionScreen.SetActive(true);
    }

    public void OnClickCodeSilverHideLevel()
    {
        levelSelected = "Code_S_Hide";
        levelSelectionScreen.SetActive(false);
        difficultySelectionScreen.SetActive(true);
    }

    public void OnClickBackButton()
    {
        levelSelectionScreen.SetActive(false);
        mainMenuScreen.SetActive(true);
    }

    public string GetLevelSelected()
    {
        return levelSelected;
    }

    public void SetLevelSelected(string str)
    {
        levelSelected = str; 
    }
}
