using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Staircase : MonoBehaviour
{
    int nextScene;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("test");
        if (collision.gameObject.CompareTag("Player"))
        {
            nextScene++;
            SceneManager.LoadScene(nextScene);
        }
    }

}
