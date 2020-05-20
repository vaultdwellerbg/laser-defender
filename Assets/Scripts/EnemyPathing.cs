using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] WaveConfig waveConfig;
    float moveSpeed;
    List<Transform> waypoints;
    int currentWaypointIndex = 0;

    private void Start()
    {
        moveSpeed = waveConfig.GetMoveSpeed();
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[currentWaypointIndex].transform.position;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (currentWaypointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[currentWaypointIndex].transform.position;
            var movementThisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition)
            {
                currentWaypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
