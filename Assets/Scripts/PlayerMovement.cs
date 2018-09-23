using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

    public float moveSpeed = 6f;
    public int damage = 20;

    private int dir = 0; // 0:front 1:back 2:left 3:right
    private static bool playerExists;

    float knockbackCount;
    bool knockbackButton;

    [HideInInspector]
    public Vector3 temp_position;
    public Vector3 movement;
    public AudioClip attackClip;

    protected Animator anim;
    protected Rigidbody2D rb;
    Rigidbody2D enemyRb;
    GameObject curEnemy;
    Vector3 enemyPos;
    AudioSource playerAudio;

    public GameObject leftBox;
    public GameObject rightBox;
    public GameObject upBox;
    public GameObject downBox;

    protected void Awake () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerAudio = GetComponent<AudioSource>();
        SetCollision();
        knockbackCount = 0;
        knockbackButton = false;

        if (!playerExists && gameObject.name != "플레이어(cook)")
        {
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
	}

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        MoveAnim(h, v);

        movement.Set(h, v, 0f);
        movement = movement.normalized * moveSpeed * Time.deltaTime;

        rb.MovePosition(transform.position + movement);


        Vector2 pushForce = new Vector2(100, 50);

        if (transform.position.x > enemyPos.x)
            pushForce.x = -pushForce.x;

        if (Input.GetMouseButtonDown(0))
        {
            anim.SetInteger("isMove", 10);
            knockbackButton = true;
        }

        //force = (enemyPos - transform.position).normalized * 10;

        if (knockbackButton)
        {
            knockbackCount += Time.deltaTime;
            if (enemyRb != null) {
                enemyRb.AddForce(pushForce);
                enemyRb.AddForce(pushForce);
            }
        }

        if (knockbackCount >= 0.50f)
        {
            knockbackButton = false;
            knockbackCount = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            curEnemy = coll.gameObject;
            enemyPos = coll.gameObject.transform.position;
            enemyRb = coll.gameObject.GetComponent<Rigidbody2D>();
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            curEnemy = null;
            enemyRb = null;
        }
    }

    public void AttackEnemy(GameObject enemy)
    {
        try
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage, enemy.transform.position);
                //enemyHealth.ShowHitEffect();
            }
        }
        catch { }
    }

    protected void MoveAnim(float h, float v)
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
