using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public Rigidbody2D _rb2d;
    public PlayerFeet _feet;

    //Jump Parameter
    private bool _isJumping;
    private float _maxTimeJump = 0.2f;
    private float _currentJumptime = 0;

    public float _horizontal;
    public float _jumpForce;
    public float _gravityScale;

    public Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.freezeRotation = true;
        _animator = GetComponent<Animator>();
        _isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentVelocity = new Vector2(0, _rb2d.velocity.y);

        if (Input.GetKey(KeyCode.D))
        {
            _animator.SetFloat("Speed", 0.5f);
            currentVelocity.x += _horizontal;
            gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _animator.SetFloat("Speed", 0.5f);
            currentVelocity.x -= _horizontal;
            gameObject.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else
        {
            _animator.SetFloat("Speed", 0);
        }

        _rb2d.velocity = currentVelocity;

        //Jump event
        if (Input.GetKeyDown(KeyCode.Space) && _feet._isGrounded == true)
        {
            onJumpEvent();
        }
        //Jump continuously while holding till limit
        if (Input.GetKey(KeyCode.Space) && _isJumping == true)
        {
            _currentJumptime += Time.deltaTime;

            if (_currentJumptime < _maxTimeJump)
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
            _currentJumptime = 0;
            _isJumping = false;
        }
    }

    private void onJumpEvent()
    {
        _feet._isGrounded = false;
        _isJumping = true;
        _rb2d.velocity = new Vector2(_rb2d.velocity.x, _jumpForce);
    }
}
