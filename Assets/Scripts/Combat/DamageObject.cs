using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour {

    public int attackDamage = 15;
    public float removeTime = 0.33f;
    float removeTimer = 0f;

    GameObject player;
    PlayerHealth playerHealth;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }
	
	void Update () {
        removeTimer += Time.deltaTime;

        if (removeTimer >= removeTime)
            Destroy(gameObject);
	}

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }
}
