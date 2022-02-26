using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trialJump : MonoBehaviour
{
    [SerializeField] float _jumpFactor = 100f;
    [SerializeField] bool _goJump;

    Rigidbody2D rb2d;
    
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    

    public void Jump()
    {
        rb2d.velocity = Vector2.up * _jumpFactor;
    }

    

    
}
