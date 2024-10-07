using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCallButton : MonoBehaviour
{

    public Canvas cellPhoneInterface;
    
    public void OnClick()
    {
        cellPhoneInterface.enabled = false; 
    }
}
