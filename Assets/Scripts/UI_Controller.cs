using UnityEngine;
using UnityEngine.UI;


public class UI_Controller : MonoBehaviour
{

    public GameObject Healthbar;
    float fillhealth;

    public GameObject Manabar;
    float fillmana;

    // Update is called once per frame
    void Update()
    {

        //Health UI update
        fillhealth = (float)GameController.Health;
        fillhealth = fillhealth / GameController.MaxHealth;
        Healthbar.GetComponent<Image>().fillAmount = fillhealth;

        //Mana UI update
        fillmana = (float)GameController.Mana;
        fillmana = fillmana / GameController.MaxMana;
        Manabar.GetComponent<Image>().fillAmount = fillmana;

    }
}
