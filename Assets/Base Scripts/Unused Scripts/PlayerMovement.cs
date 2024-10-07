using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
        //basic movement variables
        public float pSpeed = 4f;
        public Rigidbody2D rigidB;
        public Animator animator;

        private Vector2 playerMotion;

        //positioning variables
      //  public VectorPosition startPosition;

        //flags to disable movement
        private bool leftRightmovementActive = true;
        private bool movementActive = true;

        //objects that allow us to disable other functionalities
     //   public ControlNotebook notebook;


        private void Start()
        {
        //  transform.position = startPosition.initialPos;
        print(animator.gameObject.activeSelf);
        animator.gameObject.SetActive(true);
        }

        void Update()
        {

        animator.gameObject.SetActive(true);
            playerMotion.x = Input.GetAxisRaw("Horizontal");
            playerMotion.y = Input.GetAxisRaw("Vertical");

            //  print(playerMotion.x);
            //  print(playerMotion.y);

            animator.SetFloat("Horizontal", playerMotion.x);
            animator.SetFloat("Vertical", playerMotion.y);
            animator.SetFloat("Speed", playerMotion.sqrMagnitude);


        }

        void FixedUpdate()
        {
            /*for testing purposes only */
            if (!movementActive)
            {
                movementActive = true;
            }
            if (leftRightmovementActive && movementActive)
            {
                //both forward and side buttons being pressed
                if (playerMotion.x != 0 && playerMotion.y != 0)
                {
                    pSpeed = 2f;
                }
                else
                {
                    pSpeed = 4f;
                }

                rigidB.MovePosition(rigidB.position + playerMotion * pSpeed * Time.fixedDeltaTime); //consistent movement
            }
            else if ((!leftRightmovementActive && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) && movementActive) //stop left/right movement when leftRightMovement is inactive
            {
                rigidB.MovePosition(rigidB.position + playerMotion * pSpeed * Time.fixedDeltaTime);
            }

        }


        public void DisableAllMovement()
        {
            movementActive = false;

        }
        public void EnableAllMovement()
        {
            movementActive = true;
        }

        public void DisableLRMovement() { leftRightmovementActive = false; }

        public void EnabledLRMovement() { leftRightmovementActive = true; }

        //public void DisableMovement() { movementActive = false;  }
    }


