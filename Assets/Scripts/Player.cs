using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject body;
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
    public Transform child;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canDash = true;
        body.GetComponent<Animator>().GetFloat("MoveX");
        body.GetComponent<Animator>().GetFloat("MoveY");
        body.GetComponent<Animator>().GetBool("NotMoveing");
        body.GetComponent<Animator>().SetBool("NotMoveing", false);

        body.GetComponent<Animator>().SetBool("Attacking", false);


    }

    
    void Update()
    {
        child.transform.rotation = Quaternion.Euler(0.0f, 0.0f, gameObject.transform.rotation.z * -1.0f);
       
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

        if(horizontal > 0)
        {
            body.GetComponent<Animator>().SetFloat("MoveX", 1);
            body.GetComponent<Animator>().SetFloat("MoveY", 0);
            body.GetComponent<Animator>().SetBool("NotMoveing", false);
        }

        if (horizontal < 0)
        {
            body.GetComponent<Animator>().SetFloat("MoveX", -1);
            body.GetComponent<Animator>().SetFloat("MoveY", 0);
            body.GetComponent<Animator>().SetBool("NotMoveing", false);
        }

        if (vertical > 0)
        {
            body.GetComponent<Animator>().SetFloat("MoveY", 1);
            body.GetComponent<Animator>().SetFloat("MoveX", 0);
            body.GetComponent<Animator>().SetBool("NotMoveing", false);
        }

        if (vertical < 0)
        {
            body.GetComponent<Animator>().SetFloat("MoveY", -1);
            body.GetComponent<Animator>().SetFloat("MoveX", 0);
            body.GetComponent<Animator>().SetBool("NotMoveing", false);
        }
        if (horizontal == 0 && vertical == 0)
        {
            body.GetComponent<Animator>().SetFloat("MoveY", 0);
            body.GetComponent<Animator>().SetFloat("MoveX", 0);
            body.GetComponent<Animator>().SetBool("NotMoveing", true);
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        Debug.Log(GameController.GodDialogue);

        if (collision.gameObject.CompareTag("tefnut"))
        {
            GameController.GodDialogue = 1;
        }
        else if (collision.gameObject.CompareTag("seth"))
        {
            GameController.GodDialogue = 2;
        }
        else if (collision.gameObject.CompareTag("shu"))
        {
            GameController.GodDialogue = 3;
        }
        else if (collision.gameObject.CompareTag("nut"))
        {
            GameController.GodDialogue = 4;
        }
        else if (collision.gameObject.CompareTag("geb"))
        {
            GameController.GodDialogue = 5;
        }
        else if (collision.gameObject.CompareTag("osiris"))
        {
            GameController.GodDialogue = 6;
        }

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
