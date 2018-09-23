using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BansheeController : EnemyAttack {

    float attackTimer = 0f;
    float attackDelay = 0.25f;

    public GameObject shockwave;

    protected override void ContactPlayer()
    {
        base.ContactPlayer();
        MoveToDestination();
    }

    protected override void Attack()
    {
        base.Attack();
        if (playerHealth.currentHealth > 0)
        {
            attackTimer += Time.deltaTime * 30;
            if(attackTimer > attackDelay)
            {
                Instantiate(shockwave, transform.position, Quaternion.identity);
                attackTimer = 0f;
            }
        }
    }
}
