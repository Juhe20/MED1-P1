using System.Collections;
using UnityEngine;

public class Seth : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rigidBody;
    private Vector3 moveDirection;
    private float timeBetweenMoveCounter;
    private float timeToMoveCounter;
    public float timeBetweenMove;
    public float meleeRange = 1.3f;
    public float bossSpeed = 1f;
    public float Cooldown = 2f;
    public float combatRange;
    public float timeToMove;
    private bool moving;
    public bool timer = true;
    bool hit = false;
    int damage = 3;
    public GameObject player;
    [SerializeField] private AudioSource AttackSoundEffect;
    [SerializeField] private AudioSource Taunt;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        timeBetweenMoveCounter = timeBetweenMove;
        timeToMoveCounter = timeToMove;
    }

    void FixedUpdate()
    {
        if (timer == true)
        {
            animator.SetBool("isWalking", false);
            bossSpeed = 0;
        }
        else if (timer == false)
        {
            bossSpeed = 1f;
        }

        //Moves Seth towards the player if he is within range he stops walking and starts attacking instead.
        if (moving)
        {
            Taunt.Play();
            animator.SetBool("isAttacking", false);
            animator.SetBool("isWalking", true);
            timeToMoveCounter -= Time.deltaTime;
            rigidBody.velocity = moveDirection;
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, bossSpeed * Time.deltaTime);
            combatRange = Vector2.Distance(transform.position, player.transform.position);
            if (combatRange <= meleeRange)
            {
                animator.SetBool("isWalking", false);
                animator.SetBool("isAttacking", true);
                MeleeAttack();
            }
            if (timeToMoveCounter < 0f)
            {
                moving = false;
                timeBetweenMoveCounter = timeBetweenMove;
            }
        }
        else
        {
            //Checks if the player is within range and starts walking if the walking cooldown timer has passed.
            combatRange = Vector2.Distance(transform.position, player.transform.position);
            animator.SetBool("isWalking", false);
            timeBetweenMoveCounter -= Time.deltaTime;
            rigidBody.velocity = Vector2.zero;

            if (timeBetweenMoveCounter < 0f && combatRange < 14)
            {
                moving = true;
                timeToMoveCounter = timeToMove;
            }
        }
    }

    //Deals damage to the player and starts a cooldown until he can attack again.
    void MeleeAttack()
    {
        hit = true;
        if (hit == true && timer == false)
        {
            damage = 2;
            StartCoroutine(CoolDown());
            GameController.DamagePlayer(damage);
            AttackSoundEffect.Play();
        }
    }

    //Cooldown for how often he can attack.
    private IEnumerator CoolDown()
    {
        timer = true;
        yield return new WaitForSeconds(Cooldown + 3);
        animator.SetBool("isAttacking", false);
        timer = false;
    }
}
