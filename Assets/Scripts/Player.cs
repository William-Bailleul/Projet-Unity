using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class introscript : MonoBehaviour
{
    public Rigidbody2D _rb2d;
    public PlayerFeet _feet;
    public Transform myTransform;

    //Jump Parameter
    private bool _isJumping;
    private float maxTimeJump = 0.2f;
    private float currentJumptime = 0;

    public float _horizontal;
    public float _jumpForce;
    public float _gravityScale;

    public int _currentColorState;

    public bool isLookingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;
        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.freezeRotation = true;
        _isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector2 currentVelocity = new Vector2(0, _rb2d.velocity.y);

        if (Input.GetKey(KeyCode.D))
        {
            currentVelocity.x += _horizontal;
            transform.localScale = new Vector3(1f, 1f, 1f); // Rotation normale (pas de miroir)
        }
        if (Input.GetKey(KeyCode.A))
        {
            currentVelocity.x -= _horizontal;
            transform.localScale = new Vector3(-1f, 1f, 1f); // Rotation normale (pas de miroir)
        }
        
        _rb2d.velocity = currentVelocity;

        if (Input.GetKeyDown(KeyCode.A) && _feet._isGrounded == true)
        {
            _currentColorState++;
        }
        if (Input.GetKeyDown(KeyCode.E) && _feet._isGrounded == true)
        {
            _currentColorState--;
        }

        //if (Input.GetKeyDown(KeyCode.Space) && _feet._isGrounded)
        //    _rb2d.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);

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
        //_rb2d.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
    }
}
