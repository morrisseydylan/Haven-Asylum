using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject DialogueUI;
    public TMP_Text TextBox;
    public TextAsset TextFile;

    private Queue<string> inputStream = new(); // stores dialogue
    private bool isTyping = false;
    private bool cancelTyping = false;

    private void Start()
    {
        StartDialogue();
    }

    private void Update()
    {
        if (DialogueUI.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            PrintDialogue();
        }
    }

    public void StartDialogue()
    {
        string txt = TextFile.text;

        string[] lines = txt.Split(System.Environment.NewLine.ToCharArray()); // Split dialogue lines by newline

        foreach (string line in lines) // for every line of dialogue
        {
            inputStream.Enqueue(line); // adds to the dialogue to be printed
        }

        inputStream.Enqueue("EndQueue");

        DialogueUI.SetActive(true);

        PrintDialogue(); // Prints out the first line of dialogue
    }

    private void PrintDialogue()
    {
        if (inputStream.Peek().Contains("EndQueue")) // special phrase to stop dialogue
        {
            // Clear Queue
            if (!isTyping)
            {
                inputStream.Dequeue();
                EndDialogue();
            }
            else
            {
                cancelTyping = true;
            }
        }
        else
        {
            if (!isTyping)
            {
                string textString = inputStream.Dequeue();
                StartCoroutine(TextScroll(textString));
            }
            else if (isTyping && !cancelTyping)
            {
                cancelTyping = true;
            }
        }


    }

    private IEnumerator TextScroll(string lineOfText)
    {
        int letter = 0;
        TextBox.text = "";
        isTyping = true;
        cancelTyping = false;
        while (isTyping && !cancelTyping && (letter < lineOfText.Length - 1))
        {
            TextBox.text += lineOfText[letter];
            letter++;
            yield return new WaitForSeconds(0.01f);
        }

        TextBox.text = lineOfText;
        isTyping = false;
        cancelTyping = false;
    }

    public void EndDialogue()
    {
        TextBox.text = "";
        inputStream.Clear();
        DialogueUI.SetActive(false);

        cancelTyping = false;
        isTyping = false;
        inputStream.Clear();
    }
}
