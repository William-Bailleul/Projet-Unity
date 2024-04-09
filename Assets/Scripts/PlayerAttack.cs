using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerAttack : MonoBehaviour
{

    introscript _player;



    private GameObject attackArea = default;

    private GameObject attackShow = default;
    private GameObject attackCooldown = default;
    private GameObject attackTouched = default;

    private bool isAttacking = false;
    

    private float timeToAttack = 0.10f;
    private float cooldown = 0.30f;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {

        attackArea = transform.GetChild(1).GetChild(0).gameObject;

        //test attack
        attackTouched = transform.GetChild(1).GetChild(1).gameObject;
        attackShow = transform.GetChild(1).GetChild(2).gameObject;
        attackCooldown = transform.GetChild(1).GetChild(3).gameObject;


        attackTouched.SetActive(false);
        attackShow.SetActive(false);
        attackCooldown.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0)){
            CheckDirection();
            Attack();
        }

        if (isAttacking)
        {
            timer += Time.deltaTime;
            

            if (timer > timeToAttack)
            {
                
                attackArea.SetActive(isAttacking);
                attackShow.SetActive(false);
            }

            if (timer > timeToAttack + cooldown)
            {
                timer = 0;
                isAttacking = false;
                attackCooldown.SetActive(false);
            }
        }


    }

    private void Attack()
    {
        isAttacking = true;
        attackArea.SetActive(isAttacking);
        attackShow.SetActive(true);
        attackCooldown.SetActive(true);
    }

    private void CheckDirection()
    {
        print(_player.isLookingRight);
        /*
        if (player.isLookingRight == true)
        {
            transform.GetChild(1).position = player.myTransform.position + new Vector3(1f, 0f, 0f);
        }

        else
        {
            transform.GetChild(1).position = player.myTransform.position + new Vector3(-1f, 0f, 0f);
        }
        */

    }


}
