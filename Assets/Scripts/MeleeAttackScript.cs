using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class MeleeAttackScript : MonoBehaviour
{
    introscript _player;

    public Transform attackPos;
    public LayerMask enemies;
    public float attackRange;

    public bool isAttacking = false;
    public float attackTime;
    public float attackCooldown;
    private float attackTimer = 0f;

    public int damage;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttacking)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
                Attack();
        }

        else
        {
            attackTimer += Time.deltaTime;

            if (attackTimer > attackTime + attackCooldown)
                StopAttack();
        }

    } // end of update

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(attackPos.position, attackRange);
    }

    private void Attack()
    {
        isAttacking = true;
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    private void StopAttack()
    {
        isAttacking = false;
    }
}