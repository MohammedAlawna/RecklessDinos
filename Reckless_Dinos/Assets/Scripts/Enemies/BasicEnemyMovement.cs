using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMovement : MonoBehaviour
{



    // Update is called once per frame
    void Update()
    {
       

    }


    /* Special Code For PatrollingBehaviour
    private void PatrollingBehaviourOnTile()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(_groundDetection.position,
            Vector2.down, rayDistance);

        if (groundInfo.collider == false)
        {
            if (_movingRight)
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

    void initValues()
    {
        [SerializeField] float speed;
        [SerializeField] float rayDistance = 2f;

        bool _movingRight = true;

        [SerializeField] Transform _groundDetection;
    }
    */
 
}
