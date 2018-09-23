using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour {

    GameObject player;
    Rigidbody2D rb;
    Vector3 destination;
    Vector2 direction;
    Quaternion rotation;

    float angle;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        destination = player.transform.position;
        direction = transform.position - destination;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void Update()
    {
        rb.AddForce(-direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 100);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
            Destroy(gameObject);
    }
}
