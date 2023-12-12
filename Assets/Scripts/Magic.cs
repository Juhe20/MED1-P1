using System.Collections;
using System.Collections.Generic;
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
        Bullet.Play();
        Destroy(gameObject);
        float danmage = GameController.Damage;
        Destroy(gameObject);

        if (collision.gameObject.CompareTag("Enemy"))
        {          
            collision.gameObject.GetComponent<EnemyController>().DamageEnemy(danmage+2);
        }
    }
}
