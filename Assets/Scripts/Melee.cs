using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    [SerializeField] private AudioSource EnemyDamage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float damage = GameController.Damage;
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Deals damage if meleePoint is colliding with an enemy when set active from the player script.
            EnemyDamage.Play();
            collision.gameObject.GetComponent<EnemyController>().DamageEnemy(damage);
        }
    }
}
