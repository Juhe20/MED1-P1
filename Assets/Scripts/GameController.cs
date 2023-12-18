using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static int health = 5;
    private static int maxHealth = 10;
    private static int mana = 5;
    private static int maxMana = 10;
    private static int minMana = 0;
    private static float moveSpeed = 1.5f;
    private static float damage = 1.0f;
    private static int shield = 0;
    private static bool revive = false;
    private static int collectedTablets = 0;
    private static int currentlyCollectedTablets;
    private static int godDialogue;
    public static int GodDialogue { get => godDialogue; set => godDialogue = value; }
    public static int CurrentlyCollectedTablets { get => currentlyCollectedTablets; set => currentlyCollectedTablets = value; }
    public static int CollectedTablets { get => collectedTablets; set => collectedTablets = value; }
    public static int Health { get => health; set => health = value; }
    public static int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public static int Mana { get => mana; set => mana = value; }
    public static int MaxMana { get => maxMana; set => maxMana = value; }
    public static int MinMana { get => minMana; set => minMana = value; }
    public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public static float Damage { get => damage; set => damage = value; }
    public static int Shield { get => shield; set => shield = value; }
    public static bool Revive { get => revive; set => revive = value; }


    public static void DamagePlayer(int TakeDamage)
    {
        //decrease amount of health based on value of parameter
        health -= TakeDamage - shield;
        if (health <= 0)
        {
            //Heals the player if they have a revive and takes the revive boon away.
            if (GameController.revive == true)
            {
                GameController.HealPlayer(4);
                GameController.revive = false;
            }
            //If the player doesn't have a revive boon they get send back to spawn.
            else
            {
                KillPlayer();
            }
        }
    }

    //Puts the health up by the amount of health the boon is set to give.
    public static void HealPlayer(int heal)
    {
        health = Mathf.Min(maxHealth, health + heal);
    }

    public static void Manachange(int manaChange)
    {
        mana = Mathf.Min(maxMana, mana + manaChange);
        mana = Mathf.Max(minMana, mana + manaChange); //gør at mana < 0
    }

    //Sets the movement speed to what speed of the boon is set to.
    public static void MoveSpeedChange(float speed)
    {
        moveSpeed += speed;
    }

    //Ups the player's attack damage by the amount the boon gives.
    public static void DamageChange(float damageChange)
    {
        damage += damageChange;
    }

    //Makes the player take less damage by the amount the boon gives. 
    public static void shieldSet(int shieldSize)
    {
        shield += shieldSize;
    }

    //Sets a bool to true if the player receives a revive from a boon.
    public static void reviveSet(bool ReviveChange)
    {
        revive = ReviveChange;
    }

    //Resets all values back to what they started as when the player dies, and sends them back to the starting scene.
    public static void KillPlayer()
    {
        SceneManager.LoadScene(0);
        health = 5;
        mana = 5;
        moveSpeed = 1.5f;
        damage = 1.0f;
        currentlyCollectedTablets = 0;
        collectedTablets = 0;
        NextLevel.NextScene = 0;
        UI_Controller.InDialogue = 0;
    }
}
