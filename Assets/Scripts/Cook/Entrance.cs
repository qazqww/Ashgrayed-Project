using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour {

    public float timer;
    private float enterTime;

    public AudioSource bell;
    SpriteRenderer render;
    Animator anim;

	void Start () {
        enterTime = Random.Range(10f, 75f);
        render = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        render.enabled = false;
        anim.enabled = false;
	}
	
	void Update () {
        timer += Time.deltaTime;
        render.enabled = false;
        anim.enabled = false;
        if (timer >= enterTime)
        {
            bell.Play();
            timer = 0f;
            render.enabled = true;
            anim.enabled = true;
            this.enabled = false;
        }
	}
}
