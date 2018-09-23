using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    public float timeBetweenAttacks = 2f; // 공격 쿨다운
    public int attackDamage = 10; // 공격 데미지
    public float chaseDistance = 5f; // 인식 거리
    public float attackDistance = 1.5f; // 공격 거리
    public float moveSpeed = 1.25f;

    protected GameObject player;
    protected PlayerHealth playerHealth;
    Animator anim;
    AudioSource enemyAudio;
    //EnemyHealth enemyHealth;

    protected float timer = 0f;

    bool playerInRange;

    public float timeBetweenMove;
    public float timeToMove;

    public bool moving;
    private float timeBetweenMoveCounter;
    private float timeToMoveCounter;

    Vector3 moveDir;
    Rigidbody2D rb;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        //enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        enemyAudio = GetComponent<AudioSource>();

        timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
        timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);
    }

    protected virtual void Update()
    {
        timer += Time.deltaTime;

        if (GetDistanceFromPlayer() < chaseDistance)
            TurnToDestination();

        if (GetDistanceFromPlayer() < attackDistance) // 공격 거리 안에 들어올 경우
        {
            if (timer >= timeBetweenAttacks)
                Attack();
        }
        else if (GetDistanceFromPlayer() < chaseDistance) // 인식 거리 안에 들어올 경우
        {
            ContactPlayer();
        }

        if (moving)
        {
            timeToMoveCounter -= Time.deltaTime;
            rb.velocity = moveDir;

            if (timeToMoveCounter < 0f)
            {
                moving = false;
                timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
            }
        }
        else
        {
            timeBetweenMoveCounter -= Time.deltaTime;
            rb.velocity = Vector2.zero;
            if (timeBetweenMoveCounter < 0f)
            {
                moving = true;
                timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);
                moveDir = new Vector3(Random.Range(-1f, 1f) * moveSpeed, Random.Range(-1f, 1f) * moveSpeed, 0f);
            }
        }

        /*
        if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
            Attack();

        if (playerHealth.currentHealth <= 0)
            anim.SetTrigger("PlayerDead");
        */
    }
    /*
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject == player)
            playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject == player)
            playerInRange = false;
    }
    */
    protected virtual void ContactPlayer()
    {
        moving = false;
    }

    protected virtual void Attack()
    {
        enemyAudio.Play();
        timer = 0f;
    }

    protected void TurnToDestination()
    {
        if (player.transform.position.x - transform.position.x > 0) // 플레이어가 오른쪽에
            anim.SetBool("direction", true);
        else
            anim.SetBool("direction", false); // 플레이어가 왼쪽에
    }

    protected void MoveToDestination()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    }

    float GetDistanceFromPlayer()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        return distance;
    }
}
