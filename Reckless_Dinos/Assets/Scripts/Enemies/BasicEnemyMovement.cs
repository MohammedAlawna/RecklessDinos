using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMovement : MonoBehaviour
{
    //[SerializeField] Transform[] waypoints;
    [SerializeField] Vector2[] waypoints;
    [SerializeField] float moveSpeed = 2f;
    int wayPointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = waypoints[wayPointIndex];

    }

    // Update is called once per frame
    void Update()
    {
        ProcessMovement();
    }

    private void ProcessMovement()
    {

        for(wayPointIndex = 0; wayPointIndex < waypoints.Length; wayPointIndex++)
        {
            transform.position = Vector2.MoveTowards(transform.position, 
                waypoints[wayPointIndex], moveSpeed * Time.deltaTime);
        }
        if (wayPointIndex == waypoints.Length)
        {
            wayPointIndex = 0;
        }
        

       /* transform.position = Vector2.MoveTowards(transform.position
            , waypoints[wayPointIndex], moveSpeed * Time.deltaTime);

        if (transform.position == waypoints[wayPointIndex])
        {
            wayPointIndex += 1;

        }

        */


    }
}
