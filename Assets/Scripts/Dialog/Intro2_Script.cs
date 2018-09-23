using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Intro2_Script : MonoBehaviour {

    public GameObject whosTimeline;
    public PlayerHealth playerHealth;
    public GameObject keys;

    PlayableDirector pd;

    bool button;

    void Start () {
        pd = whosTimeline.GetComponent<PlayableDirector>();
        button = false;
	}
	
	void Update () {
        if (playerHealth.currentHealth < 20)
            playerHealth.currentHealth = 20;
	}

    private void OnTriggerStay2D(Collider2D coll)
    {
        EnemyHealth enemyHealth = (EnemyHealth)FindObjectOfType(typeof(EnemyHealth));
        if (!enemyHealth)
            NextScene();
    }

    void NextScene()
    {
        Debug.Log("DDD");
        keys.SetActive(false);
        if (pd != null && button == false)
        {
            pd.Play();
            button = true;
        }
    }
}
