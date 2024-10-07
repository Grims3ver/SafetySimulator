using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{

    public SpreadGerms germSpreader;
    public ControlHealth healthController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.gameObject.tag.Equals("Cleaner"))
        {
            germSpreader.SetInfectedStatus(false); //player is no longer infected
            Destroy(collision.gameObject); //destroy the cleaner

        } else if (collision.gameObject.tag.Equals("Heal"))
        {
            healthController.HealPlayer(15);
            Destroy(collision.gameObject); //destroy the cleaner
        }
    }

    
}
