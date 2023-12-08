using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        

        float danmage = GameController.Damage;

        if (collision.gameObject.CompareTag("Enemy"))
        {
   
            collision.gameObject.GetComponent<EnemyController>().DamageEnemy(danmage);
        }
    }
}
