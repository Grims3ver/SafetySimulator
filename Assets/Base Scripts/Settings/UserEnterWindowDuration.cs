using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System; //convert

public class UserEnterWindowDuration : MonoBehaviour
{

    public Canvas settings;

    public TMP_InputField windowField;
    public CoverWindow coverWindow; 

    public TextMeshProUGUI defaultText;

    private string userEnteredTime;
    private double time = 0; 

    // Start is called before the first frame update
    void Start()
    {
        defaultText.enabled = false; 
    }

    // Update is called once per frame
    void Update()
    {

        if (!windowField.text.Equals("") && settings.enabled)
        {
            UserEnteredWindowTime();
        }

    }
    public void UserEnteredWindowTime()
    {

        userEnteredTime = windowField.text;

        time = Convert.ToDouble(userEnteredTime);

        //default time is approximately 1.5 seconds
        if (time == 1.5)
        {
            defaultText.enabled = true;
        }
        else
        {
            defaultText.enabled = false;
        }

        coverWindow.SetCoverWindowTime((float)time);

    }

    public void IncrementWindowTime()
    {
        if (!windowField.text.Equals(""))
        {
            userEnteredTime = windowField.text;
            time = Convert.ToDouble(userEnteredTime);
            ++time;
            windowField.text = time.ToString();
        } else
        {
            windowField.text = time.ToString();
        }
    }

    public void DecrementWindowTime()
    {
        if (!windowField.text.Equals("") && time >= 1)
        {
            userEnteredTime = windowField.text;
            time = Convert.ToDouble(userEnteredTime);
            --time;
            windowField.text = time.ToString();
        } else if (!windowField.text.Equals("") && time < 1)
        {
            time = 0;
            windowField.text = time.ToString();
        }
        else if (windowField.text.Equals(""))
        {
            windowField.text = time.ToString();
        }
    }
}
