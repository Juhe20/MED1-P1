using UnityEngine;
using UnityEngine.SceneManagement;

public class Staircase : MonoBehaviour
{
    int nextScene;
    public GameObject Stair;
    public Vector2[] spawnPoints;
    int randomSpawnPoint;
    private static GameObject spawnedStairPosition;
    public static GameObject SpawnedStairPosition { get => spawnedStairPosition; set => spawnedStairPosition = value; }
    int i = 0;


    private void Update()
    {

        StairCaseSpawner();

    }

    void StairCaseSpawner()
    {
        randomSpawnPoint = Random.Range(0, spawnPoints.Length);
        while (i < 1)
        {
            spawnedStairPosition = Instantiate(Stair, spawnPoints[randomSpawnPoint], Quaternion.identity);
            i++;
        }
    }
}
