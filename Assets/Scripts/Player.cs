using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb; 
    float vertical;
    float horizontal;

    public float speedLimit = GameController.MoveSpeed*0.5f;
    public float moveSpeed;

    Vector2 mousePos;
    Vector2 mouseDis;

    //Camera is referenced to track mouse position in void update
    public Camera cam;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        speedLimit = GameController.MoveSpeed/moveSpeed;
        moveSpeed = GameController.MoveSpeed;
    }

    private void FixedUpdate()
    {
        if (horizontal !=0 && vertical !=0)
        {
            horizontal *= speedLimit;
            vertical *= speedLimit;
        }

        rb.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg -90f;
        rb.rotation = angle;
    }
}
