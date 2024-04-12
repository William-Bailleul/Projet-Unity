using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private int opponentDamage = 2;
    private int playerDamage = 3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject parent = transform.parent.gameObject;
        if(collision.gameObject.layer == 6 && parent.layer == 7)
        {
           Damage.instance.DamagePlayer(opponentDamage);
        }
        else if(collision.gameObject.layer == 7 && parent.layer == 6)
        {
            Damage.instance.DamageEnemy(collision.gameObject, playerDamage);
        }
    }
}
