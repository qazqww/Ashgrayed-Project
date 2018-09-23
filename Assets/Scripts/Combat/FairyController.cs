using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyController : EnemyAttack {

    public GameObject lightning;

    bool button;
    float count;

    protected override void ContactPlayer()
    {
        base.ContactPlayer();
    }

    private void Start()
    {
        button = false;
        count = 0f;
    }

    protected override void Update()
    {
        base.Update();

        if (button)
            count += Time.deltaTime;

        if (count > 0.33f && count < 0.355f)
            Instantiate(lightning, transform.position + (player.transform.position - transform.position).normalized * 4f, Quaternion.identity);

        if (count > 0.66f && count < 0.75f)
        {
            Instantiate(lightning, transform.position + (player.transform.position - transform.position).normalized * 7f, Quaternion.identity);
            button = false;
            count = 0f;
        }
    }

    protected override void Attack()
    {
        base.Attack();
        if (playerHealth.currentHealth > 0)
        {
            button = true;
            Instantiate(lightning, transform.position + (player.transform.position - transform.position).normalized * 1f, Quaternion.identity);
        }
    }
}
