using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialog : MonoBehaviour {

    public Dialog dialog;

    public void TriggerDialog()
    {
        FindObjectOfType<NPCDialogManager>().StartDialog(dialog);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
            TriggerDialog();
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
            FindObjectOfType<NPCDialogManager>().EndDialog();
    }
}
