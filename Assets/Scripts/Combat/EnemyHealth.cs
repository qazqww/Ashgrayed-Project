using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    //public AudioClip deathClip;
    public GameObject item;

    //Animator anim;
    //AudioSource enemyAudio;
    //public ParticleSystem hitParticles;
    BoxCollider2D box2d;
    bool isDead;
    bool isSinking;
    float percent;

    void Awake()
    {
        //enemyAudio = GetComponent<AudioSource>();
        //hitParticles = GetComponentInChildren<ParticleSystem>();
        box2d = GetComponent<BoxCollider2D>();

        currentHealth = startingHealth;
        percent = (float)Random.Range(0, 100);

        //hitParticles.Stop();
    }


    void Update()
    {
        if (isSinking)
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
    }


    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (isDead)
            return;

        currentHealth -= amount;

        //.transform.position = hitPoint;
        //hitParticles.Play();

        if (currentHealth <= 0)
            Death();
    }
    /*
    public void ShowHitEffect()
    {
        hitParticles.Play();
    }
    */
    void Death()
    {
        isDead = true;

        box2d.isTrigger = true;

        //anim.SetTrigger("Dead");
        Destroy(gameObject);

        //enemyAudio.clip = deathClip;
        //enemyAudio.Play();

        if (percent%2 == 0)
            ItemDrop();
    }


    public void StartSinking()
    {
        isSinking = true;
        Destroy(gameObject);
    }

    void ItemDrop()
    {
        Instantiate(item, transform.position, Quaternion.identity);
    }
}
