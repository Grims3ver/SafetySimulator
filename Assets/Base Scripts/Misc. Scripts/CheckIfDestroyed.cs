using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfDestroyed : MonoBehaviour
{
    bool isDestroyed = false; 
    private void OnDestroy()
    { 
        isDestroyed = true; 
    }

    public bool IsDestroyed() { return isDestroyed;  }
}
