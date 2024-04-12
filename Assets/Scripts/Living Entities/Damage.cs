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
            _player.getInstance.KnockBackValue = 15f;
            animator.SetBool("isHurted", true);
            _player._hp -= amount;
            StartCoroutine(Invincibility());
            _player.getInstance.Knock();
        }
        else return;
    }

    public void DamageEnemy(GameObject enemy, int amount)
    {
        if(enemy != null)
        {
            Ennemies target = enemy.GetComponent<Ennemies>();
            if(target != null)
            {
                target._hp -= amount;
            }
            else
            {
                Debug.LogWarning("Enemy script not found on the GameObject" + enemy.name); // can bug
            }
        }
        else
        {
            Debug.LogWarning("GameObject is null");
        }
    }

    public IEnumerator Invincibility()
    {
        yield return new WaitForSeconds(1.5f);
        _isInvincible = false;
        animator.SetBool("isHurted", false);
    }
}
