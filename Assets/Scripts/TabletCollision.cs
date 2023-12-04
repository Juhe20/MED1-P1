using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletCollision : MonoBehaviour
{

    int collectedTablets;


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey("e"))
            {
                Debug.Log("test");
                collectedTablets++;
                GameController.Mana++;
                gameObject.SetActive(false);
            }
            
        }
    }
    

}
