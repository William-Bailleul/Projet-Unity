using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D _rb2d;
    public int _hp;
    public Animator _animator;

    public PlayerFeet _feet;

    //Jump Parameter
    public float _horizontal = 10f;
    public float _jumpForce = 35f;
    private float _time;

    private bool _isJumping;
    public bool _isFrozen;
    public bool _isLookingRight;

    //Coyote and Buffer
    public static float _coyoteTime = .15f;
    private float _coyotTimeCounter;
    public static float _JumpBuffer = .2f;
    private float _bufferTimeCounter;

    public Animator GetAnimator {  get => _animator; }

    private void Start()
    {
        Damage.instance.Animator = _animator;
        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.freezeRotation = true;
        _isJumping = false;
        _isLookingRight = true;
        Utils.UtilsRigidBody2D = _rb2d;
    }

    void Update()
    {
        //print(_feet._isGrounded);
        Vector2 currentVelocity = new Vector2(0, _rb2d.velocity.y);
        if (!_isFrozen)
        {
            if (Input.GetKey(KeyCode.D))
            {
                currentVelocity.x += _horizontal;
                _isLookingRight = true;
            }
            if (Input.GetKey(KeyCode.A))
            {
                currentVelocity.x -= _horizontal;
                _isLookingRight = false;
            }
        }
        _rb2d.velocity = currentVelocity;



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
            if (Input.GetKeyUp(KeyCode.Space) && _rb2d.velocity.y > 0f)
            {
                _rb2d.velocity = new Vector2(_rb2d.velocity.x, _rb2d.velocity.y * 0.25f);
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            Debug.Log(_hp);
            if (Damage.instance.IsInvincible == false)
            {
                Damage.instance.IsInvincible = true;
                _hp -= Damage.instance.DamagePlayer(1);
                StartCoroutine(Damage.instance.Invincibility());
            }
            else return;
        }
    }

    //public void Flip(float rotation)
    //{
    //    _rb2d.transform.localRotation = Quaternion.Euler(0f, rotation, 0f);
    //}

    private void onJumpEvent()
    {
        _feet._isGrounded = false;
        _isJumping = true;
        _rb2d.velocity = new Vector2(_rb2d.velocity.x, _jumpForce);
    }

}
