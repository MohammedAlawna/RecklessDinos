using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{


    public PlayerMovement _movement;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Transform _ceilingCheck;
    [SerializeField] private float _groundRadius = 0.2f;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] float _immuneDuration = 0.35f;
    [SerializeField] float projectileSpeed = 10f;

    bool _pGrounded;
    [SerializeField] bool _isImmune = false;

    [SerializeField] GameObject _coinNumbPrefab;
    [SerializeField] GameObject _knifePrefab; 


    [Header("EventSystem")]
    public UnityEvent onLandEvent;
    

    /* [System.Serializable]
     public class BoolEvent: UnityEvent<bool> { }
     public BoolEvent onSlidingEvent;*/

    private void Awake()
    {
        if(onLandEvent == null)
        {
            onLandEvent = new UnityEvent();
        }

       /* 
        if(onSlidingEvent == null)
        {
            onSlidingEvent = new UnityEvent();
        }
        */
    }

    /* void Update() {
         Attack();
     }*/

    void Start() {
        
    }

    private void FixedUpdate()
    {
        bool _wasGrounded = _pGrounded;
        _pGrounded = false;

        Collider2D[] colliders2D = Physics2D.OverlapCircleAll(_groundCheck.position, _groundRadius, _whatIsGround);
        for(int i = 0; i < colliders2D.Length; i++)
        {
            if(colliders2D[i].gameObject != gameObject)
            {
                _pGrounded = true;
                if(!_wasGrounded)
                {
                    onLandEvent.Invoke();
                }
            }

        }



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        //TODO Add immune duratrion (with some blinking to the player)
        //TODO Don't forget enemyAI script! 
        if(collision.collider.tag == "Enemy" && !_isImmune)
        {
            GameManager._singletonVar.TakeDamage(1);
        }

        if(collision.collider.tag == "Coin")
        {
            GameManager._singletonVar.IncrementScore(1);
            Destroy(collision.gameObject);

            var vfxObject = Instantiate(_coinNumbPrefab, collision.collider.transform.position, 
                Quaternion.identity);
            Destroy(vfxObject, 0.57f);

        }

        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "water")
        {
            Debug.Log("Hello dere!");
            GameManager._singletonVar._gameOver = true;
            //GameManager._singletonVar._currentHealth = 0;
        }

        
    }


    public void Attack()
    {
        /* Adjust Number of Attacks.
        GameManager._singletonVar._noKnives--;
        if (GameManager._singletonVar._noKnives <= 0) return;*/
       GameObject knife =  Instantiate(_knifePrefab, transform.position,
           Quaternion.identity) as GameObject;
      
        knife.GetComponent<Rigidbody2D>().velocity = new Vector2(
            projectileSpeed, 0f);


    }

}
