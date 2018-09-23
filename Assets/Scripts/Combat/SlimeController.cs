using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : EnemyAttack {

    bool isCollision;

    private void Start()
    {
        isCollision = false;
    }

    protected override void ContactPlayer()
    {
        base.ContactPlayer();
        MoveToDestination();
    }

    protected override void Attack()
    {
        if (playerHealth.currentHealth > 0 && isCollision)
        {
            base.Attack();
            playerHealth.TakeDamage(attackDamage);
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Player")
            isCollision = true;    
    }

    private void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
            isCollision = false;
    }
}
