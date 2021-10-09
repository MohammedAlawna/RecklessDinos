using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Range(0, 10)]
    [SerializeField] int chaseSpeed = 1;
    [SerializeField] int health = 1;
    [SerializeField] float chaseDistnace = 5f;

    float distanceToPlayer;

    // Update is called once per frame
    void Update()
    {
        if (GameManager._singletonVar._gameOver ||
           GameManager._singletonVar._gamePaused)
            return;
        MoveEnemy(Vector2.left);
        //ProcessChasingBehaviour();             
    }

    private void ProcessChasingBehaviour()
    {
        //This if for chasing. (We'll use it later in advanced UI enemies)
        GameObject player = GameObject.FindWithTag("Player");
        distanceToPlayer = Vector2.Distance(player.transform.position,
            transform.position);

        if (distanceToPlayer < chaseDistnace)
        {
            Debug.Log("Chasing System Works Correctly! CHASE!");
            MoveEnemy(Vector2.left);
        }
    }

    //This method was created to test the animation event thingy!
    public void SetChasingSpeed(int speed)
    {
        chaseSpeed = speed;
    }

    private void MoveEnemy(Vector2 sideToMove)
    {
        transform.Translate(sideToMove * chaseSpeed * Time.deltaTime);
    }
}
/*Detect collision with the bullet/attack came from the player, dec health then!
       if(health <= 0 )
       {
           health = 0;
           //SetDead Animation.(With delay to destroy that object)
           //
       }
       */
