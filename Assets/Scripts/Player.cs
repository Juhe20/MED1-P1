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

    Vector2 mousePosition;
    Vector2 moveDirection;

    [SerializeField] float dashSpeed = 20f;
    [SerializeField] float dashDuration = .1f;
    [SerializeField] float dashCooldown = 5f;

    bool isDashing;
    bool canDash;

    //Camera is referenced to track mouse position in void update
    public Camera cam;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        canDash = true;
    }

    
    void Update()
    {

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");


        moveDirection = new Vector2(horizontal, vertical).normalized;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetButtonDown("Dash") && canDash)
        {
            StartCoroutine(Dash());
        }

        

        speedLimit = GameController.MoveSpeed/moveSpeed;
        moveSpeed = GameController.MoveSpeed;

    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        if (horizontal !=0 && vertical !=0)
        {
            horizontal *= speedLimit;
            vertical *= speedLimit;
        }

        rb.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);

        Vector2 lookDir = mousePosition - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg -90f;
        rb.rotation = angle;
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        rb.velocity = new Vector2(moveDirection.x * dashSpeed, moveDirection.y * dashSpeed);
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
