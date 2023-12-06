using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Boss : MonoBehaviour
{
    public GameObject player;
    public GameObject bulletPrefab;
    public GameObject bulletParent;
    
    public float BossHealth = 200;
    public float BossMax = 200;
    public float bossSpeed = 2.5f;
    public float meleeRange = 1.3f;
    public float rangedAttackRange = 5.3f;
    public float combatRange;
    public float Cooldown = 0.6f;
    public bool invurnable = false;
    public bool talks = false;
    public bool talked = false;
    int shotCounter = 0;
    int damage = 3;
    float fireRate = 1.5f;
    float nextTimer = 2f;
    bool hit = false;
    public bool timer = false;


    private void FixedUpdate()
    {
        if (timer == true)
        {
            bossSpeed = 0;

        }
        else if (timer == false) { bossSpeed = 2.5f;}

    combatRange = Vector2.Distance(transform.position, player.transform.position);
        if (combatRange <= 8)  //Works
        {
            talks = true;
            if (talks = true && talked == false)
            {
                //Start samtale mellem Horus & Seth
                talked = true;
                Debug.Log("Talks");
            }

            transform.position =
               Vector2.MoveTowards(transform.position, player.transform.position, bossSpeed * Time.deltaTime);

            

        }

        if (combatRange <= meleeRange)
        {
            meleeAttack();
        }


        if (combatRange <= rangedAttackRange && shotCounter < 3 && combatRange > meleeRange)
        {
            rangedAttack();
        }
    }

    void meleeAttack()
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

    private void rangedAttack()
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
