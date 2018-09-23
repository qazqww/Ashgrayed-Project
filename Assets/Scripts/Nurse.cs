using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nurse : MonoBehaviour {

    ItemManager itemManager;
    bool canBuy;

    public Dialog dialog;

    void Awake () {
        itemManager = FindObjectOfType<ItemManager>();
        canBuy = false;
	}

    void Update()
    {
        if (canBuy && Input.GetKeyDown(KeyCode.R))
        {
            if (itemManager.money >= 50)
            {
                itemManager.ItemBuySound();
                itemManager.money -= 50;
                itemManager.potion++;
            }
        }
    }

    public void TriggerDialog()
    {
        FindObjectOfType<NPCDialogManager>().StartDialog(dialog);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            TriggerDialog();

            canBuy = true;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            FindObjectOfType<NPCDialogManager>().EndDialog();
            canBuy = false;
        }
    }
}
