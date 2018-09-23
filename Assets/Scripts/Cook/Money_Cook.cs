using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money_Cook : MonoBehaviour {

    ItemManager itemManager;
    Text text;
	
	void Awake () {
        itemManager = FindObjectOfType<ItemManager>();
        text = GetComponent<Text>();
	}
	
	
	void Update () {
        text.text = "" + itemManager.money;
	}
}
