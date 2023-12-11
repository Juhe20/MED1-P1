using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    [SerializeField] private AudioSource EnemyDamage;



    private void OnCollisionEnter2D(Collision2D collision)


    {
        

        float danmage = GameController.Damage;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyDamage.Play();
   
            collision.gameObject.GetComponent<EnemyController>().DamageEnemy(danmage);
        }
    }
}
