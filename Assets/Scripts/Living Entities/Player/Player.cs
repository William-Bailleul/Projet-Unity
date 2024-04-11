using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D _rb;
    public PlayerFeet _feet;
    public Animator _animator; 
    public Animator GetAnimator { get => _animator; }
    public float Side { get => _checkSide; set => _checkSide = value; }


    //Jump Parameter
    public float _horizontal = 10f;
    public float _jumpForce = 35f;

    public bool _isFrozen;
    public bool _isLookingRight;

    //Collider kb
    [SerializeField] private float knockBackValue;
    private float currentKnockBack;
    private float _checkSide;

    //Coyote and Buffer
    public static float _coyoteTime = .10f;
    private float _coyotTimeCounter;
    public static float _JumpBuffer = .2f;
    private float _bufferTimeCounter;

    //Player Stats
    public int _hp;



    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.freezeRotation = true;
        Utils.UtilsRigidBody2D = _rb;
        Damage.instance.Animator = _animator;
        _isLookingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentKnockBack > 0.1)
        {
            currentKnockBack -= Time.fixedDeltaTime * 7;
        }

        Vector2 currentVelocity = new Vector2(currentKnockBack * _checkSide, _rb.velocity.y);
        if (!_isFrozen)
        {
            if (Input.GetKey(KeyCode.D))
            {
                _animator.SetFloat("Speed", Mathf.Abs(_rb.velocity.x));
                currentVelocity.x += _horizontal;
                _isLookingRight = true;
            }
            if (Input.GetKey(KeyCode.A))
            {
                _animator.SetFloat("Speed", Mathf.Abs(_rb.velocity.x));
                currentVelocity.x -= _horizontal;
                _isLookingRight = false;
            }
        }
        _rb.velocity = currentVelocity;



        if (!_isFrozen)
        {
            //Jump event
            if (_feet._isGrounded)
            {
                _coyotTimeCounter = _coyoteTime;
            }
            else
            {
                _coyotTimeCounter -= Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _bufferTimeCounter = _JumpBuffer;

                if (_coyotTimeCounter > 0f && _bufferTimeCounter > 0f)
                {
                    onJumpEvent();
                    _bufferTimeCounter = 0f;
                }
            }
            else
            {
                _bufferTimeCounter -= Time.deltaTime;

            }

            //Jump continuously while holding till limit
            if (Input.GetKeyUp(KeyCode.Space) && _rb.velocity.y > 0f)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * 0.25f);
                _coyotTimeCounter = 0f;
            }
        }

        if (_isLookingRight == true)
        {
            Utils.Flip2D_Object(gameObject);
        }
        else
        {
            Utils.Flip2D_Object(gameObject, 0f, 180f);
        }

        _animator.SetFloat("Speed", Mathf.Abs(_rb.velocity.x));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Debug.Log(_hp);
            Damage.instance.IsInvincible = false;
            Damage.instance.DamagePlayer(1);
        }
    }
    private void onJumpEvent()
    {
        _feet._isGrounded = false;
        _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
    }
    public void Knock()
    {
        _rb.AddForce(new Vector2(knockBackValue, 25f), ForceMode2D.Impulse);
        currentKnockBack = knockBackValue;
    }
}
