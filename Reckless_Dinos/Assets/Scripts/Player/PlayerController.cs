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
    [SerializeField] float _moveSpeed = 40f;
    [SerializeField] float jumpingFactor = 800f; 
    Rigidbody2D rb2d;
    float _dirX;
    float _dirY;
    public bool _isMoving = false;
    public bool _isJumping = false;
    
    bool _pFacingRight = true;
    [SerializeField] [Range(0, 1)] float m_MovementSmoothing;
    Vector2 m_Velocity = Vector3.zero;
    public ParticleSystem dust; 

    [Header("PlayerController")]
    //public PlayerMovement _movement;
    //[SerializeField] private Transform _groundCheck;
    //[SerializeField] private Transform _ceilingCheck;
    //[SerializeField] private float _groundRadius = 0.2f;
   // [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] float _immuneDuration = 0.35f;
    [SerializeField] float projectileSpeed = 10f;

    public bool isCollided = false;
    [SerializeField] bool _isImmune = false;

    [SerializeField] GameObject _coinNumbPrefab;
    [SerializeField] GameObject _knifePrefab; 

    //[SerializeField] GameObject _groundCheck; 

    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        ProcessHorizontalMovement();
    }

    public void ProcessJump(){
        _isJumping = true;
        _animator.SetBool("isJumping", true);
        rb2d.AddForce(new Vector2(0f, jumpingFactor) * Time.DeltaTime, ForceMode2D.Impulse);
    }

    void ProcessHorizontalMovement(){
        

        if(isCollided == true) {
           //_isJumping = false;
           /* Vector3 defectionFactor = new Vector3(-0.003f, 0f, 0f);
            transform.position += defectionFactor;*/
           _animator.SetBool("Run", false);
           // _animator.SetBool("isJumping", false);
           // isCollided = false;
            return;
        }
        if(_isJumping == true) return;

        if(!_isJumping) _animator.SetBool("isJumping", false);
        

        if (GameManager._singletonVar._gamePaused ||
            GameManager._singletonVar._gameOver) return;

        if (GameManager._singletonVar._gamePaused ||
          GameManager._singletonVar._gameOver) return;

        _dirX = CrossPlatformInputManager.GetAxis("Horizontal") * _moveSpeed;

    
  

        if(/*isCollided == true &&*/ _dirX == 0){
            _animator.SetBool("Run", false);
        }

        if(/*isCollided == true ||*/ _dirX == 0){
            _animator.SetBool("Run", false);
           // _dirX = 0;
        }

        if(/*isCollided == false && */_dirX > 0 || _dirX < 0)
        {
            isCollided = false;
           //_animator.SetFloat("Speed", Mathf.Abs(_dirX));
           _animator.SetBool("Run", true);
        }
        
        
        rb2d.velocity = new Vector2(_dirX * Time.deltaTime, 0f);

        
        if (_dirX > 0 && !_pFacingRight )
        {
            Flip();
           // _animator.SetBool("Run", true);
        }

        if(_dirX < 0 && _pFacingRight)
        {
           // _animator.SetBool("Run", true);
            Flip();
        }
    }

     // TODO
    public void OnSliding(bool isSliding)
    {
        _animator.SetBool("isSliding", isSliding);
    }


    public void Attack()
    {
       GameObject knife =  Instantiate(_knifePrefab, transform.position,
           Quaternion.identity) as GameObject;
      
        knife.GetComponent<Rigidbody2D>().velocity = new Vector2(
            projectileSpeed, 0f);

           GameObject.Destroy(knife, 2.0f);
    }

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
    IEnumerator ProcessCollidingWithOthers(float waitTime){
        _isJumping = false;
        isCollided = true;
      //  transform.position += new Vector3(-0.2f, 0f, 0f);
        yield return new WaitForSeconds(waitTime);
        isCollided = false;
    }
    
    
    //OnMoveButton Clicked (Fix Buggy Character)
    public void EnableRunningAnimation(){
        _animator.SetBool("Run", true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        //Disable Animation If Collided (Separate function for enabling it..)
        if(collision.collider.tag == "Other"){
           _animator.SetBool("Run", false);
           StartCoroutine(ProcessCollidingWithOthers(0.20f));
    //    _animator.SetBool("Run", false);
        }
      
      
        
        //Check if Grounded
        if(collision.collider.tag == "Ground"){
            _animator.SetBool("isJumping", false);
            _isJumping = false;
            isCollided = false;
            
        }
        if(collision.collider.tag != "Ground" && collision.collider.tag != "Other"){
            _isJumping = true;
        }

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

    private void OnTriggerEnter2D(Collider2D other) {
        //If Collided with Coin:
        if(other.tag == "Coin"){
            AudioManager.i.PlaySound(AudioManager.i.gameSFX[3]);
            GameManager._singletonVar.IncrementScore(1);
            
             var vfxObject = Instantiate(_coinNumbPrefab, other.transform.position, 
                Quaternion.identity);
            Destroy(vfxObject, 0.23f);
            Destroy(other.gameObject);
        }
    }
}
