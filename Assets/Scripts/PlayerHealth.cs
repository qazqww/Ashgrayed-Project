using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    public int MaxHealth = 300;
    public int currentHealth;
    //public Slider healthSlider;
    public Image health;
    public Image damageImage;
    public Image deathImage;
    public AudioClip hurtClip;
    public AudioClip deathClip;
    public AudioClip potionClip;
    public Animator deathFader;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    public float flashSpeed = 5f;
    public float restartDelay = 5f;

    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    ItemManager itemManager;
    bool isDead;
    bool damaged;
    float restartTimer;
    float currentHealth_Real;

    void Awake()
    {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        itemManager = GetComponent<ItemManager>();
        currentHealth = MaxHealth;
        isDead = false;
    }

    void Update ()
    {
        Debug.Log(isDead);
        currentHealth_Real = (float)currentHealth / 300;
        health.fillAmount = currentHealth_Real;

        if (damaged)
            damageImage.color = flashColour;
        else
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        damaged = false;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if(itemManager.potion > 0)
            {
                playerAudio.clip = potionClip;
                playerAudio.Play();
                
                itemManager.potion--;
                if (currentHealth <= 250)
                    currentHealth += 50;
                else
                    currentHealth = 300;

                //healthSlider.value = currentHealth;
            }
        }

        if (isDead)
        {
            restartTimer += Time.deltaTime;
            if (restartTimer >= restartDelay)
            {
                Debug.Log("Restart");
                Rebirth();
            }
        }
    }

    public void TakeDamage(int amount)
    {
        damaged = true;
        currentHealth -= amount;
        //healthSlider.value = currentHealth;
        playerAudio.clip = hurtClip;
        playerAudio.Play();

        if (currentHealth <= 0 && !isDead)
            Death();
    }

    void Death()
    {
        isDead = true;

        if (itemManager.money >= 100)
            itemManager.money -= 100;
        else
            itemManager.money = 0;

        anim.SetTrigger("Die");
        deathFader.SetTrigger("GameOver");

        playerAudio.clip = deathClip;
        playerAudio.Play();
        
        playerMovement.enabled = false;
    }

    void Rebirth()
    {
        isDead = false;
        playerAudio.clip = hurtClip;
        playerMovement.enabled = true;
        restartTimer = 0f;

        transform.position = new Vector2(-16.5f, -26f);
        SceneManager.LoadScene("forest town", LoadSceneMode.Single);
        currentHealth = MaxHealth/3;
        //healthSlider.value = currentHealth;
    }
}
