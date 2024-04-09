using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D _rb;
    public PlayerFeet _feet;

    //Jump Parameter
    public float _horizontal = 10f;
    public float _jumpForce = 35f;
    private float _time;

    private bool _isJumping;
    public bool _isFrozen;

    //Coyote and Buffer
    public static float _coyoteTime = .15f;
    private float _coyotTimeCounter;
    public static float _JumpBuffer = .2f;
    private float _bufferTimeCounter;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.freezeRotation = true;
        _isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        //print(_feet._isGrounded);
        Vector2 currentVelocity = new Vector2(0, _rb.velocity.y);
        if (!_isFrozen)
        {
            if (Input.GetKey(KeyCode.D))
                currentVelocity.x += _horizontal;
            if (Input.GetKey(KeyCode.A))
                currentVelocity.x -= _horizontal;
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
        
    }

    private void onJumpEvent()
    {
        _feet._isGrounded = false;
        _isJumping = true;
        _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
    }
}
