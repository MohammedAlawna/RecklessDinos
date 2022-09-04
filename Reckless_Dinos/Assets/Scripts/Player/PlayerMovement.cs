using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{

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

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
       // _moveSpeed = 580f;

    }

    private void Update()
    {
        if (GameManager._singletonVar._gamePaused ||
            GameManager._singletonVar._gameOver) return;

        if (GameManager._singletonVar._gamePaused ||
          GameManager._singletonVar._gameOver) return;
        _dirX = CrossPlatformInputManager.GetAxis("Horizontal") * _moveSpeed;
        //_dirX = Input.GetAxisRaw("Horizontal") * _moveSpeed;
        _animator.SetFloat("Speed", Mathf.Abs(_dirX));


        if (CrossPlatformInputManager.GetButtonDown("Jump") &&
            rb2d.velocity.y == 0)
        {

            AudioManager.i.PlaySound(AudioManager.i.gameSFX[2]);
            _jump = true;
            _animator.SetBool("isJumping", _jump);
            AudioManager.i.PlaySound(AudioManager.i.gameSFX[2]);
            //rb2d.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
             //rb2d.AddForce(new Vector2(0, _jumpSpeed));
            rb2d.velocity = Vector2.up * _jumpFactor;
            // rb2d.velocity += new Vector2(0f, _jumpSpeed);
        }

        if (_dirX > 0 && !_pFacingRight )
     {
            Flip();
     } 
     if(_dirX < 0 && _pFacingRight)
     {
            Flip();
     }

        rb2d.velocity = new Vector2(_dirX * Time.fixedDeltaTime, 0f);
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


    public void Move(float move)
    {
        Vector2 targetVelocity = new Vector2(move * 10f, rb2d.velocity.y);
        // And then smoothing it out and applying it to the character
        rb2d.velocity = Vector2.SmoothDamp(rb2d.velocity, targetVelocity, 
            ref m_Velocity, m_MovementSmoothing);
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
}
