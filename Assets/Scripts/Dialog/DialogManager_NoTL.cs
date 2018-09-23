using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class DialogManager_NoTL : MonoBehaviour
{

    public Text nameText;
    public Text dialogText;
    public Animator anim;
    private Queue<string> sentences;
    PlayerMovement playerMovement;

    void Start()
    {
        sentences = new Queue<string>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
    }

    public void StartDialog(Dialog dialog)
    {
        anim.SetBool("IsOpen", true);
        playerMovement.enabled = false;

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

    IEnumerator TypeSentence(string sentence)
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
        playerMovement.enabled = true;
    }
}
