using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {

    public int potion;
    public int slime;
    public int fairy;
    public int fire;
    public int banshee;
    public int slimeSpice;
    public int fairySpice;
    public int fireSpice;
    public int bansSpice;
    public int money;

    bool button;

    public AudioClip itemGetClip;
    public AudioClip itemBuyClip;

    GameObject other;
    AudioSource playerAudio;

    void Start () {
        potion = 2;
        slime = 2;
        fairy = 2;
        fire = 2;
        banshee = 2;
        slimeSpice = 0;
        fairySpice = 0;
        fireSpice = 0;
        bansSpice = 0;
        money = 100;
        button = false;

        playerAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && button)
        {
            playerAudio.clip = itemGetClip;
            playerAudio.Play();

            switch (other.name)
            {
                case "SlimeItem":
                case "SlimeItem(Clone)":
                    slime++;
                    Destroy(other);
                    break;
                case "FairyItem":
                case "FairyItem(Clone)":
                    fairy++;
                    Destroy(other);
                    break;
                case "FireItem":
                case "FireItem(Clone)":
                    fire++;
                    Destroy(other);
                    break;
                case "BansheeItem":
                case "BansheeItem(Clone)":
                    banshee++;
                    Destroy(other);
                    break;
            }
        }
    }

    public void ItemBuySound()
    {
        playerAudio.clip = itemBuyClip;
        playerAudio.Play();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Item")
        {
            other = coll.gameObject;
            button = true;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Item")
        {
            other = null;
            button = false;
        }
    }
}
