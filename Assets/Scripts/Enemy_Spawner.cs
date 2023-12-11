using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy; 


    [SerializeField]
    private float spawnTimer = 3.5f;

    public GameObject player;
    float spawnRange;


    // Start is called before the first frame update
    void FixedUpdate()
    {
        spawnRange = Vector2.Distance(transform.position, player.transform.position);
        if (spawnRange < 10)
        {
            StartCoroutine(spawnEnemy(spawnTimer, enemy));
        }

    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-4f, 4), Random.Range(-3f, 3f), 0), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
        spawnTimer = Random.Range(5f, 12f);
    }
}