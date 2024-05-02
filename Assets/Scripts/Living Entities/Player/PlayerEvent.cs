using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEvent : MonoBehaviour
{
    private PlayerEvent _instance;
    // Player Components
    private Rigidbody2D _rb;
    private PlayerFeet _feet;
    private Animator _animator;
    private GameObject _gameObject;
    [SerializeField] private PauseMenu _pauseMenu;
    [SerializeField] private float _buildupSpeed;

    public Rigidbody2D PlayerRigidBody { get => _rb; set => _rb = value; }
    public PlayerFeet PlayerFeet { get => _feet; set => _feet = value; }
    public Animator PlayerAnimator { get => _animator; set => _animator = value; }
    public GameObject PlayerGameObject { get => _gameObject; set => _gameObject = value; }
    public PlayerEvent Instance { get => _instance; }

    // Movement values
    private float _horizontal;
    private float _jumpForce;
    private bool _isFrozen;
    private bool _isLookingRight;
    private bool _isAttacking = false;

    public float HorizontalForce { get => _horizontal; set => _horizontal = value; }
    public float JumpForce { get => _jumpForce; set => _jumpForce = value; }
    public bool IsFrozen { get => _isFrozen; set => _isFrozen = value; }
    public bool IsPaused { get => _isFrozen; set => _isFrozen = value; }
    public bool IsLookingRight { get => _isLookingRight; set => _isLookingRight = value; }

    // Knockback values
    private float _knockBackValue;
    private float _currentKnockBack;
    private int _checkSide;

    public float KnockBackValue { get => _knockBackValue; set => _knockBackValue = value; }
    public int PlayerLookingSide { get => _checkSide; set => _checkSide = value; }

    // Coyote and Buffer
    private float _coyoteTime = .10f;
    private float _coyoteTimeCounter;
    private float _jumpBuffer = .2f;
    private float _bufferTimeCounter;

    private void Awake()
    {
        _instance = this;
        IsPaused = false;
    }

    public void MoveEvents(float dT)
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _pauseMenu.PauseGame();
        }

        float dirX = 0;

        if(_isFrozen == false)
        {
            if(Input.GetKey(KeyCode.D))
            {
                _animator.SetFloat("Speed", Mathf.Abs(_rb.velocity.x));
                dirX += 1;
                _isLookingRight = true;
            }
            if(Input.GetKey(KeyCode.A))
            {
                _animator.SetFloat("Speed", Mathf.Abs(_rb.velocity.x));
                dirX -= 1;
                _isLookingRight = false;
            }
            _rb.velocity = new Vector2(_horizontal * dirX, _rb.velocity.y);
        }

        if(Input.GetKeyDown(KeyCode.Mouse0) && _isAttacking == false)
        {
            StartCoroutine(PlayAttack());
        }

        if(_isFrozen == false)
        {
            if(_feet._isGrounded == true) // Jump event section
            {
                _coyoteTimeCounter = _coyoteTime;
            }
            else
            {
                _coyoteTimeCounter -= dT;
            }

            if(Input.GetKeyDown(KeyCode.Space))
            {
                _bufferTimeCounter = _jumpBuffer;

                if(_coyoteTimeCounter > 0f & _bufferTimeCounter > 0f)
                {
                    OnJumpEvent();
                    _bufferTimeCounter = 0f;
                }
            }
            else
            {
                _bufferTimeCounter -= dT;
            }

            // Jump continuously while holding till limit
            if(Input.GetKeyUp(KeyCode.Space) && _rb.velocity.y > 0f)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * 0.25f);
                _coyoteTimeCounter = 0f;
            }
        }

        if(_isLookingRight == true)
        {
            Utils.Flip2D_Object(_gameObject);
        }
        else
        {
            Utils.Flip2D_Object(_gameObject, 0f, 180f);
        }

        _animator.SetFloat("Speed", Mathf.Abs(_rb.velocity.x));
    }

    private void OnJumpEvent()
    {
        _feet._isGrounded = false;
        _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
    }

    public IEnumerator Knock()
    {
        _isFrozen = true;
        _rb.AddForce(new Vector2(_knockBackValue * _checkSide, 25f), ForceMode2D.Impulse);
        if (_rb.velocity.y > 25f)
            _rb.velocity = new Vector2(_rb.velocity.x, 25f);
        yield return new WaitForSeconds(0.2f);
        _isFrozen = false;
    }

    private IEnumerator PlayAttack()
    {
        _isAttacking = true;
        _animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.8f);
        _isAttacking = false;
    }

    public void ChangePlayerSpawnpoint(GameObject gameObject)
    {
        _rb.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

}