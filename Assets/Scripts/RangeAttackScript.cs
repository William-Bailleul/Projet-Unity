using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerAttack : MonoBehaviour
{

    introscript _player;


    private bool isAttacking = false;


    private const float attackTime = 0.10f;
    private const float attackCooldown = 0.30f;

    private float attackTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttacking)
        {
            if (Input.GetKey(KeyCode.Mouse0))
                Attack();
        }

        else
        {
            attackTimer += Time.deltaTime;

            if (attackTimer > attackTime + attackCooldown)
                StopAttack();
        }

    } // end of update

    private void Attack()
    {
        isAttacking = true;
    }

    private void StopAttack()
    {
        isAttacking = false;
    }
}