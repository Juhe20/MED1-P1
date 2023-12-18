using UnityEngine;

public class TabletCollision : MonoBehaviour
{
    private static bool tabletCollision = false;
    public static bool TabletCollide { get => tabletCollision; set => tabletCollision = value; }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Updates variables for other scripts when clicking the stone tablet. Timescale freezes the game.
            if (Input.GetKey("e")) 
            {
                GameController.CollectedTablets++;
                GameController.CurrentlyCollectedTablets++;
                GameController.Mana++;
                TabletCollide = true;
                gameObject.SetActive(false);
                Time.timeScale = 0;
            }
        }
    }
}
