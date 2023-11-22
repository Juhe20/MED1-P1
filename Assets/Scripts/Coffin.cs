using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffin : MonoBehaviour
{

    [SerializeField]
    GameObject mummy;
    [SerializeField]
    GameObject coin;
    private int chance;
    bool open = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        chance = Random.Range(1, 101);

        if (open == false && chance > 50)
        {
            GameObject newEnemy = Instantiate(mummy, gameObject.transform.position, Quaternion.identity);
            open = true;

        }
        if (open == false && chance < 50)
        {
            GameObject newEnemy = Instantiate(coin, gameObject.transform.position, Quaternion.identity);
            open = true;

        }
    }


}
