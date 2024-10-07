using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideInCloset : MonoBehaviour
{
    //player sprites
    public SpriteRenderer playerSprite;
    public Sprite hiddenCloset; //different sprite when the player is hidden in the closet
   
    //flag
    private bool colliding = false;

    //closet sprites
    private SpriteRenderer closetImage; //the default image of the closet
    private Sprite temp; //holder
   

    public void Start()
    {
     
      //  moveControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        closetImage = GetComponent<SpriteRenderer>();
        temp = closetImage.sprite; //store the sprite of the original image

    }

    /*
     * Future notes: why not use OnTriggerStay? Yeah, cause' it doesn't work (for whatever reason).
     * 
     * This code may seem a bit redudant but it's convoluted structure serves a purpose!
     * 
     */
    private void Update()
    {
       
        if (colliding && Input.GetKey(KeyCode.F))
        {
            playerSprite.enabled = false;
        
            closetImage.sprite = hiddenCloset; //change sprite to show hidden status     
         //   moveControl.DisableLRMovement(); //player should no longer be able to move left/right
            }
        }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            colliding = true;
        }
    }

     void OnTriggerExit2D(Collider2D collision)
    {
        playerSprite.enabled = true;
        closetImage.sprite = temp;
        colliding = false;
   //     moveControl.EnabledLRMovement(); //player can move left and right again
       
    }

    public bool GetNearbyStatus() { return colliding;  }


}
