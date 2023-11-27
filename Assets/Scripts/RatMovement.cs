using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public Vector3 direction = Vector3.zero; // (0,0,0)
    bool running = false;
    //Vector3 destination;



    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()

    {
        if(!running)
        {
            StartCoroutine(changeDirection()); // Skifter retning
        }

        //destination = transform.position + direction;
        transform.position += direction * speed;
        //transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime);



    }

    IEnumerator changeDirection()
    {
        running = true;
        yield return new WaitForSeconds(2);
        direction.x = Random.Range(-1f, 1f);
        direction.y = Random.Range(-1f, 1f);
        direction.Normalize();
        running = false;
    }

}
