using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLBSlot : MonoBehaviour
{
    

    public void OnClickSlot1()
    {
        SceneManager.LoadScene("TilemapEditor");
    }

    public void OnClickSlot2()
    {
        SceneManager.LoadScene("TilemapEditor1");
    }

    public void OnClickSlot3()
    {
        SceneManager.LoadScene("TilemapEditor2");
    }
}
