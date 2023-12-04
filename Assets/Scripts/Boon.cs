using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boon : MonoBehaviour
{
    public string Name;
    public string Description;
    public Sprite Image;

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
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player") 
        {
            GameController.HealPlayer(healthChange);
            GameController.Manachange(manaChange);
            GameController.MoveSpeedChange(speedChange);
            GameController.DamageChange(damageChange);
            GameController.shieldSet(shieldSize);
            GameController.reviveSet(revive);
            Debug.Log(revive);
            Destroy(gameObject);
        }

    }

}
