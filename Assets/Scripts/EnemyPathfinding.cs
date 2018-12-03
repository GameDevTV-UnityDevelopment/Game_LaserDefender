﻿using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    private WaveConfig waveConfig;
    private List<Transform> waypoints;
    private int waypointIndex = 0;


    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private void Start()
    {
        waypoints = waveConfig.GetWaypoints();

        transform.position = waypoints[waypointIndex].position;
    }

    private void Update()
    {
       Move();
    }

    private void Move()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            Vector3 targetPosition = waypoints[waypointIndex].position;
            float movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}