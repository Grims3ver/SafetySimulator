using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnNearby : MonoBehaviour
{
    //this is a personal script, written to allow enemies to damage players when nearby
    //they also damage when touch, which is handled by a script provided in the TopDown engine

    [HideInInspector]
    public int damageTaken = 5;
    public GameObject player;
    private ControlHealth playerHealth;

    private bool colliding = false;
    private bool coroutineRunning = false;
    void Start()
    {
        //get player health controller
        playerHealth = player.GetComponent<ControlHealth>();
    }

    void Update()
    {
       bool isNearby = CheckIfPlayerNearby();

        //if the enemy is near the player, not colliding (damage on collision is handled in another script), and if the coroutine is not running
       if (isNearby && !colliding && !coroutineRunning)
        {
            StartCoroutine("SlowlyDamage");
            coroutineRunning = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            colliding = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            colliding = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            colliding = false;
        }
    }

    //slowly damage the player when in proximity
    private IEnumerator SlowlyDamage()
    {
        playerHealth.TakeDamage(damageTaken);
       
        yield return new WaitForSecondsRealtime(1f);
        coroutineRunning = false; 

        StopCoroutine("SlowlyDamage");

    }

    //determines if player is nearby enemy
    private bool CheckIfPlayerNearby()
    {
        if ((Mathf.Abs(player.transform.position.x - this.transform.position.x) < 2) && (Mathf.Abs(player.transform.position.y - this.transform.position.y) < 2))
        {
            return true;
        }

        return false;
    }
}
