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

    [TextArea(3, 18)]
    public string[] tabletSentences;
    public TextMeshProUGUI boonText;

    [TextArea(3, 18)]
    public string[] boonSentences;

    [TextArea(3, 10)]
    public string[] characterNames;
    public TextMeshProUGUI nameText;

    //public GameObject Seth;
    //float Sethfill;

    // Update is called once per frame
    void Update()
    {



        if (TabletCollision.TabletCollide)
        {
            StartTabletDialogue();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TabletCollision.TabletCollide = false;
                GameController.MoveSpeed = 1;
                Time.timeScale = 1;
            }
        }
        else
        {
            Dialoguepanel.SetActive(false);
        }


        if (Boon.BoonCollision)
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

        
        if (NextLevel.NextScene == 1)
        {
            if (inDialogue == 5 || InDialogue == 2 && GameController.CurrentlyCollectedTablets == 0)
            {
                tabletsCollectedWindow.SetActive(true);
                tabletsCollectedText.SetText("I found the door to Seth's chamber." +
                    System.Environment.NewLine + "I have only collected" + " " + GameController.CurrentlyCollectedTablets + " " + "out of 2 of my father's stone tablets." +
                    System.Environment.NewLine + "I should consider finding the rest before heading into the final battle.");
            }

        }
        else if (NextLevel.NextScene == 0)
        {
            if (inDialogue == 1)
            {
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
        if (tabletsCollectedWindow.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                tabletsCollectedWindow.SetActive(false);
                inDialogue = 3;
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


    public void StartTabletDialogue()
    {
        Dialoguepanel.SetActive(true);
        nameText.SetText(characterNames[6]);
        tabletSentenceIndex = (GameController.CollectedTablets) % tabletSentences.Length;
        tabletText.SetText(tabletSentences[tabletSentenceIndex]);
    }

    public void StartBoonDialogue()
    {
        
        boonDialogue.SetActive(true);
        nameText.SetText(characterNames[GameController.GodDialogue]);
        imageContainer.sprite = (characterPortrait[GameController.GodDialogue]);
        boonText.SetText(boonSentences[GameController.GodDialogue]);
    }

}
