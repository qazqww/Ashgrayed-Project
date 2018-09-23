using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour{

    public ItemManager itemManager;

    private string itemName;
    private int amount;

    Text text;

	void Start () {
        itemName = gameObject.name;
        text = GetComponentInChildren<Text>();
    }

	void Update () {
		switch(itemName)
        {
            case "PotionSlot":
                amount = itemManager.potion;
                text.text = "" + amount;
                break;
            case "Money":
                amount = itemManager.money;
                text.text = "" + amount;
                break;
            case "SlimeSlot":
                amount = itemManager.slime;
                text.text = "x " + amount;
                break;
            case "FairySlot":
                amount = itemManager.fairy;
                text.text = "x " + amount;
                break;
            case "FireSlot":
                amount = itemManager.fire;
                text.text = "x " + amount;
                break;
            case "BansheeSlot":
                amount = itemManager.banshee;
                text.text = "x " + amount;
                break;
            case "SlimeSpiceSlot":
                amount = itemManager.slimeSpice;
                text.text = "x " + amount;
                break;
            case "FairySpiceSlot":
                amount = itemManager.fairySpice;
                text.text = "x " + amount;
                break;
            case "FireSpiceSlot":
                amount = itemManager.fireSpice;
                text.text = "x " + amount;
                break;
            case "BansheeSpiceSlot":
                amount = itemManager.bansSpice;
                text.text = "x " + amount;
                break;
        }
	}
}
