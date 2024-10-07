using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeSiver_Controller : MonoBehaviour
{
    //this script controls the flow of events in a Code Silver event

    public ControlTasks tasker;
    public ControlEmergencyPhoneScene phoneCutscene;
    public ControlUpdateCanvas update; 

    bool crossedOff = false;

    void Update()
    {
        if(phoneCutscene.isCodeSilverOver && !crossedOff)
        {
            tasker.SetTaskCompletion(0);
            crossedOff = true;
            update.StartCoroutine("ShowCodexUpdate");

        }
    }
}
