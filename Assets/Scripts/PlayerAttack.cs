using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    EnemyHealth enemyHealth;

	void Start () {
		
	}
	
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            enemyHealth.TakeDamage(25, coll.gameObject.transform.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Enemy")
        {
            enemyHealth = coll.gameObject.GetComponent<EnemyHealth>();
            
        }
    }    
}
