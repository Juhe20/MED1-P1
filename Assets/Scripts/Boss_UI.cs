using UnityEngine;
using UnityEngine.UI;

public class Boss_UI : MonoBehaviour
{
    public GameObject Healthbar;
    float fillhealth;

    void FixedUpdate()
    {

        //Health UI update
        //fillhealth = Boss.BossHealth;
        //fillhealth = fillhealth / Boss.BossMax;
        Healthbar.GetComponent<Image>().fillAmount = fillhealth;
    }
    
}
