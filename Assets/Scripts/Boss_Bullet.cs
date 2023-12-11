using UnityEngine;

public class Boss_Bullet : MonoBehaviour
{
    GameObject target;
    public float speed = 1.5f;
    int damage = 1;
    Rigidbody2D bulletrb;

    // Update is called once per frame
    void Start()
    {

        bulletrb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        bulletrb.velocity = new Vector2(moveDir.x, moveDir.y);
        Destroy(this.gameObject, 4);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameController.DamagePlayer(damage);
        }

    }
}
