using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfColliderTriggered : MonoBehaviour
{
    public bool headTriggered;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        headTriggered = true; 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        headTriggered = false; 
    }

    
}
