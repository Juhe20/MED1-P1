using UnityEngine;

public class CamMovement : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float speed;

    void Update()
    {
        //Sets the camera's position to the player's position. Offset can be set in Unity's inspector.
        Vector3 desiredPos = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPos, speed * Time.deltaTime);
    }
}
