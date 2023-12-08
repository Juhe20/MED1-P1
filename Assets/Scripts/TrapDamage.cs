using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    [SerializeField] private float damage;
    [Header("Spike Timer")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Animator anim;
    private SpriteRenderer spriteRend;

    private bool triggered; //when trap gets triggered
    private bool active; //when trap is active and can hurt player

    private void Awake()
    {
       anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!triggered)
            {
                StartCoroutine(ActivateTrap());
            }
            if (active)
                GameController.DamagePlayer(1);
        }
    }
    private IEnumerator ActivateTrap()
    {
        triggered = true;
        yield return new WaitForSeconds(activationDelay);//det tror jeg ik vi vil ha spikes skal hurtigt op i m�sen
        active = true;
        //anim.SetBool("activated", true); //hvis navnet er activated for t�ndt trap
        //wait seconds u choose in unity under script, deactivate trap and reset all variables and animators
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        //om spike trap hedder activated n�r den stikker dig eller et andet navn
        //anim.SetBool("activated", false);
    }
           }