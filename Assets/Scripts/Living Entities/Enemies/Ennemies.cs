using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemies : MonoBehaviour
{
    private Animator _animator;
    public Player _player;
    private BoxCollider2D _collider;
    public int _hp;
    private float _attackRange = 2f;
    private float _attackAngle = 30f;
    private LayerMask _playerLayer;
    [SerializeField]private float patroll;
    private float _startPos;
    private float _endPos;
    private float _speed = 5f;
    [SerializeField]private bool _movingForward;
    private bool _isPlayerInFront = false;
    private bool _isAttacking = false;
    private bool _walk;
    private bool _isDead = false;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>();
        _playerLayer = LayerMask.GetMask("Player");
        _startPos = transform.position.x;
        _endPos = _startPos + patroll;
        _walk = true;
    }
    void FixedUpdate()
    {
        if(_isAttacking == false)
        {
            Walk(_walk);
        } 

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _attackRange, _playerLayer);

        foreach (Collider2D collider in colliders)
        {
            Vector2 directionToPlayer = collider.transform.position - transform.position;
            Vector2 forwardDirection = transform.right;
            if (Vector2.Dot(directionToPlayer.normalized, forwardDirection) > Mathf.Cos(_attackAngle * Mathf.Deg2Rad))
            {
                _isPlayerInFront = true;
                break;
            }
            else _isPlayerInFront = false;
        }

        if(colliders.Length > 0 && _isPlayerInFront && _isAttacking == false)
        {
            _walk = false;
            _player.getInstance.PlayerLookingSide = _movingForward ? 1 : -1;
            Walk(_walk);
            StartCoroutine(PlayAttack());
        }

        if(_movingForward == true && _walk == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(_endPos, transform.position.y), _speed * Time.deltaTime);

            if(transform.position.x >= _endPos)
            {
                _movingForward = false;
                Utils.Flip2D_Object(gameObject, 0f, 180f);
            }
        }
        else if(_movingForward == false && _walk == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(_startPos, transform.position.y), _speed * Time.deltaTime);
            
            if(transform.position.x <= _startPos)
            {
                _movingForward = true;
                Utils.Flip2D_Object(gameObject);
            }
        }

        if(_hp <= 0 && _isDead == false)
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator PlayAttack()
    {
        _isAttacking = true;
        _animator.SetTrigger("Attack");
        yield return new WaitForSeconds(1.5f);
        _walk = true;
        _isAttacking = false;
    }

    private IEnumerator Die()
    {
        _animator.SetTrigger("die");
        _isDead = true;
        _walk = false;
        _isAttacking = false;
        _collider.enabled = false;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
    private void Walk(bool value)
    {
        if (value == true)
        {
            _animator.SetFloat("Speed", 0.5f);
        }
        else _animator.SetFloat("Speed", 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 3)
        {
            Damage.instance.DamagePlayer(1);
        }
    }

}
