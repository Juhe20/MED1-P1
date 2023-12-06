using UnityEngine;

public class TabletCollision : MonoBehaviour
{
    private static bool tabletCollision = false;
    public static bool TabletCollide { get => tabletCollision; set => tabletCollision = value; }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey("e"))
            {
                GameController.CollectedTablets++;
                GameController.Mana++;
                TabletCollide = true;
                gameObject.SetActive(false);
                GameController.MoveSpeed = 0;
            }

        }

    }
}
