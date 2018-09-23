using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interface : MonoBehaviour {

    public GameObject inventory;
    private bool inventoryBtn;

	void Start () {
        inventory.SetActive(false);
        inventoryBtn = false;
	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.I))
        {
            inventoryBtn = !inventoryBtn;
        }

        if (!inventoryBtn)
            inventory.SetActive(false);
        else
            inventory.SetActive(true);
    }
}
