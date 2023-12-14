using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UI_Controller : MonoBehaviour
{
   
    public GameObject tabletsCollectedWindow;
    public TextMeshProUGUI tabletsCollectedText;
    private static int inDialogue = 0;
    public static int InDialogue { get => inDialogue; set => inDialogue = value; }
    public GameObject Dialoguepanel;
    public GameObject boonDialogue;
    public GameObject Healthbar;
    public Sprite[] characterPortrait;
    public Image imageContainer;
    float fillhealth;
    public GameObject Manabar;
    float fillmana;
    private int tabletSentenceIndex = 0;
    public TextMeshProUGUI tabletText;
    public GameObject Seth;

    [TextArea(3, 18)]
    public string[] tabletSentences;
    public TextMeshProUGUI boonText;

    [TextArea(3, 18)]
    public string[] boonSentences;

    [TextArea(3, 10)]
    public string[] characterNames;
    public TextMeshProUGUI nameText;

    void Update()
    {
        if (TabletCollision.TabletCollide) //Checks if you've collided with a tablet and starts the dialogue. Turns it off again when clicking space.
        {
            StartTabletDialogue();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TabletCollision.TabletCollide = false;
               
                Time.timeScale = 1;
            }
        }
        else
        {
            Dialoguepanel.SetActive(false);
        }


        if (Boon.BoonCollision) //Checks which boon you've collided with and starts the dialogue. Turns it off again when clicking space.
        {
            StartBoonDialogue();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Boon.BoonCollision = false;
                GameController.GodDialogue = 0;
                Time.timeScale = 1;
            }
        }
        else
        {
            boonDialogue.SetActive(false);
        }

        if (NextLevel.NextScene == 1) //Checks what floor you're on and a variable to check if you've collected all tablets yet.
        {
            if (inDialogue == 5 || InDialogue == 2 && GameController.CurrentlyCollectedTablets == 0)
            {
                Time.timeScale = 0;
                tabletsCollectedWindow.SetActive(true); //Sets window with text active. Shows the amount of tablets collected in the sentence.
                tabletsCollectedText.SetText("I found the door to Seth's chamber." +
                    System.Environment.NewLine + "I have only collected" + " " + GameController.CurrentlyCollectedTablets + " " + "out of 2 of my father's stone tablets." +
                    System.Environment.NewLine + "I should consider finding the rest before heading into the final battle.");
            }

        }
        else if (NextLevel.NextScene == 0)
        {
            if (inDialogue == 1)
            {
                Time.timeScale = 0;
                tabletsCollectedWindow.SetActive(true);
                tabletsCollectedText.SetText("I found the stairs to the next level." +
                    System.Environment.NewLine + "I have only collected" + " " + GameController.CollectedTablets + " " + "out of 2 of my father's stone tablets." +
                    System.Environment.NewLine + "I should consider finding the rest before going up the stairs.");

            }

        }
        else
        {
            tabletsCollectedWindow.SetActive(false);
        }
        if (tabletsCollectedWindow.activeInHierarchy) //Checks if the dialogue window is active after colliding with the stairs.
        {
            if (GameController.Health <= 0) //Sets window inactive if player dies.
            {
                tabletsCollectedWindow.SetActive(false);
                inDialogue = 0;
            }
            if (Input.GetKeyDown(KeyCode.Space)) //Sets window inactive if space is clicked.
            {
                Time.timeScale = 1;
                tabletsCollectedWindow.SetActive(false);
                inDialogue = 3;
            }
        }
        
        if(NextLevel.NextScene == 2)
        {
            if (!Seth.activeInHierarchy) //Checks if Seth is active or not and displays the victory dialogue if he's inactive.
            {
                Dialoguepanel.SetActive(true);
                nameText.SetText(characterNames[6]); 
                tabletText.SetText(tabletSentences[0]);
            }

        }




        //Health UI update
        fillhealth = (float)GameController.Health;
        fillhealth = fillhealth / GameController.MaxHealth;
        Healthbar.GetComponent<Image>().fillAmount = fillhealth;

        //Mana UI update
        fillmana = (float)GameController.Mana;
        fillmana = fillmana / GameController.MaxMana;
        Manabar.GetComponent<Image>().fillAmount = fillmana;


    }

    public void StartTabletDialogue() //Sets tablet dialogue window active and uses an array of character names and dialogue text to determine what the window shows.
    {
        Dialoguepanel.SetActive(true);
        nameText.SetText(characterNames[6]);
        tabletSentenceIndex = (GameController.CollectedTablets) % tabletSentences.Length;
        tabletText.SetText(tabletSentences[tabletSentenceIndex]);
    }

    public void StartBoonDialogue() //Sets boon dialogue window active and uses arrays of boon, character names and portrait to determine what the window shows.
    {
        
        boonDialogue.SetActive(true);
        nameText.SetText(characterNames[GameController.GodDialogue]);
        imageContainer.sprite = (characterPortrait[GameController.GodDialogue]);
        boonText.SetText(boonSentences[GameController.GodDialogue]);
    }

}
