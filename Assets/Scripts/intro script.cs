using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class introscript : MonoBehaviour
{
    public Rigidbody2D _rb2d;
    public PlayerFeet _feet;

    //Jump Parameter
    private bool _isJumping;
    private float minTimeJump = 0.2f;
    private float maxTimeJump = 0.2f;
    private float currentJumptime = 0;

    public float _horizontal;
    public float _jumpForce;
    public float _gravityScale;

    // Start is called before the first frame update
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.freezeRotation = true;
        _isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        print(_feet._isGrounded);
        Vector2 currentVelocity = new Vector2(0, _rb2d.velocity.y);

        if (Input.GetKey(KeyCode.D))
            currentVelocity.x += _horizontal;
        if (Input.GetKey(KeyCode.A))
            currentVelocity.x -= _horizontal;

        _rb2d.velocity = currentVelocity;

        //Jump event
        if (Input.GetKeyDown(KeyCode.Space) && _feet._isGrounded == true)
        {
            onJumpEvent();
        }
        //Jump continuously while holding till limit
        if (Input.GetKey(KeyCode.Space) && _isJumping == true)
        {
            currentJumptime += Time.deltaTime;

            if (currentJumptime < maxTimeJump)
            {
                onJumpEvent();
            }
        }
        else
        {
            _isJumping = false;
        }

        if (_feet._isGrounded == true)
        {
            currentJumptime = 0;
            _isJumping = false;
        }
    }

    private void FixedUpdate()
    {
        
    }

    private void onJumpEvent()
    {
        _feet._isGrounded = false;
        _isJumping = true;
        _rb2d.velocity = new Vector2(_rb2d.velocity.x, _jumpForce);
    }
}
