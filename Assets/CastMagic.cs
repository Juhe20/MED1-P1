using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastMagic : MonoBehaviour
{

    public Transform CastPoint;
    public GameObject MagicPrefab;
    public GameObject MeleeRange;

    private bool meleeAttack;

    public float magicForce = 15f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Melee();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Cast();
        }
    }

    void Melee()
    {
        
    }

    void Cast()
    {
        //Intantiating spawning in the gameobject calling it "magic" to refer to in the next line.
        GameObject magic = Instantiate(MagicPrefab, CastPoint.position, CastPoint.rotation);
        // We call the "magic", the Rigidbody and naming it "rb".
        Rigidbody2D rb = magic.GetComponent<Rigidbody2D>();
        //We use rb to access a function on the component adding a force in the "up direction".
        rb.AddForce(CastPoint.up * magicForce, ForceMode2D.Impulse);
    }
}
