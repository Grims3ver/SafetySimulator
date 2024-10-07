using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ControlNotebook : MonoBehaviour
{

    public Canvas noteBookCanvas;
    private bool notebookInterfaceEnabled;


    public void Start()
    { 
        notebookInterfaceEnabled = true; //by default it is usable
    }

    public void Update()
    {
        OpenNoteBook();
    }


    public void OpenNoteBook()
    {
        if (Input.GetKeyUp(KeyCode.N) && !noteBookCanvas.enabled && notebookInterfaceEnabled)
        {
            noteBookCanvas.enabled = true;
        }
        else if (Input.GetKeyUp(KeyCode.N) && noteBookCanvas.enabled && notebookInterfaceEnabled)
        {
            noteBookCanvas.enabled = false;
        }
    }

    public void DisableNotebookInterface() { notebookInterfaceEnabled = false;  }
    public void EnableNotebookInterface() { notebookInterfaceEnabled = true;  }
}
