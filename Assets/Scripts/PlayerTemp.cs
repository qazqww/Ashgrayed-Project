using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTemp : MonoBehaviour
{

    public float moveSpeed = 6f;
    public int damage = 20;

    private int dir = 0; // 0:front 1:back 2:left 3:right
    private static bool playerExists;

    [HideInInspector]
    public Vector3 temp_position;
    public Vector3 movement;
    public AudioClip attackClip;

    Animator anim;
    Rigidbody2D rb;
    GameObject curEnemy;
    AudioSource playerAudio;

    public GameObject leftBox;
    public GameObject rightBox;
    public GameObject upBox;
    public GameObject downBox;

    protected void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerAudio = GetComponent<AudioSource>();
        SetCollision();
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        MoveAnim(h, v);

        movement.Set(h, v, 0f);
        movement = movement.normalized * moveSpeed * Time.deltaTime;

        rb.MovePosition(transform.position + movement);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetInteger("isMove", 10);
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            curEnemy = coll.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            curEnemy = null;
        }
    }

    public void AttackEnemy(GameObject enemy)
    {
        try
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
                enemyHealth.TakeDamage(damage, enemy.transform.position);
        }
        catch { }
    }

    void MoveAnim(float h, float v)
    {
        // 4: frontmove 5:back 6:left 7:right
        if (v > 0) // backmove
        {
            anim.SetInteger("isMove", 5);
            SetCollision();
            upBox.SetActive(true);
            dir = 1;
        }

        if (v < 0) // front
        {
            anim.SetInteger("isMove", 4);
            SetCollision();
            downBox.SetActive(true);
            dir = 0;
        }

        if (h > 0) // right
        {
            anim.SetInteger("isMove", 7);
            SetCollision();
            rightBox.SetActive(true);
            dir = 3;
        }

        if (h < 0) // left
        {
            anim.SetInteger("isMove", 6);
            SetCollision();
            leftBox.SetActive(true);
            dir = 2;
        }

        // 0: frontidle 1:back 2:left 3:right
        if (v == 0 && dir == 0)
        {
            anim.SetInteger("isMove", 0);
        }

        if (v == 0 && dir == 1)
        {
            anim.SetInteger("isMove", 1);
        }

        if (h == 0 && dir == 2)
        {
            anim.SetInteger("isMove", 2);
        }

        if (h == 0 && dir == 3)
        {
            anim.SetInteger("isMove", 3);
        }
    }

    void SetCollision()
    {
        leftBox.SetActive(false);
        rightBox.SetActive(false);
        upBox.SetActive(false);
        downBox.SetActive(false);
    }

    void AttackSound()
    {
        playerAudio.clip = attackClip;
        playerAudio.Play();
        AttackEnemy(curEnemy);
    }
}
