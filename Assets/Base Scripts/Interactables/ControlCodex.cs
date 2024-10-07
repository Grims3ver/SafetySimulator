using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlCodex : MonoBehaviour
{
    public Canvas codexInterface;
    //so we have access to the right page as well
    public GameObject rightPage;
    public ControlSubtitles subtitles;
    //create an array of pages, each with title and body
    private Page[] pages;
    private static int pageNumb;

    //textboxes present in window
    private TextMeshProUGUI[] textBoxes;

    //Audio
    private AudioSource pageFlipSound;

    //images 

    public Image imageLeft;
    public Image imageRight;

    //sprites for images

    public Sprite enemySprite; //showcase enemy
    public Sprite npcSprite; //showcase friendly characters
    void Start()
    {
        //3 pages for now
        pages = new Page[4];
        pageNumb = 0;

        // ***The entire purpose of this code is just to get all textboxes of the book into one array, for easy editing***
        TextMeshProUGUI[] tempLeftBox = GetComponentsInChildren<TextMeshProUGUI>();
        TextMeshProUGUI[] tempRightBox = rightPage.GetComponentsInChildren<TextMeshProUGUI>();

        int i = 0;
        textBoxes = new TextMeshProUGUI[tempLeftBox.Length + tempRightBox.Length];
        //get left page textboxes into new combined array
        for (i = 0; i < tempLeftBox.Length; ++i)
        {
            textBoxes[i] = tempLeftBox[i];
        }

        //continue where the previous array left off, and get the textboxes from the right page
        for (int j = 0; j < tempRightBox.Length; ++j)
        {
            textBoxes[i] = tempRightBox[j];
            ++i;
        }

        /*textBoxes[0] = title left page
         * textBoxes[1] = title body
         * textBoxes[2] = left page button text ("Game Information")
         * textBoxes[3] = left page button text ("Code Silver")
         * textBoxes[4] = right page title text 
           textBoxes[5] = right page body text */
        //****
        pageFlipSound = GetComponent<AudioSource>();
        SetPages();
        codexInterface.enabled = false;
        UpdateText(); //begin on the first page
    }

    void SetPages()
    {
        pages[0] = new Page("Code Silver", "Running is important - you may need to leave patients behind, but your safety is key. We cannot help our patients if we get injured.");
        pages[1] = new Page("Code Silver", "Hiding is helpful when you cannot run. Remember to do everything possible to minimize your presence - block windows, silence devices, and hide against the door-bound wall.");
        pages[2] = new Page("Code Silver", "Fight only as a last resort, if you cannot run or hide. If you must fight, attempt to strike quickly and flee.");
        pages[3] = new Page("Game Information", "Enemies will wear pink and carry weapons. Friendly characters will wear teal (light blue-green), and be unarmed.");

    }

    public void FlipPageForward()
    {

        if (pageNumb >= pages.Length - 1)
        {
            pageNumb = 0;
        }
        else
        {
            ++pageNumb;
        }

        UpdateText();
    }

    public void FlipPageBackward()
    {

        if (pageNumb <= 0)
        {
            pageNumb = pages.Length - 1; //return to the "end"
        }
        else
        {
            --pageNumb;
        }

        UpdateText();
    }

    void UpdateText()
    {
        UpdateImages();
        //don't play the flip sound if the codex isn't open (prevents flip sound from playing when book opens/closes)
        if (codexInterface.enabled)
        {
            pageFlipSound.Play();

            if (SubtitleState.subtitlesOn)
            {
                string pageFlipDescription = "[page flipping sound]";
                subtitles.SetSubtitleText(pageFlipDescription);
            }
        }
        //currently edits right page only
        textBoxes[4].text = pages[pageNumb].title;
        textBoxes[5].text = pages[pageNumb].body;
    }

    void UpdateImages()
    {
        //if we want to show the images
        if (pageNumb == 3)
        {
            //image alphas are previously set to 0 - they do not need to be shown on other pages
            //so we are setting the image alpha to 1 on this page, to display images
            var tempColour = imageLeft.color;
            tempColour.a = 1f;
            imageLeft.color = tempColour;
            //set sprite image
            imageLeft.sprite = enemySprite;
            imageRight.color = tempColour;
            //set sprite
            imageRight.sprite = npcSprite;
        }
        else //otherwise that page has no images
        {
            //invisible
            var tempColour = imageLeft.color;
            tempColour.a = 0f;
            imageLeft.color = tempColour;
            imageRight.color = tempColour;
            //set sprite image
        }
    }

    public void ClickExitCodexButton()
    {
        codexInterface.enabled = false; 
    }

    public void ClickCodeSilver()
    {
        //Code Silver data currently starts on first page
        pageNumb = 0;
        UpdateText();
    }

    public void ClickGameInformation()
    {
        //game info information begins on page 3
        pageNumb = 3;
        UpdateText();

    }
    public class Page
    {
        public string title;
        public string body;

        Page()
        {
            title = "Uh-oh, this shouldn't have happened!";
            body = "Better get someone to fix this :(";
        }

        public Page(string t, string b)
        {
            title = t;
            body = b;
        }
    }
}
