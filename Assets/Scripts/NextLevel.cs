using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{


    private static int nextScene;
    public static int NextScene { get => nextScene; set => nextScene = value; }


    //Checks if the player collides with stair and takes them to the next level if they collected all tablets
    //or have already collided with the stair once 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            if (UI_Controller.InDialogue == 0 && GameController.CollectedTablets == 2 || 
                UI_Controller.InDialogue == 1 && GameController.CollectedTablets == 4 || 
                UI_Controller.InDialogue == 3 ||
                UI_Controller.InDialogue == 5) 
            {
                nextScene += 1;
                SceneManager.LoadScene(nextScene);
                GameController.CurrentlyCollectedTablets = 0;
            }

        }

    }
}
