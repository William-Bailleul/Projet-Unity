using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private GameObject attackArea = default;
    private GameObject attackDone = default;
    private GameObject attackCooldown = default;

    private bool isAttacking = false;

    private float timeToAttack = 0.10f;
    private float cooldown = 0.30f;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        attackArea = transform.GetChild(0).gameObject;
        attackDone = transform.GetChild(1).gameObject;
        attackCooldown = transform.GetChild(2).gameObject;

        attackDone.SetActive(false);
        attackCooldown.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0)){
            Attack();
        }

        if (isAttacking)
        {
            timer += Time.deltaTime;
            

            if (timer > timeToAttack)
            {
                
                attackArea.SetActive(isAttacking);
                attackDone.SetActive(false);
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
        attackDone.SetActive(true);
        attackCooldown.SetActive(true);
    }



}
