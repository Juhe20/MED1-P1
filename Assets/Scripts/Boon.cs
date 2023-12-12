using UnityEngine;

public class Boon : MonoBehaviour
{
    [SerializeField] private AudioSource Speaking;
    public string Name;
    public string Description;
    public Sprite Image;
    private static bool boonCollision = false;
    private static int boonsCollected = 0;
    public static int BoonsCollected { get => boonsCollected; set => boonsCollected = value; }
    public static bool BoonCollision { get => boonCollision; set => boonCollision = value; }
    

    public int healthChange;
    public float speedChange;
    public int manaChange;
    public float damageChange;
    public int shieldSize;
    public bool revive;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = Image;
    }

    // Update is called once per frame
    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey("e"))
            {
                Speaking.Play();
                boonsCollected++;
                BoonCollision = true;
                GameController.HealPlayer(healthChange);
                GameController.Manachange(manaChange);
                GameController.MoveSpeedChange(speedChange);
                GameController.DamageChange(damageChange);
                GameController.shieldSet(shieldSize);
                GameController.reviveSet(revive);
                gameObject.SetActive(false);
                Time.timeScale = 0;
                

            }
        }
    }

}
