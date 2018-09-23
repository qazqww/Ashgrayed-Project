using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class DialogManager : MonoBehaviour {

    public Text nameText;
    public Text dialogText;
    public GameObject whosTimeline; // 후행 타임라인
    public Animator anim;

    DialogTrigger2 dialogTrigger;
    PlayableDirector pd;
    PlayerMovement playerMovement;
    PlayerTemp playerTemp;
    float timer;
    bool timerSet;
    bool button;

    private Queue<string> sentences;

	void Start () {
        pd = whosTimeline.GetComponent<PlayableDirector>();
        try { playerMovement = FindObjectOfType<PlayerMovement>(); }
        catch {  }
        try { playerTemp = FindObjectOfType<PlayerTemp>(); }
        catch { }

        dialogTrigger = FindObjectOfType<DialogTrigger2>();
        sentences = new Queue<string>();
        timer = 0f;
        timerSet = false;
        button = false;
	}
	
	void Update () {
        if (timerSet)
            timer += Time.deltaTime;

        if (timer >= 0.5f)
            PlayNextTimeline();
	}
    
    public void StartDialog (Dialog dialog)
    {
        anim.SetBool("IsOpen", true);
        try { playerMovement.enabled = false; }
        catch {  }
        try { playerTemp.enabled = false; }
        catch { }

        Debug.Log("Starting conversation with " + dialog.name);

        nameText.text = dialog.name;

        sentences.Clear();

        foreach (string sentence in dialog.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return null;
        }
    }

    void EndDialog()
    {
        anim.SetBool("IsOpen", false);
        try { playerMovement.enabled = true;}
        catch {  }
        try { playerTemp.enabled = true; }
        catch { }
        timerSet = true;
        if (button)
        {
            dialogTrigger.GoNextScene();
            button = false;
        }
        button = true;
    }

    void PlayNextTimeline()
    {
        timer = 0f;
        if (pd != null)
            pd.Play();
    }
}
