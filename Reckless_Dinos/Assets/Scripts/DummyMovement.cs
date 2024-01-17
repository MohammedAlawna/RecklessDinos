using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyMovement : MonoBehaviour
{
    [SerializeField] private  float _jumpFactor = 800f;
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

    public void JumpPressed(){
        
    }

    public void MoveToRightPressed(){
        Debug.Log("Move to left was pressed..");
        rb2d.velocity= new Vector2(150f * Time.fixedDeltaTime, 0f);
    }

    
}
