using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MoreMountains.Tools;

public class SpreadGerms : MonoBehaviour
{
    
    private float spreadFactor = 5.0f; // spread fasters depending on environment - slow 5sec, fast 2sec
    private bool infected = false; //does the player have the ability to spread germs 
    private bool nearby = false; //determines if player is nearby objects that can carry germs
    private bool sinkNearby = false; //determines if player is near sink (superspreader)
  
    //sprite spawning stuff
    public Texture2D texture; //germs.png - set in Unity
    private SpriteRenderer spriteRenderer;
    private Sprite germSprite;

    //manages sound
    public AudioClip infectedSound;
    public MMSoundManager soundManager;

    //ui
    public Canvas infectedIcon;

    //flags
    bool callOnce = false; 
   

    void Start()
    {
        //start necessary coroutines
        StartCoroutine("ProximityCheck");
        infectedIcon.enabled = false; 
     
    }

    private void Update()
    {
        nearby = CheckProximityOfSpreadableItems();

        if (infected)
        {
            infectedIcon.enabled = true;

            if (callOnce)
            {
                soundManager.PlaySound(infectedSound, MMSoundManagerPlayOptions.Default);
                callOnce = false;
            }
        }
        else if (!infected)
        {
            infectedIcon.enabled = false;
            callOnce = true;
        }
    }

    
    //coroutine to reduce overhead + checks player proximity to spreadable items every half second
    //if the player is close enough to spread, it spreads germs based on what the player is standing near
    IEnumerator ProximityCheck()
    {
        
        for (; ; )
        {
            
            if (nearby && infected)
            {
                StartCoroutine("CreateNewGerms");
                
                yield return new WaitForSeconds(spreadFactor); //wait to spread 
            }
            else
            {
                StopCoroutine("CreateNewGerms"); //stop spreading if there's nothing nearby to spread to 
            }

            yield return new WaitForSeconds(0.5f);
        }
        
    }
    

    bool CheckProximityOfSpreadableItems()
    {
        GameObject[] spreadableItems = GameObject.FindGameObjectsWithTag("CarryGerms");
        sinkNearby = false;

        for (int i = 0; i < spreadableItems.Length; ++i)
        {
            //if player distance from items that can carry germs is less than two
            if (Vector3.Distance(transform.position, spreadableItems[i].transform.position) <= 1.5)
            {
                spreadFactor = 5f; //reset; germs spread more slowly
                return true;
            }

        }

        //super spreaders are any wet or otherwise heavily infected areas
        GameObject[] superSpreaders = GameObject.FindGameObjectsWithTag("SuperSpreader");

        for (int i = 0; i < superSpreaders.Length; ++i)
        {
            //if player distance from items that can carry germs is less than two
            if (Vector3.Distance(transform.position, superSpreaders[i].transform.position) <= 2)
            {
                spreadFactor = 2f; //germs spread more quickly 
                sinkNearby = true;
                return true;
            }

        }
        return false;
    }

    public bool GetSinkStatus() { return sinkNearby;  }
    
    public bool GetInfectedStatus()
    { 
        return infected; 
    }

    
    public void SetInfectedStatus(bool temp) { 
        
        infected = temp; 
    
    }



    IEnumerator CreateNewGerms()
    {
        germSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 40);
        GameObject newSprite = new GameObject();
        newSprite.AddComponent<SpriteRenderer>();
        //tag it so it can actually be cleaned up after 
        newSprite.tag = "Germs"; 
        spriteRenderer = newSprite.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = germSprite;
        //so that it is above the floor tiles
        spriteRenderer.sortingOrder = 1; 
        //right now it spawns where the player is, can be edited later
        spriteRenderer.transform.position = new Vector2(transform.position.x + Random.Range(0f, 1f), transform.position.y + Random.Range(0f, 1f)); 
        yield return new WaitForSeconds(spreadFactor);

    }
    

}
