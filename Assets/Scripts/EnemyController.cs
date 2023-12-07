using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float enemyhealth;
    public  float maxenemyhealth = 3;

    private void Start()
    {
        enemyhealth = maxenemyhealth;
    }


    public void DamageEnemy(float playerDamage)
    {
        enemyhealth -= playerDamage;

        if (enemyhealth <= 0)
        {
            gameObject.SetActive(false);
        }

    }
}
