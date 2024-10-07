using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //load 

public class LoadCodeSilverLevel : MonoBehaviour
{
   public void OnClickCodeSilverButton()
    {
        SceneManager.LoadScene("Code_Silver");
    }
}
