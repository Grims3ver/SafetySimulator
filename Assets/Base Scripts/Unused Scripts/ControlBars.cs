using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBars : MonoBehaviour
{
    [SerializeField] private GameObject blackBarController;
    [SerializeField] private Animator cinematicController;

    private void Start()
    {
        blackBarController.SetActive(false);
    }
    public void ShowCinematicBars()
    {
        blackBarController.SetActive(true);
        //apparently setting the parent active doesn't reactivate the children? weird
        blackBarController.transform.GetChild(0).gameObject.SetActive(true); //activate top bar
        blackBarController.transform.GetChild(1).gameObject.SetActive(true); //activate bottom bar

      
    }

    public void HideCinematicBars()
    {
        //if present in scene and active
        if (blackBarController.activeSelf)
        {
            StartCoroutine("WaitForAnimationToPlay");
        }
        
    }

    
    private IEnumerator WaitForAnimationToPlay()
    {
       // print("disabling");
        cinematicController.SetTrigger("EndCutscene");
        yield return new WaitForSeconds(2f);
        blackBarController.SetActive(false);
        StopCoroutine("WaitForAnimationToPlay");
    } 
}
