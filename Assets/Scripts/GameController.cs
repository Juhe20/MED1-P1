using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public static bool Dead = false;

    private static int health = 5;
    private static int maxHealth = 10;
    private static int mana = 5;
    private static int maxMana = 10;
    private static int minMana = 0;
    private static float moveSpeed = 3.0f;
    private static float fireRate = 1.5f;
    private static bool shield = false;

    public static int Health { get => health; set => health = value; }

    public static int MaxHealth { get => maxHealth; set => maxHealth = value; }

    public static int Mana { get => mana; set => mana= value; }

    public static int MaxMana { get => maxMana; set => maxMana = value; }

    public static int MinMana { get => minMana; set => minMana = value; }

    public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    public static float FireRate { get => fireRate; set => fireRate = value; }

    public static bool Shield { get => shield; set => shield = value; } //virker måske


    public static void DamagePlayer(int damange)
    { 
        health -= damange;

        if (health <= 0)
        {
            KillPlayer();
        }
    }

    public static void HealPlayer(int heal)
    {
        health = Mathf.Min(maxHealth, health + heal);
    }

    public static void Manachange(int manaChange) 
    {
        mana = Mathf.Min(maxMana, mana + manaChange);
        mana = Mathf.Max(minMana, mana + manaChange); //Skulle gerne gøre at mana ikke kan at, mana < 0
    }

    public static void MoveSpeedChange(float speed)
    {
        moveSpeed += speed;
    }

    public static void FireRateChange(float rate)
    {
        fireRate -= rate;
    }

    public static void shieldSet() 
    { 
        shield = true;
    }

    public static void KillPlayer()
    {
         //Skriv en kode der sætter gang i death anim. + sætter spilleren tilbage til starten:-)
    }
}
