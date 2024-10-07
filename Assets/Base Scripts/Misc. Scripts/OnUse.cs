using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnUse : MonoBehaviour
{

    public string nextScene;
    public Vector2 playerPos;
    public VectorPosition storedLocation;

    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public float loadTime = 0.3f;

    private void Awake()
    {
        if (fadeInPanel != null) //if the panel has been set for this scene
        {
            //no rotation, placed at zero
            GameObject inGamePanel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject; //create our object in game 
            Destroy(inGamePanel, 1.5f); //destroy panel after 1.5 seconds
        }
    }

    private void OnTriggerEnter2D(Collider2D cObj)
    {

        if (cObj.CompareTag("Player")) //player can walk through doors and moves to the next scene
        {
            storedLocation.initialPos = playerPos;

            SceneManager.LoadScene(nextScene); //load the correct scene

            StartCoroutine("controlFade");
        }


    }

    public IEnumerator controlFade()

    {  
        if (fadeOutPanel)
        {
           // print("instantiating panel");
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity); //fade out panel
        }

        yield return new WaitForSeconds(loadTime);

        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(nextScene);

        while (!asyncOp.isDone)
        {
            yield return null; //done, load the scene
        }
    }

    
}
