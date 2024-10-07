using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPhoneAnimation : MonoBehaviour
{
    
    //sounds
    public AudioSource phoneSound;
    private Animator[] phoneAnimator;

    int soundCheck;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] phones = GameObject.FindGameObjectsWithTag("ProduceSound");
        phoneAnimator = new Animator[phones.Length];

        for (int i = 0; i < phoneAnimator.Length; ++i)
        {
            phoneAnimator[i] = phones[i].GetComponent<Animator>();
            phoneAnimator[i].enabled = false;
        }


    }

    // Update is called once per frame
    void Update()
    {

        soundCheck = CheckProximityOfLoudObjects();

        if (soundCheck != -1 && !phoneSound.isPlaying)
        {
            phoneAnimator[soundCheck].enabled = true;
            phoneSound.Play();
            phoneAnimator[soundCheck].Play("DeskPhone_Play");

        }
    }

    public int CheckProximityOfLoudObjects()
    {
        GameObject[] loudObjects = GameObject.FindGameObjectsWithTag("ProduceSound");
        Transform player = GameObject.FindWithTag("Player").transform;

        for (int i = 0; i < loudObjects.Length; ++i)
        {
            //if player distance from doors is less than = to one (closer than germs)
            if (Vector3.Distance(player.position, loudObjects[i].transform.position) <= 2)
            {

                return i;
            }

        }

        for (int i = 0; i < phoneAnimator.Length; ++i)
        {
            phoneAnimator[i].Play("DeskPhone_Play", 0, 0f);
        }
        // radioAnimator.enabled = false;

        return -1;
    }
}
