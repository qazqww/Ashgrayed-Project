using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour {

    // 대화 내용
    public Dialog dialog;

    public void TriggerDialog()
    {
        // DialogManager가 위의 내용을 읽도록 함.
        FindObjectOfType<DialogManager>().StartDialog(dialog);
    }
}
