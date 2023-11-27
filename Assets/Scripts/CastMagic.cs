using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastMagic : MonoBehaviour
{
    public Transform castPoint;
    public GameObject magicPrefab;

    [SerializeField]
    public float castDelay = 2f;
    private float lastCastTime;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("mouse 1"))            
        {
            castMagic();
        }
    }

    private void castMagic()
    {
        if (!(lastCastTime + castDelay < Time.time)) return;
        lastCastTime = Time.time;
        Instantiate(magicPrefab, castPoint.position, transform.rotation);
    }
}