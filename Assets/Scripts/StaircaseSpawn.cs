using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Staircase : MonoBehaviour
{
    int nextScene;
    public GameObject Stair;
    public Vector2[] spawnPoints;
    int randomSpawnPoint;
    int i = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            nextScene++;
            SceneManager.LoadScene(nextScene);
        }

    }

    private void Update()
    {

        StairCaseSpawner();
        
    }

    void StairCaseSpawner()
    {
        randomSpawnPoint = Random.Range(0, spawnPoints.Length);
        while (i < 1)
        {
            Instantiate(Stair, spawnPoints[randomSpawnPoint], Quaternion.identity);
            i++;
        }
    }
}
