using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seth : MonoBehaviour
{
	private Animator animator;
	private Rigidbody2D myRigidbody;
	private Vector3 moveDirection;

	private float timeBetweenMoveCounter;
	private float timeToMoveCounter;
	public float timeBetweenMove;
	public float rangedAttackRange = 6f;
	public float meleeRange = 1.3f;
	public float bossSpeed = 1f;
	public float Cooldown = 2f;
	public float combatRange;
	public float timeToMove;
	public float moveSpeed;
	
	private bool moving;
	public bool timer = true;

	bool hit = false;

	public int shotCounter = 0;
	int damage = 3;

	public GameObject player;
	public GameObject bulletPrefab;
	public GameObject bulletParent;

	void Start()
	{
		animator = GetComponent<Animator>();
		myRigidbody = GetComponent<Rigidbody2D>();
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
		
		if (moving)
		{
			animator.SetBool("isAttacking", false);
			animator.SetBool("isWalking", true);

			timeToMoveCounter -= Time.deltaTime;
			myRigidbody.velocity = moveDirection;
			animator.SetFloat("horizontal", moveDirection.normalized.x);
			animator.SetFloat("vertical", moveDirection.normalized.y);

			transform.position = Vector2.MoveTowards(transform.position, player.transform.position, bossSpeed * Time.deltaTime);

			combatRange = Vector2.Distance(transform.position, player.transform.position);
			if (combatRange <= meleeRange)
			{
				animator.SetBool("isWalking", false);
				animator.SetBool("isAttacking", true);
				MeleeAttack();

			}

			if (combatRange <= rangedAttackRange && shotCounter < 3 && combatRange > 6)
			{
				animator.SetBool("isWalking", false);
				animator.SetBool("isAttacking", true);
				RangedAttack();
			}

			if (timeToMoveCounter < 0f)
			{
				moving = false;
				timeBetweenMoveCounter = timeBetweenMove;
			}
		}

		else
		{
			animator.SetBool("isWalking", false);

			timeBetweenMoveCounter -= Time.deltaTime;
			myRigidbody.velocity = Vector2.zero;

			if (timeBetweenMoveCounter < 0f)
			{
				moving = true;
				timeToMoveCounter = timeToMove;
			}
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

		yield return new WaitForSeconds(Cooldown + 3);
        animator.SetBool("isAttacking", false);
		timer = false;
	}
}
