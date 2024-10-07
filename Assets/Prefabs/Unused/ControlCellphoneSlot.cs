using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCellphoneSlot : MonoBehaviour
{
    public Canvas cellPhoneInterface;

    public void Start()
    {
       
        cellPhoneInterface.enabled = false; 
    }
    public void OnClick()
    {
        
       if (cellPhoneInterface.enabled)
        {
            cellPhoneInterface.enabled = false; 
        } else
        {
            cellPhoneInterface.enabled = true;
        }
    }

}
