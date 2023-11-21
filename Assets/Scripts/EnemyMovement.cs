using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float distanceBetween;

    private float distance;
    void Start()
    {
        
    }

    
    void Update()
    {
        //Måler længden mellem enemy og player
        distance = Vector2.Distance(transform.position,player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        //Til at rotere enemy, Atan2 bruges til at finde vinkel mellem to punkter
        float angle = Mathf.Atan2(direction.y,direction.x)*Mathf.Rad2Deg;
        
        if (distance < distanceBetween)
        {
            //Får enemy til at bevæge sig
            transform.position =
                Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
    }
}
