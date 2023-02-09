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
    [Header("Empty GameObject containing all dialogue & choice UI goes here.\n" +
        "Children must be named:\n" +
        "Dialogue Text\n" +
        "Choice (1)\n" +
        " > Choice (1) Text\n" +
        "etc.")]

    public TextAsset TextFile; // Prototype only; TextAssets should be argued in the StartDialogue method, called elsewhere

    private TMP_Text dialogueText;

    private GameObject choice1UI;
    private TMP_Text choice1Text;

    private GameObject choice2UI;
    private TMP_Text choice2Text;

    Queue<string> dialogue = new();
    bool isPrinting = false;
    bool instantPrint = false;

    // Start is called before the first frame update
    void Start()
    {
        choice1UI = DialogueUI.transform.Find("Choice (1)").gameObject;
        choice2UI = DialogueUI.transform.Find("Choice (2)").gameObject;
        choice1Text = choice1UI.transform.Find("Choice (1) Text").GetComponent<TMP_Text>();
        choice2Text = choice2UI.transform.Find("Choice (2) Text").GetComponent<TMP_Text>();
        dialogueText = DialogueUI.transform.Find("Dialogue Text").GetComponent<TMP_Text>();
        StartDialogue(TextFile); // Prototype only
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogueUI.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            if (isPrinting) // If the current line is still printing, instantly print the rest of the current line
            {
                instantPrint = true;
            }
            else
            {
                PrintDialogue();
            }
        }
    }

    public void StartDialogue(TextAsset textFile)
    {
        string delimiter = String.Concat(Environment.NewLine, Environment.NewLine); // Text file will be split by double newlines
        string[] lines = textFile.text.Split(delimiter, StringSplitOptions.RemoveEmptyEntries); // Split text file and remove empties
        foreach (string line in lines)
        {
            dialogue.Enqueue(line); // Add each line of dialogue to the queue
        }

        DialogueUI.SetActive(true);
        PrintDialogue();
    }

    void PrintDialogue()
    {
        if (dialogue.Any()) // If dialogue queue is not empty, start printing
        {
            //StartCoroutine(TextScroll(dialogue.Dequeue()));
            string line = dialogue.Dequeue();
            if (line.Contains("[CHOICE#"))
            {
                string[] lines = line.Split(Environment.NewLine);
                StartCoroutine(TextScroll(lines[0]));
                choice1UI.SetActive(true);
                choice2UI.SetActive(true);
                choice1Text.text = lines[1].Substring(lines[1].IndexOf("]") + 1);
                choice2Text.text = lines[2].Substring(lines[2].IndexOf("]") + 1);
            }
            else
            {
                StartCoroutine(TextScroll(line));
            }
        }
        else // If empty, end dialogue
        {
            DialogueUI.SetActive(false);
            dialogueText.text = "";
        }
    }

    IEnumerator TextScroll(string line) // Coroutine for printing each character individually
    {
        dialogueText.text = "";
        isPrinting = true;

        int charIndex = 0;
        while (!instantPrint && (charIndex < line.Length - 1))
        {
            dialogueText.text += line[charIndex];
            charIndex++;
            yield return new WaitForSeconds(0.01f);
        }

        dialogueText.text = line;
        isPrinting = false;
        instantPrint = false;
    }
}
