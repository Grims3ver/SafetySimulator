using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AllowBarricadeRemoval : MonoBehaviour
{
    //true by default, doesn't need to be editable in inspector
    [HideInInspector]
    private bool allowDoorsUnbarricaded = true;

    //default text that indicates when the default setting is selected (on)
    public TextMeshProUGUI defaultText;

    private void Start()
    {
        //toggle begins at false
        defaultText.enabled = true;
    }

    public void ToggleBarricadeDoors()
    {
        allowDoorsUnbarricaded = !allowDoorsUnbarricaded; //toggle functionality

        //by default doors are NOT highlighted
        if (allowDoorsUnbarricaded)
        {
            defaultText.enabled = true;
        }
        else
        {
            defaultText.enabled = false;
        }
    }

    public bool GetBarricadeDoorStatus()
    {
        return allowDoorsUnbarricaded;
    }
}
