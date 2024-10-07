using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;

public class ControlHealth : MonoBehaviour
{
    
   public Health healthController; 
    public HealingItem healthKit;

    //   public AudioSource takeDamageSound;

    public void TakeDamage(int amnt)
    {
        healthController.SetHealth(healthController.CurrentHealth - amnt);
        healthController.UpdateHealthBar(true);

    }

    public int GetHealth()
    {
        return healthController.CurrentHealth;

    }

    public void HealPlayer(int healAmnt)
    {
        if (healthController.CurrentHealth + healAmnt > 100)
        {
            healthController.CurrentHealth = 100;
        }
        else
        {
            healthController.CurrentHealth += healAmnt;
        }
        //update the character's health bar

        healthController.UpdateHealthBar(true);

       
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //colliding with health kit
        if (collision.gameObject.tag.Equals("Heal"))
        {
            //by default the medkit restores 20 health
            HealPlayer(healthKit.healthRestored);
        }
       
        }


    public void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.GetComponent<Enemy>() != null)
        {
            switch (collision.gameObject.GetComponent<Enemy>().enemy.enemyType.ToString())
            {
                case "Shooter":
                   // TakeDamage(10);
                    break;
                case "Default":
                    print("I shouldn't be here, no behaviour has been set.");
                    break;
                default:
                    print("definitely shouldn't be here");
                    break;
            }
        }
    }
}





