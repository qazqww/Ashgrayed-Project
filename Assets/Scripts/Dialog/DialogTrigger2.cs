using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogTrigger2 : MonoBehaviour {

    // 대화 내용
    public Dialog dialog;

    float timer;
    bool timerSet;

    void Start()
    {
        timer = 0f;
        timerSet = false;
    }

    void Update()
    {
        if (timerSet)
            timer += Time.deltaTime;

        if (timer >= 2f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            timerSet = false;
            timer = 0f;
        }
    }

    public void TriggerDialog2()
    {
        // DialogManager가 위의 내용을 읽도록 함.
        FindObjectOfType<DialogManager>().StartDialog(dialog);
    }

    public void GoNextScene()
    {
        timerSet = true;
    }
}
