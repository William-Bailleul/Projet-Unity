using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private ScriptableStats _stats;
    public Rigidbody2D _rb;
    public PlayerFeet _feet;

    //Jump Parameter
    public float timeLeftGrounded;
    private float _timeLeftGrounded = float.MinValue;

    private bool _isJumping;
    public bool _isFrozen;



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
                currentVelocity.x += _stats.horizontal;
            if (Input.GetKey(KeyCode.A))
                currentVelocity.x -= _stats.horizontal;
        }
        _rb.velocity = currentVelocity;

        if (!_isFrozen)
        {
            //Jump event
            if (Input.GetKeyDown(KeyCode.Space) && _feet._isGrounded)
            {
                onJumpEvent();
            }
            if (Input.GetKeyDown(KeyCode.Space) && !_feet._isGrounded && _timeLeftGrounded + _stats.coyoteTime > Time.time)
            {
                onJumpEvent();
            }
            //Jump continuously while holding till limit
            if (Input.GetKey(KeyCode.Space) && _isJumping)
            {
                _stats.currentJumpTime += Time.deltaTime;

                if (_stats.currentJumpTime < _stats.maxTimeJump)
                {
                    //print("jumping");
                    onJumpEvent();
                }
            }
            else
            {
                _isJumping = false;
            }

            if (_feet._isGrounded == true)
            {
                _stats.currentJumpTime = 0;
                _isJumping = false;
            }
        }
        
    }

    private void FixedUpdate()
    {
        
    }

    private void onJumpEvent()
    {
        _feet._isGrounded = false;
        _isJumping = true;
        _rb.velocity = new Vector2(_rb.velocity.x, _stats.jumpForce);
    }
}
