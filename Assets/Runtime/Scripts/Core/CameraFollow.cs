using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float offSetX;

    void LateUpdate()
    {
        Vector3 currentPosition = transform.position;
        Vector2 playerPosition = player.transform.position;
        currentPosition.x = playerPosition.x + offSetX;
        transform.position = currentPosition;
    }
}
