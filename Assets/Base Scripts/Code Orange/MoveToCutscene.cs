using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveToCutscene : MonoBehaviour
{
    public Transform player;
    public CodeOrange checkIfOrangeAnnounced;
    public GameObject NPC;

    public Canvas blackScreen;
    public Image blackImage;

    [HideInInspector]
    public bool moved = false;

    private Color opacity; 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && checkIfOrangeAnnounced.codeOrangeAnnounced) //player can walk through doors and moves to the end level area
        {
            StartCoroutine("FadeToBlack");

        }
        else if (collision.gameObject.tag.Equals("NPC"))
        {
            //the NPC 'leaves' 
            NPC.SetActive(false);
        }
    }

    public IEnumerator FadeToBlack()
    {
        blackScreen.enabled = true; 
        opacity = blackImage.color;
        float fade;

        while (opacity.a < 1)
        {
            fade = opacity.a + (1.01f * Time.deltaTime);
            opacity = new Color(opacity.r, opacity.g, opacity.b, fade);
            blackImage.color = opacity;
            yield return null;
        }

        if(opacity.a >= 1 && !moved)
        {
            player.position = new Vector2(-63, 50); //move the player
            StartCoroutine("FadeToClear");
            StopCoroutine("FadeToBlack");
        }
    }

    private IEnumerator FadeToClear()
    {
        opacity = blackImage.color;
        float fade;

        while (opacity.a > 0)
        {
            fade = opacity.a - (1.01f * Time.deltaTime);
            opacity = new Color(opacity.r, opacity.g, opacity.b, fade);
            blackImage.color = opacity;
            yield return null;
        }

        if (opacity.a <= 0)
        {
            moved = true;
            blackScreen.enabled = false;
        }
    }

}
