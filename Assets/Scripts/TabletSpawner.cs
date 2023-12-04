using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class StoneTablet : MonoBehaviour
{
    public GameObject Tablet;
    public Vector2[] spawnPoints;
    int randomSpawnPoint;
    int randomSpawnPoint2;
    int i = 0;



    private void Start()
    {

        TabletSpawner();
        
    }

    void TabletSpawner()
    {
        while (i == 0)
        {
            randomSpawnPoint = Random.Range(0, spawnPoints.Length);
            randomSpawnPoint2 = Random.Range(0, spawnPoints.Length);
            if (randomSpawnPoint2 != randomSpawnPoint)
            {
                Instantiate(Tablet, spawnPoints[randomSpawnPoint], Quaternion.identity);
                Instantiate(Tablet, spawnPoints[randomSpawnPoint2], Quaternion.identity);
                i++;
            }
            else
            {
                randomSpawnPoint2 = Random.Range(0, spawnPoints.Length);
            }
        }
    }
}
