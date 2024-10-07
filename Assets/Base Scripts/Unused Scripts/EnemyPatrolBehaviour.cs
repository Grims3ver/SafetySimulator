using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolBehaviour: MonoBehaviour
{
    //this is a secondary enemy movement script which mirrors the NPC movement - random movement points
    //are selected, except the enemy is also attracted to lockable doors and will search rooms

    //unlike the FollowPath script, this movement is relatively randomized 

    public float walkSpeed = 10f; //default to 1 for now 

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D enemyBody;
    private BoxCollider2D enemyCollider;

    private int lockableDoorNearby = -1;
    private List<GameObject> lockableDoors;

    private Vector2 nextPoint;
    private Vector2 updatingPosition;

    private bool[] doorwayChecked;

    // public CompositeCollider2D levelBounds;
    public ControlSimpleDoor doorController;

    public ControlEntryCutscene entryCutscene;

    private int check = -1;
    private float tempDist;
    private bool isRunning = false;


    void Start()
    {
        GameObject[] lockableDoorsTemp;
        enemyBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyCollider = GetComponent<BoxCollider2D>();

        lockableDoorsTemp = GameObject.FindGameObjectsWithTag("LockableDoor");
        doorwayChecked = new bool[lockableDoorsTemp.Length];
        lockableDoors = new List<GameObject>();

        for (int i = 0; i < lockableDoorsTemp.Length; ++i)
        {
            lockableDoors.Add(lockableDoorsTemp[i]);
            doorwayChecked[i] = false;
        }

        lockableDoorNearby = CheckProximityOfLockableDoors();
        SetNewPoint(lockableDoors[(int)lockableDoorNearby].transform.position.x, lockableDoors[(int)lockableDoorNearby].transform.position.y);



    }



    void FixedUpdate()
    {

        if (entryCutscene.cutscenePlayed)
        {
            //move enemy
            updatingPosition = Vector2.MoveTowards(enemyBody.transform.position, nextPoint, walkSpeed * Time.deltaTime);

            enemyBody.MovePosition(updatingPosition);
        }
     
        
        if (Mathf.Abs(updatingPosition.x - nextPoint.x) < 2 && Mathf.Abs(updatingPosition.y - nextPoint.y) < 2 && check == -1)
        {
            if (CheckNearbyDoorStatus())
            {
                StartCoroutine(ExploreRoom());
                //prevents multiple needless calls to coroutine
                check = 0;
            } else
            {
                print("HERE!");
                MoveToNextDoor();
            }
           
        }


    }

    bool CheckNearbyDoorStatus()
    {
        ControlSimpleDoor doorController = lockableDoors[(int)lockableDoorNearby].GetComponent<ControlSimpleDoor>();
        //if door is locked
        if (doorController.IsDoorLocked())
        {
            //don't proceed, move to the next door
            return false; 
        }
        return true; 
    }


    void MoveToNextDoor()
    {
       
        CheckIfFinishedPath(); 

        //find a new door
        lockableDoorNearby = CheckProximityOfLockableDoors();

        //set new point to new door location 
        SetNewPoint(lockableDoors[(int)lockableDoorNearby].transform.position.x, lockableDoors[(int)lockableDoorNearby].transform.position.y);

        check = -1;

    }
    
    //this method checks if path is finished - if so, the enemy resets and proceeds backwards, back the way they came
    void CheckIfFinishedPath()
    {
        bool isFinished = false;

        for (int i = 0; i < doorwayChecked.Length; ++i)
        {
            if (!doorwayChecked[i])
            {
                isFinished = false;
                break;
            }
            else
            {
                isFinished = true;
            }
        }

        if (isFinished)
        {
            
            ResetPath();
        }
    }


    void ResetPath()
    {
        for (int i = 0; i < doorwayChecked.Length; ++i)
        {
            if (i != lockableDoorNearby)
            {
                doorwayChecked[i] = false;
            }
        }
    }
    IEnumerator ExploreRoom()

    {
        isRunning = true;
     
        //Pause in doorway
        yield return new WaitForSeconds(1f);

        //if the door is facing the rightmost direction
        //"room" sizes is relatively standard, so the player should always be able to walk 4 units in
        if (lockableDoors[(int)lockableDoorNearby].transform.eulerAngles.z == 0)
        {
            //set the point in the room & move to it 
            SetNewPoint(lockableDoors[(int)lockableDoorNearby].transform.position.x + 4, lockableDoors[(int)lockableDoorNearby].transform.position.y);
            //wait until that point is reached
            yield return new WaitUntil(() => updatingPosition == nextPoint);
            //assign a new point which allows us to exit the room
            SetNewPoint(transform.position.x - 5, lockableDoors[(int)lockableDoorNearby].transform.position.y);
            //wait to exit
            yield return new WaitUntil(() => updatingPosition == nextPoint);

            doorwayChecked[(int)lockableDoorNearby] = true;
            MoveToNextDoor();
            StopCoroutine(ExploreRoom());
            //door is on bottom of the room
        }
        else if (lockableDoors[(int)lockableDoorNearby].transform.eulerAngles.z == 270)
        {
         
            SetNewPoint(lockableDoors[(int)lockableDoorNearby].transform.position.x, lockableDoors[(int)lockableDoorNearby].transform.position.y - 4);
            yield return new WaitUntil(() => updatingPosition == nextPoint);
            SetNewPoint(lockableDoors[(int)lockableDoorNearby].transform.position.x, transform.position.y + 5);
            yield return new WaitUntil(() => updatingPosition == nextPoint);

            doorwayChecked[(int)lockableDoorNearby] = true;
            MoveToNextDoor();
            StopCoroutine(ExploreRoom());
           

        }
        else if (lockableDoors[(int)lockableDoorNearby].transform.eulerAngles.z == 90)
        {

            SetNewPoint(lockableDoors[(int)lockableDoorNearby].transform.position.x, lockableDoors[(int)lockableDoorNearby].transform.position.y + 4);
            yield return new WaitUntil(() => updatingPosition == nextPoint);
            SetNewPoint(lockableDoors[(int)lockableDoorNearby].transform.position.x, transform.position.y - 5);
            yield return new WaitUntil(() => updatingPosition == nextPoint);
            doorwayChecked[(int)lockableDoorNearby] = true;
            MoveToNextDoor();
            
            StopCoroutine(ExploreRoom());
        } else
        {
            MoveToNextDoor();
            //just in case, move on 
        }

        isRunning = false;


    }



    //enemies look for doors
    int CheckProximityOfLockableDoors()
    {
        //should not be -1 by end of method
        int closestDoor = -1;

        //grab an unchecked door
        for (int i = 0; i < doorwayChecked.Length; ++i)
        {
            if (!doorwayChecked[i])
            {
                closestDoor = i;
                break;
            }
        }
       //determine how far it is 
        tempDist = Vector3.Distance(transform.position, lockableDoors[closestDoor].transform.position);

        for (int i = 0; i < lockableDoors.Count; ++i)
        {
            //determine the distance between the enemy and each door
            float minDist = Vector3.Distance(transform.position, lockableDoors[i].transform.position);

            //if the distance is less than the previous door, change it 
            if (minDist < tempDist && !doorwayChecked[i])
            {
                closestDoor = i;
            }

            tempDist = minDist;
        }

       
        return closestDoor;
    }

    //TODO: addcases for hitting the walls, and also, add case for left most door

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "LockableDoor")
        {
            float doorDirection = collision.gameObject.transform.rotation.z;
            // ExploreRoom(doorDirection);
        }
       
    }
    //set new point to travel to 
    void SetNewPoint(float x, float y)
    {
        nextPoint = new Vector2(x, y);
      
    }



}
