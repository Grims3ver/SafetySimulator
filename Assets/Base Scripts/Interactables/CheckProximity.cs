using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckProximity : MonoBehaviour
{
    private bool germCheck; //checks for proximity of germs
    private GameObject nearestGermSprite;

    private bool doorCheck = false; 
    // public Sprite blankSprite;


    void Update() //called every frame 
    {

        germCheck = CheckProximityOfGerms();
        if (germCheck && Input.GetKey(KeyCode.F))
        {
            CleanGerms();
        }
        //  print(germCheck); tracer

        doorCheck = CheckProximityOfDoors();

    }

    void CleanGerms()
    {
        //TODO: add functionality for increasing score when germs are cleaned? 
        //Add minigame function... What could be a fun minigame involving cleaning?
        // print("Cleaning germs!"); tracer

        Destroy(nearestGermSprite);
        germCheck = true;
    }

    public bool CheckProximityOfGerms()
    {
        GameObject[] germs = GameObject.FindGameObjectsWithTag("Germs");

        for (int i = 0; i < germs.Length; ++i)
        {
            //if player distance from germs is less than = two
            if (Vector3.Distance(transform.position, germs[i].transform.position) <= 2)
            {
                nearestGermSprite = germs[i];
                /// print(nearestGermSprite.transform.position); love me my tracer code, haha
                return true;
            }

        }
        return false;
    }

    public bool CheckProximityOfDoors()
    {
        GameObject[] doors = GameObject.FindGameObjectsWithTag("Doors");


        for (int i = 0; i < doors.Length; ++i)
        {
            //if player distance from doors is less than = to one (closer than germs)
            if (Vector3.Distance(transform.position, doors[i].transform.position) <= 0.5)
            {
                return true;
            }

        }
        return false;
    }

    




}



