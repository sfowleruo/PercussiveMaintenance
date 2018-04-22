using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public List<Vector3> WaypointsToGoal;
    public void SpawnEnemy(BaseEnemy enemy)
    {
        Debug.Log("Spawned: " + enemy.ID);
        enemy.transform.position = transform.position;
        enemy.Waypoints = new List<Vector3>(WaypointsToGoal);
    }
}