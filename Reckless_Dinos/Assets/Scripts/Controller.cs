using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Controller : MonoBehaviour
{
    Rigidbody2D rb;
    float dirX; 

    private Animator anim;
    
    [SerializeField]
    float moveSpeed = 5f, jumpForce = 600f, kniveSpeed = 500f;

    bool facingRight = true;
    Vector3 localScale; 

    public Transform barrel;
    public GameObject knive; 
    
    // Start is called before the first frame update
    void Start()
    {
       anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        dirX = CrossPlatformInputManager.GetAxis("Horizontal");
        if(Mathf.Abs(dirX) > 0 && rb.velocity.y == 0){
            anim.SetBool("Run", true);
        }
        else 
        {
            anim.SetBool("Run", false);
        }

        //if Jumping
        if(rb.velocity.y > 0){
            anim.SetBool("isJumping", true);
        }
        else {
            anim.SetBool("isJumping", false);
        }

        if(CrossPlatformInputManager.GetButtonDown("Jump")){
           Jump();
        }
        if(CrossPlatformInputManager.GetButtonDown("Fire1")){
           Shoot();
        }
    }

    public void LateUpdate() {
        CheckWhereToFace();
    }

    private void FixedUpdate() {
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

    }

    void CheckWhereToFace(){
        if(dirX > 0){
            facingRight = true;
        }
        else if(dirX < 0) {
            facingRight = false;
        }

        if(((facingRight) && (localScale.x < 0))|| (!facingRight) && (localScale.x > 0) ){
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }

    void Jump(){
        if(rb.velocity.y == 0){
            rb.AddForce(Vector2.up * jumpForce);
        }
    }

    void Shoot(){
        //GameObject knifeShot = Instantiate(knive, transform.position, Quaternion.identity) as GameObject;
        GameObject knife =  Instantiate(knive, transform.position,
           Quaternion.identity) as GameObject;
           
        knive.GetComponent<Rigidbody2D>().velocity = new Vector2(
            kniveSpeed, 0f
        );

        GameObject.Destroy(knife, 2.0f);
       // knife.GetComponent<Rigidbody2D>().AddForce(barrel.up * kniveSpeed);

    }
}
