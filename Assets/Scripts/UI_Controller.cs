using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class UI_Controller : MonoBehaviour
{
    public GameObject Dialoguepanel;
    public GameObject boonDialogue;
    public GameObject Healthbar;
    float fillhealth;
    public GameObject Manabar;
    float fillmana;
    private int tabletSentenceIndex = 0;
    private int boonSentenceIndex = 0;
    public TextMeshProUGUI tabletText;

    [TextArea(3, 10)]
    public string[] tabletSentences;
    public TextMeshProUGUI boonText;

    [TextArea(3, 10)]
    public string[] boonSentences;

    // Update is called once per frame
    void Update()
    {
        if (TabletCollision.TabletCollide)
        {
            StartTabletDialogue();
            WaitSeconds();
            if (Input.GetKeyDown("e"))
            {
                TabletCollision.TabletCollide = false;
                GameController.MoveSpeed = 1;
            }
        }
        else
        {
            Dialoguepanel.SetActive(false);
        }


        if (Boon.BoonCollision)
        {
            StartBoonDialogue();
            WaitSeconds();
            if (Input.GetKeyDown("e"))
            {
                Boon.BoonCollision = false;
                GameController.MoveSpeed = 1;
            }
        }
        else
        {
            boonDialogue.SetActive(false);
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
        tabletSentenceIndex = (GameController.CollectedTablets) % tabletSentences.Length;
        tabletText.SetText(tabletSentences[tabletSentenceIndex]);
    }

    public void StartBoonDialogue()
    {
        boonDialogue.SetActive(true);
        boonSentenceIndex = (Boon.BoonsCollected) % boonSentences.Length;
        boonText.SetText(boonSentences[boonSentenceIndex]);
    }
    private IEnumerator WaitSeconds()
    { 
        yield return new WaitForSeconds(2f);

    }
}
