using UnityEngine;

public class StoneTablet : MonoBehaviour
{
    public GameObject Tablet;
    public Vector2[] spawnPoints;
    int randomSpawnPoint;
    int randomSpawnPoint2;
    int i = 0;

    //Only spawns tablets once per scene.
    private void Start()
    {
        TabletSpawner();
    }

    void TabletSpawner()
    {
        //Keeps running a while loop until 2 different random spawnpoints are found. Increments i to stop the loop whenever they are different.
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
