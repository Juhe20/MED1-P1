using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    private static int nextScene;
    public static int NextScene { get { return nextScene; } }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            nextScene += 1;
            Debug.Log(nextScene);
            SceneManager.LoadScene(nextScene);
        }

    }
}
