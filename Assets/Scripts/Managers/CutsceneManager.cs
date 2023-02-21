using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public DialogueManager Manager;
    public TextAsset DialogueA;
    public TextAsset DialogueB1;
    public TextAsset DialogueB2;
    public TextAsset DialogueC;

    TextAsset currentDialogue;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayStart());
    }

    IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(5);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            yield break;
        }
        currentDialogue = DialogueA;
        StartDialogue();
    }

    public void DialogueFinish(int choice)
    {
        if (currentDialogue == DialogueA)
        {
            if (choice == 1)
            {
                currentDialogue = DialogueB1;
            }
            else
            {
                currentDialogue = DialogueB2;
            }
        }
        else if (currentDialogue == DialogueB1 || currentDialogue == DialogueB2)
        {
            currentDialogue = DialogueC;
        }
        else if (currentDialogue == DialogueC)
        {
            currentDialogue = null;
        }

        StartDialogue();
    }

    void StartDialogue()
    {
        if (currentDialogue != null)
        {
            Manager.StartDialogue(currentDialogue);
        }
    }
}
