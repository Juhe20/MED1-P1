using UnityEngine;

public class Boon : MonoBehaviour
{
    private static bool boonCollision = false;
    public static bool BoonCollision { get => boonCollision; set => boonCollision = value; }

    //These values are all changed in Unity's inspector window.
    public int healthChange;
    public float speedChange;
    public int manaChange;
    public float damageChange;
    public int shieldSize;
    public bool revive;

    private void OnCollisionStay2D(Collision2D collision)
    {
        //Calls all the boon methods from the Game Controller scipt when the player clicks "e" on a boon.
        //Only uses the input values set in the Unity inspector window for that specific boon object. Time scale pauses the game.
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey("e"))
            {
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
