using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowPath : MonoBehaviour
{

    [SerializeField]
    private Transform[] pathPoints; //serialized, visible in unity 

    private int currentPoint = 0; //start at the first point 

    private float walkSpeed = 1f; //default to 1 for now 

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D enemyBody;
    private BoxCollider2D enemyCollider;

    private bool isAtEndLevelDoor = false;
    private GameObject[] doors;




    void Start()
    {

        //to do: this may need to change to RB based movement, idk 

        enemyBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyCollider = GetComponent<BoxCollider2D>();

        doors = GameObject.FindGameObjectsWithTag("Doors");
    }

    // Update is called once per frame
    void Update()
    {
        //checks if enemy has reached end of path without reaching a door (restart path) -- used for patrolling enemies
        if (!isAtEndLevelDoor && currentPoint == pathPoints.Length)
        {
            currentPoint = 0;
        }

    }
    private void FixedUpdate()
    {
        WalkPath();
    }

    private void WalkPath()
    {
        if (currentPoint <= pathPoints.Length - 1) //if there are still path points to go (not arrived)
        {
            Vector2 positionToMove = Vector2.MoveTowards(transform.position, pathPoints[currentPoint].transform.position, walkSpeed * Time.deltaTime);
           enemyBody.MovePosition(positionToMove);

            if (transform.position == pathPoints[currentPoint].position)
            {
                ++currentPoint;
            }
        }
    }

    //for interacting with triggers
    void OnTriggerEnter2D(Collider2D itemBeingHit)
    {

        //slightly misleading, but too late to change my tags now - this is for end level doors
        if (itemBeingHit.gameObject.tag == "Doors")
        {
            isAtEndLevelDoor = true; //this is to differentiate between an enemy reach the end of it's path (reset), or hitting a door

           spriteRenderer.enabled = false; //disable rendering
           enemyCollider.enabled = false; //disable collider temporarily (so the player cannot accidentally hit them)

            StartCoroutine("EnemySpawn");
        }
        
    }


    IEnumerator EnemySpawn()
    {
        //wait between 8-30 seconds for now 
        float randWait = Random.Range(8f, 30f);
      

        yield return new WaitForSeconds(randWait); //wait a random amount of time 

        int randDoor = Random.Range(0, doors.Length); //pick a random door 
                                                      //print(randDoor);

        //if the chosen door contains a pathpoint & the enemy is present

        for (int i = 0; i < pathPoints.Length; ++i)
        {
            while (Vector3.Distance(doors[randDoor].gameObject.transform.position, pathPoints[i].transform.position) <= 0.5)
            {
                randDoor = Random.Range(0, doors.Length); //pick a random door 
                                                          // print("changed rand-door to: " + randDoor);
            }
        }

        //TODO add case for bottom door 

        //if door x pos is -ve, need to move in +ve y direction
        //if door x pos is zero, it's in the middle door, and it needs to move in the - y direction
        //if the door x pos is +ve, y needs to move in -ve x

        //left door
        if (doors[randDoor].gameObject.transform.position.x < 0)
        { 
            spriteRenderer.transform.position = new Vector2(doors[randDoor].gameObject.transform.position.x, doors[randDoor].gameObject.transform.position.y + 0.03f);
        }
        //upper door
        else if (doors[randDoor].gameObject.transform.position.x == 0)
        {
            spriteRenderer.transform.position = new Vector2(doors[randDoor].gameObject.transform.position.x, doors[randDoor].gameObject.transform.position.y - 2.0f);
        }
        //right door
        else if (doors[randDoor].gameObject.transform.position.x > 0)
        {
            spriteRenderer.transform.position = new Vector2(doors[randDoor].gameObject.transform.position.x - 2.0f, doors[randDoor].gameObject.transform.position.y);
        }

        spriteRenderer.enabled = true;
        enemyCollider.enabled = true;
        currentPoint = 0; //reset path

    }

}








