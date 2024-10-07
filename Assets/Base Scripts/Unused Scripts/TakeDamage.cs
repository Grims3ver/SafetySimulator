using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;

public class TakeDamage : MonoBehaviour
{

    
    public GameObject player;
    private Health healthController;

    private void Start()
    {
       healthController = player.GetComponent<Health>();
    }

    public void Update()
    {
       bool playerInRange = CheckPlayerDistance();

        if (playerInRange)
        {
            healthController.Damage(10, this.gameObject, 1f, 3f, new Vector3(0, 0, 0));
            print("doing damage, health is " + healthController.CurrentHealth);
            playerInRange = false; 
        }
    }
    public bool CheckPlayerDistance()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= 1.5)
        {
            return true;
        }

        return false; 
    }
}
