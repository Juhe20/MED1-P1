using UnityEngine;

public class Magic : MonoBehaviour
{
    [SerializeField] private AudioSource Bullet;
    public float speed = 1f;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Destroys bullet on collision and deals damage if the collision is of tag "Enemy"
        Bullet.Play();
        Destroy(gameObject);
        float damage = GameController.Damage;
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyController>().DamageEnemy(damage + 2);
        }
    }
}
