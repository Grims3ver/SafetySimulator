using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadHideLevel : MonoBehaviour
{
    public void OnClickHideLevel()
    {
        SceneManager.LoadScene("Code_S_Hide");
    }
}
