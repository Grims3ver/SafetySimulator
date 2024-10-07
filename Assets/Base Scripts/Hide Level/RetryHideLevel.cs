using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RetryHideLevel : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("Code_S_Hide");
    }
}
