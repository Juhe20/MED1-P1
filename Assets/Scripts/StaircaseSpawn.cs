using UnityEngine;

public class Staircase : MonoBehaviour
{
    public GameObject Stair;
    public Vector2[] spawnPoints;
    int randomSpawnPoint;


    //Runs the stair case spawner once per scene.
    private void Start()
    {
        StairCaseSpawner();
    }

    //Sets a random spawnpoint from the spawnpoint array. Array size and spawn positions from the array can be set in Unity's inspector window.
    void StairCaseSpawner()
    {
        randomSpawnPoint = Random.Range(0, spawnPoints.Length);
        Instantiate(Stair, spawnPoints[randomSpawnPoint], Quaternion.identity);
    }
}
