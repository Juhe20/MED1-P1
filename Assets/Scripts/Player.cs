using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject body;
    private Rigidbody2D rb;
    float vertical;
    float horizontal;
    public float speedLimit = GameController.MoveSpeed * 0.5f;
    public float moveSpeed;
    Vector2 mousePosition;
    Vector2 moveDirection;
    [SerializeField] float dashSpeed = 20f;
    [SerializeField] float dashDuration = .1f;
    [SerializeField] float dashCooldown = 5f;
    [SerializeField] private AudioSource DashSoundEffect;
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
        body.GetComponent<Animator>().GetBool("NotMoving");
        body.GetComponent<Animator>().SetBool("NotMoving", false);
        body.GetComponent<Animator>().SetBool("Attacking", false);
    }

    void Update()
    {
        //Makes the player move. Uses a body as a child so the sprite doesn't rotate with the mouse.
        child.transform.rotation = Quaternion.Euler(0.0f, 0.0f, gameObject.transform.rotation.z * -1.0f);
        Debug.Log(UI_Controller.InDialogue);
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(horizontal, vertical).normalized;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetButtonDown("Dash") && canDash)
        {
            DashSoundEffect.Play();
            StartCoroutine(Dash());
        }
        speedLimit = GameController.MoveSpeed / moveSpeed;
        moveSpeed = GameController.MoveSpeed;
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        if (horizontal != 0 && vertical != 0)
        {
            horizontal *= speedLimit;
            vertical *= speedLimit;
        }

        rb.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);
        Vector2 lookDir = mousePosition - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

        //Sets the animation depending on which way the player is walking. Uses x and y positions as input parameters.
        void SetAnimation(int yDirection, int xDirection, bool moving)
        {
            body.GetComponent<Animator>().SetFloat("MoveY", yDirection);
            body.GetComponent<Animator>().SetFloat("MoveX", xDirection);
            body.GetComponent<Animator>().SetBool("NotMoving", moving);
        }

        if (horizontal > 0)
        {
            SetAnimation(0, 1, false);
        }
        if (horizontal < 0)
        {
            SetAnimation(0, -1, false);
        }
        if (vertical > 0)
        {
            SetAnimation(1, 0, false);
        }
        if (vertical < 0)
        {
            SetAnimation(-1, 0, false);
        }
        if (horizontal == 0 && vertical == 0)
        {
            SetAnimation(0, 0, true);
        }
    }

    //Checks collision with boon tags for dialogue variables.
    private void OnCollisionStay2D(Collision2D collision) 
    {
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

    //Checks stair collision for dialogue, increments the value which will be used in UI_Controller
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("stair"))
        {
            if (GameController.CurrentlyCollectedTablets < 2)
            {
                UI_Controller.InDialogue++;
            }
        }
    }

    //Updates players velocity with a new vector to push it in a direction. Puts it on cooldown afterwards.
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
