using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO; //contains .Sort()


public class ControlScores : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject scoreMenu;

    public TextMeshProUGUI scoreText;

    private List<int> scoreArray;
    //sets button behaviour onClick()
    public void OnClick()
    {
        scoreArray = new List<int>();
        mainMenu.SetActive(false);
        scoreMenu.SetActive(true);
       UpdateScoreList();
    }
    

    public void UpdateScoreList()
    {
        string scorePath = Application.persistentDataPath + "/scores.txt";


        if (!File.Exists(scorePath))
        {
            File.WriteAllText(scorePath, "");
        }

        StreamReader reader = new StreamReader(scorePath);
        string[] lines = File.ReadAllLines(scorePath);
        print(scorePath);
        reader.Close();

        for (int i = 0; i < lines.Length; ++i)
        {
            int holder;
            int.TryParse(lines[i], out holder);
            scoreArray.Add(holder);
        }

        //this ensures the document is sorted if the game opens with previous scores
        scoreArray.Sort();
        scoreArray.Reverse();

        DisplayTopFiveScores();
    }

    private void DisplayTopFiveScores()
    {

        if (scoreText.text != "")
        {
            scoreText.text = "";
        }

        int size = scoreArray.Count;

        if (size >= 5)
        {
            size = 5;
        }
      
        if (size == 0)
        {
            scoreText.text = "No scores yet - why not add some by playing?";
        }
        else
        {
            for (int i = 0; i < size; ++i)
            {
                scoreText.text += scoreArray[i] + "\n";
            }
        }
    }

}
