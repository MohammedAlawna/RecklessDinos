using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.CrossPlatformInput;


public class PlayerController : MonoBehaviour
{
    [Header("PlayerMovement")]
    public Animator _animator;
    Rigidbody2D rb2d;
    [SerializeField] float _moveSpeed = 40f;
    [SerializeField] float _jumpFactor = 100f;
    float _dirX;
    public bool _isMoving = false;
    public bool _jump = false;
    bool _pFacingRight = true;
    [SerializeField] [Range(0, 1)] float m_MovementSmoothing;
    Vector2 m_Velocity = Vector3.zero;
    public ParticleSystem dust; 


    [Header("PlayerController")]
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
        rb2d = GetComponent<Rigidbody2D>();
        if(onLandEvent == null)
        {
            onLandEvent = new UnityEvent();
        }

    }


    void Start() {
        
    }

    private void FixedUpdate()
    {
        ProcessMovement();
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

    void ProcessMovement(){
        if (GameManager._singletonVar._gamePaused ||
            GameManager._singletonVar._gameOver) return;

        if (GameManager._singletonVar._gamePaused ||
          GameManager._singletonVar._gameOver) return;
        _dirX = CrossPlatformInputManager.GetAxis("Horizontal") * _moveSpeed;
        //_dirX = Input.GetAxisRaw("Horizontal") * _moveSpeed;
        _animator.SetFloat("Speed", Mathf.Abs(_dirX));
        _animator.SetBool("Run", true);

        if (CrossPlatformInputManager.GetButtonDown("Jump") &&
            rb2d.velocity.y == 0)
        {

            rb2d.velocity = Vector2.up * _jumpFactor;
            AudioManager.i.PlaySound(AudioManager.i.gameSFX[2]);
            _jump = true;
            _animator.SetBool("isJumping", _jump);
            AudioManager.i.PlaySound(AudioManager.i.gameSFX[2]);
            //rb2d.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
             //rb2d.AddForce(new Vector2(0, _jumpSpeed));
           
            // rb2d.velocity += new Vector2(0f, _jumpSpeed);
        }
        
        rb2d.velocity = new Vector2(_dirX * Time.fixedDeltaTime, 0f);
        if(_dirX == 0){
            _animator.SetBool("Run", false);
        }

        if (_dirX > 0 && !_pFacingRight )
        {
            Flip();
            _animator.SetBool("Run", true);
        }

     if(_dirX < 0 && _pFacingRight)
        {
            _animator.SetBool("Run", true);
            Flip();
        }
        

        
    }

    public void OnLadning()
    {
        _jump = false;
       
        _animator.SetBool("isJumping", _jump);
        StartCoroutine(ProcessDustCreationWithDelay(0.5f));
        //AudioManager.i.PlaySound(AudioManager.i.gameSFX[1]);
    }

     // TODO
    public void OnSliding(bool isSliding)
    {
        _animator.SetBool("isSliding", isSliding);
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

           GameObject.Destroy(knife, 2.0f);


    }

    /* public void Move(float move)
    {
        Vector2 targetVelocity = new Vector2(move * 10f, rb2d.velocity.y);
        // And then smoothing it out and applying it to the character
        rb2d.velocity = Vector2.SmoothDamp(rb2d.velocity, targetVelocity, 
            ref m_Velocity, m_MovementSmoothing);
    }*/

     void Flip()
    {
        StartCoroutine(ProcessDustCreationWithDelay(0.5f));
        _pFacingRight = !_pFacingRight;

        Vector3 _flippedScale = transform.localScale;
        _flippedScale.x *= -1;
        transform.localScale = _flippedScale;
    }

    public void FreezePosition()
    {
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void SetDeadAnimation()
    {
        _animator.SetBool("isDead", true);
    }


    IEnumerator ProcessDustCreationWithDelay(float delayTime)
    {
        dust.Play();
        yield return new WaitForSeconds(delayTime);
        dust.Stop();
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
            AudioManager.i.PlaySound(AudioManager.i.gameSFX[3]);
            GameManager._singletonVar.IncrementScore(1);
            

            var vfxObject = Instantiate(_coinNumbPrefab, collision.collider.transform.position, 
                Quaternion.identity);
            Destroy(vfxObject, 0.23f);
            Destroy(collision.gameObject);

        }
        if(collision.collider.tag == "Door")
        {
            
            GameManager._singletonVar.ShowGameWinnerPanel();
        }

        

    }

  /*  private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "water")
        {
            Debug.Log("Hello dere!");
            GameManager._singletonVar._gameOver = true;
            
            //GameManager._singletonVar._currentHealth = 0;
        }

        
    }*/


}
