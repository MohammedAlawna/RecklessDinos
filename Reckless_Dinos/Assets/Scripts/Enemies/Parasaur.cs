using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parasaur : MonoBehaviour
{
    [SerializeField] int health = 1;
    [SerializeField] float speed = 550f;

    [SerializeField] float chaseDistnace = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindWithTag("Player");
       float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);
        
        if(distanceToPlayer < chaseDistnace)
        {
            print("Chase!");
        }


     /*Detect collision with the bullet/attack came from the player, dec health then!
        if(health <= 0 )
        {
            health = 0;
            //SetDead Animation.(With delay to destroy that object)
            //
        }
        */
    }
}
