using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System; // convert

public class UserEnterLevelDuration : MonoBehaviour
{
    public Canvas settings;

    public TMP_InputField timerField;
    public ControlHideLevel hideLevel;

    public TextMeshProUGUI defaultText;

    private string userEnteredTime;

    private double time = 0; 

   

    // Start is called before the first frame update
    void Start()
    {
        defaultText.enabled = false; 
    }

    private void Update()
    {
        //as the text field is set to take in doubles only, no need to check for non-integer characters
        if (!timerField.text.Equals("") && settings.enabled)
        {
            UserEnteredTime();
        }
    }

    //this function controls the user's entered time, and displays the default values
    public void UserEnteredTime()
    {

        userEnteredTime = timerField.text;

        time = Convert.ToDouble(userEnteredTime);

        if (time == 120)
        {
            defaultText.enabled = true; 
        } else
        {
            defaultText.enabled = false;
        }

        hideLevel.SetTimerDuration((float)time);
     
    }

    public void IncrementLevelTime()
    {
        if (!timerField.text.Equals(""))
        {
            userEnteredTime = timerField.text;
            time = Convert.ToDouble(userEnteredTime);
            ++time;
            timerField.text = time.ToString();
        }
        else
        {
            timerField.text = time.ToString();
        }
    }

    public void DecrementLevelTime()
    {
        if (!timerField.text.Equals("") && time >= 1)
        {
            userEnteredTime = timerField.text;
            time = Convert.ToDouble(userEnteredTime);
            --time;
            timerField.text = time.ToString();
        }
        else if (!timerField.text.Equals("") && time < 1)
        {
            time = 0;
            timerField.text = time.ToString();
        }
        else if (timerField.text.Equals(""))
        {
            timerField.text = time.ToString();
        }
    }

}
