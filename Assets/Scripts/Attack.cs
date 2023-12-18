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

        //Uses left mouse button check the mouse position and trigger an attack and corresponding animations.
        if (Input.GetButtonDown("Fire1"))
        {
            meleeAttack();
            body.GetComponent<Animator>().SetBool("Attacking", true);

            if (Input.mousePosition.x - Screen.width / 2 > 0 && Input.mousePosition.y - Screen.height / 2 < 60 && Input.mousePosition.y - Screen.height / 2 > -113)
            {
                body.GetComponent<Animator>().SetFloat("AttackX", 1);
                body.GetComponent<Animator>().SetFloat("AttackY", 0);
            }
            if (Input.mousePosition.x - Screen.width / 2 < 0 && Input.mousePosition.y - Screen.height / 2 > -60 && Input.mousePosition.y - Screen.height / 2 < 113)
            {
                body.GetComponent<Animator>().SetFloat("AttackX", -1);
                body.GetComponent<Animator>().SetFloat("AttackY", 0);
            }
            if (Input.mousePosition.y - Screen.height / 2 < 0 && Input.mousePosition.x - Screen.width / 2 > -220 && Input.mousePosition.x - Screen.width / 2 < -200)
            {
                body.GetComponent<Animator>().SetFloat("AttackY", -1);
                body.GetComponent<Animator>().SetFloat("AttackX", 0);
            }
            if (Input.mousePosition.y - Screen.height / 2 > 0)
            {
                body.GetComponent<Animator>().SetFloat("AttackY", 1);
                body.GetComponent<Animator>().SetFloat("AttackX", 0);
            }
        }

        //Uses right mouse button to use mana and call the magic method, which instantiates the magic bullet.
        if (Input.GetButtonDown("Fire2"))
        {
            if (GameController.Mana > 0)
            {
                GameController.Mana--;
                castMagic();
            }
        }
    }

    //Sets melee prefab active which is what measures if the player is in range of an enemy.
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
            if (attackTimer >= attackDuration)
            {
                attackTimer = 0;
                isAttacking = false;
                MeleePrefab.SetActive(false);
            }
        }
    }
}
