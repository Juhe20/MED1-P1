using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public static bool Dead = false;
    private static int health = 5;
    private static int maxHealth = 10;
    private static int mana = 5;
    private static int maxMana = 10;
    private static int minMana = 0;
    private static float moveSpeed = 1.0f;
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

    public static int Mana { get => mana; set => mana= value; }

    public static int MaxMana { get => maxMana; set => maxMana = value; }

    public static int MinMana { get => minMana; set => minMana = value; }

    public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    public static float Damage { get => damage; set => damage= value; }

    public static int Shield { get => shield; set => shield = value; }

    public static bool Revive { get => revive; set => revive = value; } 


    public static void DamagePlayer(int TakeDamage)
    { 
        health -= TakeDamage - shield; //decrease amount of health based on value of parameter

        if (health <= 0)
        {
            if (GameController.revive == true)
            {
                Debug.Log(revive);
                GameController.HealPlayer(4);
                GameController.revive = false;
            }
            else 
            {
                KillPlayer();
            }
            
        }
    }



    public static void HealPlayer(int heal)
    {
        health = Mathf.Min(maxHealth, health + heal);
    }

    public static void Manachange(int manaChange) 
    {
        mana = Mathf.Min(maxMana, mana + manaChange);
        mana = Mathf.Max(minMana, mana + manaChange); //gør at mana < 0
    }

    public static void MoveSpeedChange(float speed)
    {
        moveSpeed += speed;
    }

    public static void DamageChange(float damageChange)
    {
        damage += damageChange;
    }



    public static void shieldSet(int shieldSize) 
    {
        shield += shieldSize;
    }

    public static void reviveSet(bool ReviveChange)
    {
        revive = ReviveChange;
    }

    public static void KillPlayer()
    {
        SceneManager.LoadScene(0);
        health = 5;
        mana = 5;
        moveSpeed = 1.0f;
        damage = 1.0f;
    }


}
