using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiceCount : MonoBehaviour {

    ItemManager itemManager;
    Text text;

	// Use this for initialization
	void Start () {
        itemManager = FindObjectOfType<ItemManager>();
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		switch(gameObject.name)
        {
            case "spice_slime":
                text.text = "x " + itemManager.slimeSpice;
                break;
            case "spice_fairy":
                text.text = "x " + itemManager.fairySpice;
                break;
            case "spice_fire":
                text.text = "x " + itemManager.fireSpice;
                break;
            case "spice_banshee":
                text.text = "x " + itemManager.bansSpice;
                break;
        }
	}
}
