using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Transform castPoint;
    public Transform meleePoint;
    public GameObject magicPrefab;
    public GameObject MeleePrefab;
 
    float magicForce = 10f;
    float attackDuration = 0.5f;
    float attackTimer = 0f;

    bool isAttacking = false;

    // Update is called once per frame
    void Update()
    {
        CheckAttackTimer();

        if (Input.GetButtonDown("Fire1"))
        {
            meleeAttack();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            if (GameController.Mana > 0)
            {
                GameController.Mana--;
                castMagic();
            }
            
        }
    }

    void meleeAttack()
    {
        if (!isAttacking)
        {
            MeleePrefab.SetActive(true);
            isAttacking = true;
        }        
    }


    void castMagic()
    {
        //Intantiating spawning in the gameobject calling it "magic" to refer to it in the next line.
        GameObject magic = Instantiate(magicPrefab, castPoint.position, castPoint.rotation);
        // We call the "magic", the Rigidbody and naming it "rb".
        Rigidbody2D rb = magic.GetComponent<Rigidbody2D>();
        //We use rb to access a function on the component adding a force in the "up direction".
        rb.AddForce(castPoint.up * magicForce, ForceMode2D.Impulse);
    }

    void CheckAttackTimer()
    {
        if (isAttacking)
        {
            attackTimer += Time.deltaTime;
            if(attackTimer >= attackDuration)
            {
                attackTimer = 0;
                isAttacking = false;
                MeleePrefab.SetActive(false);
            }
        }
    }
}
