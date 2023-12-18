using System.Collections;
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
    [SerializeField] private AudioSource PlayerDamage;

    private void Start()
    {
        GetComponent<Animator>().GetFloat("MoveX");
        GetComponent<Animator>().GetFloat("MoveY");
    }

    //Method for setting animations of the enemy. Only the snakes have animations so does not affect mummies.
    private void SetAnimation(int xDirection, int yDirection)
    {
        GetComponent<Animator>().SetFloat("MoveX", xDirection);
        GetComponent<Animator>().SetFloat("MoveY", yDirection);
    }

    void FixedUpdate()
    {
        //Measures length between enemy and player.
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();


        if (distance < distanceBetween)
        {
            //Makes the enemy move if they are within a distanced determined in the Unity inspector.
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            if (this.transform.position.x - player.transform.position.x > 0.5)
            {
                SetAnimation(1, 0);
            }
            if (this.transform.position.x - player.transform.position.x < -0.5)
            {
                SetAnimation(-1, 0);
            }
            if (this.transform.position.y - player.transform.position.y > 0.5)
            {
                SetAnimation(0, 1);
            }
            if (this.transform.position.y - player.transform.position.y < -0.5)
            {
                SetAnimation(0, -1);
            }
        }

        //Uses attack method if within attack range of the player.
        if (Vector3.Distance(transform.position, player.transform.position) <= AttackRange)
        {
            Attack();
        }
    }

    //Enemy deals damage to the player
    void Attack()
    {
        if (!Hit)
        {
            PlayerDamage.Play();
            GameController.DamagePlayer(1);
            StartCoroutine(CoolDown());
        }
    }

    //Cooldown between attacks so the enemies won't kill the player instantly.
    private IEnumerator CoolDown()
    {
        Hit = true;
        yield return new WaitForSeconds(Cooldown);
        Hit = false;
    }
}
