using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemies : MonoBehaviour
{
    private Animator _animator;
    public Player _player;
    public int _hp;
    private GameObject _attackArea = default;
    private float _attackRange = 2f;
    private float _attackAngle = 30f;
    private LayerMask _playerLayer;
    private float _startPos;
    private float _endPos;
    private float _speed = 5f;
    private bool _movingForward = true;
    private bool _isPlayerInFront = false;
    private bool _isAttacking = false;
    private bool _walk;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _attackArea = transform.GetChild(0).gameObject;
        _playerLayer = LayerMask.GetMask("Player");
        _startPos = transform.position.x;
        _endPos = _startPos + 10f;
        _walk = true;
    }
    void FixedUpdate()
    {
        Walk(_walk);
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
        }

        if(colliders.Length > 0 && _isPlayerInFront && _isAttacking == false)
        {
            _walk = false;
            _player.Side = _movingForward ? 1f : -1f;
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

        if(_hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator PlayAttack()
    {
        _isAttacking = true;
        _animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f);
        _walk = true;
        _isAttacking = false;
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
