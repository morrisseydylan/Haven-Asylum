using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Linq;

public class DialogueManager : MonoBehaviour
{
    public GameObject DialogueUI;
    public TMP_Text TextBox;
    public TextAsset TextFile;

    Queue<string> dialogue = new();
    bool isPrinting = false;
    bool instantPrint = false;

    // Start is called before the first frame update
    void Start()
    {
        StartDialogue(TextFile);
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogueUI.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            PrintDialogue();
        }
    }

    public void StartDialogue(TextAsset textFile)
    {
        string[] lines = textFile.text.Split(Environment.NewLine.ToCharArray()); // Split text file by newlines
        foreach (string line in lines)
        {
            if (!line.Any())
            {
                continue; // Skip empty lines
            }
            dialogue.Enqueue(line); // Add the line of dialogue to the queue
        }

        DialogueUI.SetActive(true);
        PrintDialogue();
    }

    void PrintDialogue()
    {
        if (isPrinting) // If user gives input to print next line but current line is still printing...
        {
            instantPrint = true; // ...instantly print the rest of the current line
        }
        else if (dialogue.Any()) // If dialogue queue is not empty, start printing
        {
            StartCoroutine(TextScroll(dialogue.Dequeue()));
        }
        else // If empty, end dialogue
        {
            DialogueUI.SetActive(false);
            TextBox.text = "";
        }
    }

    IEnumerator TextScroll(string line) // Coroutine for printing each character individually
    {
        TextBox.text = "";
        isPrinting = true;

        int charIndex = 0;
        while (!instantPrint && (charIndex < line.Length - 1))
        {
            TextBox.text += line[charIndex];
            charIndex++;
            yield return new WaitForSeconds(0.01f);
        }

        TextBox.text = line;
        isPrinting = false;
        instantPrint = false;
    }
}
