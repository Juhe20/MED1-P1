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
    public float AttackRange = 1.1f;
    public float Cooldown;
    private bool Hit = false;

    public int enemyDamage = 2;

    private float distance;
    private float canAttack;
    private Transform target;

    [SerializeField] private float attackSpeed = 1f;

    [SerializeField] private AudioSource PlayerDamage;
    private void Start()
    {
        GetComponent<Animator>().GetFloat("MoveX");
        GetComponent<Animator>().GetFloat("MoveY");
        GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        //Måler længden mellem enemy og player
        distance = Vector2.Distance(transform.position,player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        //Til at rotere enemy, Atan2 bruges til at finde vinkel mellem to punkter
        
        

        if (distance < distanceBetween)
        {
            //Får enemy til at bevæge sig
            transform.position =
                Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);

            if (this.transform.position.x - player.transform.position.x > 0.5)
            {
                GetComponent<Animator>().SetFloat("MoveX", 1);
                GetComponent<Animator>().SetFloat("MoveY", 0);
            }


            if (this.transform.position.x - player.transform.position.x < -0.5)
            {
                GetComponent<Animator>().SetFloat("MoveX", -1);
                GetComponent<Animator>().SetFloat("MoveY", 0);
            }


            if (this.transform.position.y - player.transform.position.y > 0.5)
            {
                GetComponent<Animator>().SetFloat("MoveX", 0);
                GetComponent<Animator>().SetFloat("MoveY", 1);
            }

            if (this.transform.position.y - player.transform.position.y < -0.5)
            {
                GetComponent<Animator>().SetFloat("MoveX", 0);
                GetComponent<Animator>().SetFloat("MoveY", -1);
            }



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
            PlayerDamage.Play();

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
