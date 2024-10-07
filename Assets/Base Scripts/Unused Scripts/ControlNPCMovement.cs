using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlNPCMovement : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private float walkSpeed = 1f;


    private Vector2 randomPositionOnScreen;
    private Vector2 newPosition;
    private HeadTriggered head;


    /* NPC is designed to move left, right, up, or down only. 
     * Adds to NPC-like effect, and makes their movement more predictable*/


    void Start()
    {
        //todo: THIS WILL NEED TO BE UPDATED WHEN MORE ENEMY TYPES ARE ADDED
        rigidBody = GetComponent<Rigidbody2D>();
        head = GetComponentInChildren<HeadTriggered>(); 
    }

    void FixedUpdate()
    {
        
        newPosition = Vector2.MoveTowards(rigidBody.transform.position, randomPositionOnScreen, walkSpeed * Time.deltaTime);
        
        rigidBody.MovePosition(newPosition);

        if (Mathf.Abs(newPosition.x - randomPositionOnScreen.x) < 0.1 && Mathf.Abs(newPosition.y - randomPositionOnScreen.y) < 0.1)
        {

            if (walkSpeed == 2f)
            {
                walkSpeed = 1f;
            }

            GenerateNewPoint();
        }
    }

    private IEnumerator Idle(float idleTime)
    {
        //TODO: add idle animation to play 

        yield return new WaitForSeconds(idleTime);
    }

    

    void GenerateNewPoint()
    {

        float randomizedXDirection = Random.Range(-6.7f, 6.9f);

        float randomizedYDirection = Random.Range(-4.5f, 4.8f);

        float randVal = Random.Range(-1f, 1f);
        float randIdle = Random.Range(0f, 4f);

        StartCoroutine(Idle(randIdle));
        

        //turn in a random 4-way direction
        if (randVal > 0)
        {
            randomPositionOnScreen = new Vector2(randomizedXDirection, newPosition.y);
        }
        else
        {
            randomPositionOnScreen = new Vector2(newPosition.x, randomizedYDirection);
        }

    }

    void SetNewPoint(float x, float y)
    {
        randomPositionOnScreen = new Vector2(x, y);
    }
   

    //new idea: move position, not MoveRotation for this rigid body - maybe try again tmrw if you feel like it :) 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (walkSpeed == 2f)
        {
            walkSpeed = 1f;
        }

        if (collision.gameObject.tag == "Shooter")
        {
            //todo: add more cases as continued enemy interactions continue
            walkSpeed = 2f;
            if (!head.GetHeadStatus())
            {
                SetNewPoint(newPosition.x * (-1f), newPosition.y * (-1f));
            } else if (head.GetHeadStatus() && newPosition.y < 0)
            {
                SetNewPoint(newPosition.x, newPosition.y - 3);
            } else
            {
                SetNewPoint(newPosition.x, newPosition.y + 3);
            }
        }
        else
        {
            rigidBody.MoveRotation(90f);
            GenerateNewPoint();
        }
    }

}
