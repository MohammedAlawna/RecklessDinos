using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMovement : MonoBehaviour
{
    [SerializeField] Transform[] waypoints;
    [SerializeField] float moveSpeed = 2f;
    int wayPointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = waypoints[wayPointIndex].transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        ProcessMovement();
    }

    private void ProcessMovement()
    {
        transform.position = Vector2.MoveTowards(transform.position
            , waypoints[wayPointIndex].transform.position, moveSpeed * Time.deltaTime);

        if (transform.position == waypoints[wayPointIndex].transform.position)
        {
            wayPointIndex += 1;

        }

        if (wayPointIndex == waypoints.Length) {
            wayPointIndex = 0;
        }


    }
}
