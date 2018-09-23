using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMap : MonoBehaviour {

    [SerializeField]
    private string levelToLoad;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(levelToLoad, LoadSceneMode.Single);
        }
    }
}
