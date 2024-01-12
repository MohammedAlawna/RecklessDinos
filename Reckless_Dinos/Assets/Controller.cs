using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    Rigidbody2D rb; 
    int _jumpPower;

    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space)){
            rb.velocity = new Vector2(rb.velocity.x, _jumpPower);
        Debug.Log("Hello!");
        }
    }
}
