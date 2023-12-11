using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject body;
    public Transform castPoint;
    public Transform meleePoint;
    public GameObject magicPrefab;
    public GameObject MeleePrefab;
    public Camera cam;

    Vector2 mousePosition;
    float magicForce = 10f;
    float attackDuration = 0.5f;
    float attackTimer = 0f;

    bool isAttacking = false;
    [SerializeField] private AudioSource HitSoundEffect;

    private void Start()
    {
        body.GetComponent<Animator>().GetFloat("AttackX");
        body.GetComponent<Animator>().GetFloat("AttackY");
        body.GetComponent<Animator>().GetBool("Attacking");
    }

    // Update is called once per frame
    void Update()
    {

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CheckAttackTimer();

        if (Input.GetButtonDown("Fire1"))
        {
             
            meleeAttack();
           Debug.Log(Input.mousePosition.x- Screen.width/2);
           Debug.Log(Input.mousePosition.y - Screen.height / 2);
            
            body.GetComponent<Animator>().SetBool("Attacking", true);

            if (Input.mousePosition.x - Screen.width / 2 > 0 && Input.mousePosition.y - Screen.height/2 < 60 && Input.mousePosition.y - Screen.height / 2 > - 113)
            {
                Debug.Log("Test1");
                body.GetComponent<Animator>().SetFloat("AttackX", 1);
                body.GetComponent<Animator>().SetFloat("AttackY", 0);
            }
            if (Input.mousePosition.x - Screen.width / 2 < 0 && Input.mousePosition.y - Screen.height / 2 > -60 && Input.mousePosition.y - Screen.height / 2 < 113)
            {
                Debug.Log("Test2");
                body.GetComponent<Animator>().SetFloat("AttackX", -1);
                body.GetComponent<Animator>().SetFloat("AttackY", 0);
            }
            if (Input.mousePosition.y - Screen.height / 2 < 0 && Input.mousePosition.x - Screen.width/ 2 > -220 && Input.mousePosition.x - Screen.width / 2 < -200) 
            {
                Debug.Log("Test3");
                body.GetComponent<Animator>().SetFloat("AttackY", -1);
                body.GetComponent<Animator>().SetFloat("AttackX", 0);
            }
            if (Input.mousePosition.y - Screen.height / 2 > 0)
            {
                Debug.Log("Test4");
                body.GetComponent<Animator>().SetFloat("AttackY", 1);
                body.GetComponent<Animator>().SetFloat("AttackX", 0);
            }
     
        }

        if (Input.GetButtonDown("Fire2"))
        {
            if (GameController.Mana > 0)
            {
                GameController.Mana--;
                castMagic();
            }
            
        }
    }

    void meleeAttack()
    {
        if (!isAttacking)
        {
            HitSoundEffect.Play();

            MeleePrefab.SetActive(true);
            isAttacking = true;

        }        
    }


    void castMagic()
    {
        //Intantiating spawning in the gameobject calling it "magic" to refer to it in the next line.
        GameObject magic = Instantiate(magicPrefab, castPoint.position, castPoint.rotation);
        // We call the "magic", the Rigidbody and naming it "rb".
        Rigidbody2D rb = magic.GetComponent<Rigidbody2D>();
        //We use rb to access a function on the component adding a force in the "up direction".
        rb.AddForce(castPoint.up * magicForce, ForceMode2D.Impulse);
    }

    void CheckAttackTimer()
    {
        if (isAttacking)
        {
            body.GetComponent<Animator>().SetBool("Attacking", false);
            attackTimer += Time.deltaTime;
            if(attackTimer >= attackDuration)
            {
                attackTimer = 0;
                isAttacking = false;
                MeleePrefab.SetActive(false);
            }
        }
    }
}
