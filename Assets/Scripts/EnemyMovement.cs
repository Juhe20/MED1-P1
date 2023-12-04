using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float distanceBetween;
    public float AttackRange = 1f;
    public float Cooldown;
    private bool Hit = false;

    private float distance;
    private float canAttack;
    private Transform target;

    [SerializeField] private float attackSpeed = 1f;


    
    void FixedUpdate()
    {
        //Måler længden mellem enemy og player
        distance = Vector2.Distance(transform.position,player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        //Til at rotere enemy, Atan2 bruges til at finde vinkel mellem to punkter
        float angle = Mathf.Atan2(direction.y,direction.x)*Mathf.Rad2Deg;
        
        if (distance < distanceBetween)
        {
            //Får enemy til at bevæge sig
            transform.position =
                Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }

        if (Vector3.Distance(transform.position, player.transform.position) <= AttackRange)
        {
            Attack();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (attackSpeed >= canAttack)
            {
                GameController.DamagePlayer(1);
                Debug.Log(GameController.Health);
                canAttack = 0f;
                //add force to player
            }
            else
            {
                canAttack += Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            target = transform;
        }
    }

    void Attack() 
    {
        if (!Hit)
        {
            GameController.DamagePlayer(1);
            StartCoroutine(CoolDown());
        }
    }




    private IEnumerator CoolDown()
    {
        Hit = true;
        yield return new WaitForSeconds(Cooldown);
        Hit = false;
    }
}
