using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public static Damage instance;
    public Player _player;
    private bool _isInvincible = false;
    private Animator animator;


    private void Awake()
    {
        instance = this;
    }

    public Animator Animator { set =>  animator = value; }

    public bool IsInvincible { get { return _isInvincible; } set => _isInvincible = value; }

    public void DamagePlayer(int amount)
    {
        if (_isInvincible == false)
        {
            _isInvincible = true;
            animator.SetBool("isHurted", true);
            _player._hp -= amount;
            StartCoroutine(Invincibility());
            _player.Knock();
        }
        else return;
    }

    public void DamageEnemy(int amount)
    {
        //Ennemies._instance._hp -= amount;
    }

    public IEnumerator Invincibility()
    {
        print("je suis invincible");
        yield return new WaitForSeconds(1.5f);
        _isInvincible = false;
        animator.SetBool("isHurted", false);
    }
}
