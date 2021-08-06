using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMovement : MonoBehaviour
{
    [SerializeField] float speed;
    float rayDistance = 2f;

    bool _movingRight = true;

    [SerializeField] Transform _groundDetection; 


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(_groundDetection.position, 
            Vector2.down, rayDistance);

        if(groundInfo.collider == false)
        {
            if(_movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                _movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                _movingRight = true;
            }
        }

    }

 
}
