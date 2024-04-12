using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerEvent _playerEvent;
    public Rigidbody2D _rb;
    public PlayerFeet _feet;
    public Animator _animator;
    public PlayerEvent getInstance { get => _playerEvent.Instance; }

    //Jump Parameter
    [SerializeField] private float _horizontal = 10f;
    [SerializeField] private float _jumpForce = 35f;

    [SerializeField] private bool _isFrozen;
    [SerializeField] private bool _isLookingRight;

    //Collider kb
    [SerializeField] private float _knockBackValue = 15f;

    //Player Stats
    public int _hp;
    private bool _isDead = false;

    private string _spawnPoint = "Spawn";

    // Start is called before the first frame update
    void Start()
    {
        getInstance.PlayerRigidBody = _rb;
        getInstance.IsLookingRight = _isLookingRight;
        getInstance.HorizontalForce = _horizontal;
        getInstance.JumpForce = _jumpForce;
        getInstance.PlayerFeet = _feet;
        getInstance.PlayerAnimator = _animator;
        getInstance.PlayerGameObject = gameObject;
        getInstance.KnockBackValue = _knockBackValue;
        getInstance.ChangePlayerSpawnpoint(GameObject.Find("Spawnpoint").transform.Find(_spawnPoint).gameObject);
        _rb.freezeRotation = true;
        Utils.UtilsRigidBody2D = _rb;
        Damage.instance.Animator = _animator;
    }

    // Update is called once per frame
    void Update()
    {
        getInstance.MoveEvents(Time.deltaTime);

        if(_hp <= 0 && _isDead == false)
        {
            StartCoroutine(Die());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Debug.Log(_hp);
            getInstance.KnockBackValue = 7.5f;
            Damage.instance.IsInvincible = false;
            Damage.instance.DamagePlayer(1);
        }
    }

    private IEnumerator Die()
    {
        _animator.SetTrigger("die");
        _isDead = true;
        yield return new WaitForSeconds(2f);
        _hp = 10;
        _isDead = false;
        _animator.SetFloat("Speed", 0f);
        getInstance.ChangePlayerSpawnpoint(GameObject.Find("Spawnpoint").transform.Find(_spawnPoint).gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            _spawnPoint = collision.gameObject.name;
        }
        else return;
    }
}
