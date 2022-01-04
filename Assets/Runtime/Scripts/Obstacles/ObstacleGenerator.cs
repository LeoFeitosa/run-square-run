using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] obstacles;
    GameObject obstaclePrefab;

    void Start()
    {
        SpawnObstacle();
    }

    void SpawnObstacle()
    {
        GameObject obstacle = obstacles[Random.Range(0, obstacles.Length)];
        obstaclePrefab = Instantiate(obstacle, transform);
        //obstaclePrefab.transform.localPosition = Vector2.zero;
        obstaclePrefab.transform.rotation = Quaternion.identity;
    }
}
