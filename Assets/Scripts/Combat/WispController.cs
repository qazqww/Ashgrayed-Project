using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WispController : EnemyAttack {

    public GameObject fire;

    protected override void ContactPlayer()
    {
        base.ContactPlayer();
    }

    protected override void Attack()
    {
        base.Attack();
        if (playerHealth.currentHealth > 0)
        {
            Instantiate(fire, transform.position, Quaternion.identity);
        }
    }
}
