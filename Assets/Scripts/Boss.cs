using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public enum State
{
    idle,
    walk,
    attack
}

public class Boss : MonoBehaviour
{
    public GameObject player;
    public GameObject bulletPrefab;
    public GameObject bulletParent;

    public State currentState;

    public GameObject direction;
    public float bossSpeed = 1f;
    public float meleeRange = 1.3f;
    public float rangedAttackRange = 6f;
    public float combatRange;
    public float Cooldown = 2f;
    public bool invurnable = false;
    public bool talks = false;
    public bool talked = false;
    public bool moving = false;
    int shotCounter = 0;
    int damage = 3;
    //float fireRate = 1.5f;
    //float nextTimer = 2f;
    bool hit = false;
    public bool timer;

    private void Start()
    {
        currentState = State.idle;
        GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {


        if (timer == true)
        {
            bossSpeed = 0;
        }
        else if (timer == false)
        {
            bossSpeed = 1f;
        }

        combatRange = Vector2.Distance(transform.position, player.transform.position);
        if (combatRange <= 6)
        {
            talks = true;
            if (talks = true && talked == false)
            {
                //Start samtale mellem Horus & Seth
                talked = true;
                Debug.Log("Talks");
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, bossSpeed * Time.deltaTime);


        if (combatRange <= meleeRange)
        {
            MeleeAttack();
        }

        if (combatRange <= rangedAttackRange && shotCounter < 3 && combatRange > meleeRange)
        {
            RangedAttack();
        }
    }

    void MeleeAttack()
    {
        hit = true;
        if (hit == true && timer == false)
        {
            shotCounter = 0;
            damage = 2;
            StartCoroutine(CoolDown());
            GameController.DamagePlayer(damage);
            Debug.Log("Av");
        }
    }

    void RangedAttack()
    {
        Instantiate(bulletPrefab, bulletParent.transform.position, Quaternion.identity);
        StartCoroutine(CoolDown());
        shotCounter++;
    }

    private IEnumerator CoolDown()
    {
        timer = true;

        yield return new WaitForSeconds(Cooldown + 5);
        timer = false;
    }

}
