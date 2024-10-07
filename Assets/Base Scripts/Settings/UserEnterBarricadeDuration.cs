using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System; //convert

public class UserEnterBarricadeDuration : MonoBehaviour
{
    public Canvas settings;

    public TMP_InputField barricadeTimeField;
    public Gurney barricadeDoor;

    public TextMeshProUGUI defaultText;

    private string userEnteredBarricadeTime;

    private double time = 0;



    // Start is called before the first frame update
    void Start()
    {
        defaultText.enabled = false;
    }

    private void Update()
    {
        //as the text field is set to take in doubles only, no need to check for non-integer characters
        if (!barricadeTimeField.text.Equals("") && settings.enabled)
        {
            UserEnteredTime();
        }
    }

    //this function controls the user's entered time, and displays the default values
    public void UserEnteredTime()
    {

        userEnteredBarricadeTime = barricadeTimeField.text;

        time = Convert.ToDouble(userEnteredBarricadeTime);

        //default time is 3 seconds
        if (time == 3)
        {
            defaultText.enabled = true;
        }
        else
        {
            defaultText.enabled = false;
        }

        barricadeDoor.SetBarricadeTime((float)time);
        
    }

    //increments barricade time, plans for multiple scenarios 
    //if timer field is empty, it begins at 0
    public void IncrementBarricadeTime()
    {
        if (!barricadeTimeField.text.Equals(""))
        {
            userEnteredBarricadeTime = barricadeTimeField.text;
            time = Convert.ToDouble(userEnteredBarricadeTime);
            ++time;
            barricadeTimeField.text = time.ToString();
        }
        else
        {
            barricadeTimeField.text = time.ToString();
        }
    }

    public void DecrementBarricadeTime()
    {
        if (!barricadeTimeField.text.Equals("") && time >= 1)
        {
            userEnteredBarricadeTime = barricadeTimeField.text;
            time = Convert.ToDouble(userEnteredBarricadeTime);
            --time;
            barricadeTimeField.text = time.ToString();
        }
        else if (!barricadeTimeField.text.Equals("") && time < 1)
        {
            time = 0;
            barricadeTimeField.text = time.ToString();
        }
        else if (barricadeTimeField.text.Equals(""))
        {
            barricadeTimeField.text = time.ToString();
        }
    }


}
