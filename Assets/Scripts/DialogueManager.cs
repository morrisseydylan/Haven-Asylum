using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Linq;
using Unity.VisualScripting;

public class DialogueManager : MonoBehaviour
{
    public TextAsset TextFile; // Prototype only; TextAssets should be argued in the StartDialogue method, called elsewhere

    public GameObject DialogueUI;
    public GameObject ChoicesUI;

    TMP_Text dialogueText;
    Queue<string> dialogueQueue = new();
    bool isChoice = false;
    bool isPrinting = false;
    bool instantPrint = false;

    // Start is called before the first frame update
    void Start()
    {
        dialogueText = DialogueUI.GetComponentInChildren<TMP_Text>();
        StartDialogue(TextFile); // Prototype only
    }

    public void OnDialogueClick(int choice) // 0 = dialogue box clicked; 1-3 = choice clicked
    {
        if (isPrinting) // If the current line is still printing, instantly print the rest of the current line
        {
            instantPrint = true;
            return;
        }

        if (isChoice)
        {
            if (choice == 0) // Don't proceed to next dialogue if the player is prompted with a choice but doesn't select one
            {
                return;
            }

            isChoice = false;
            ChoicesUI.SetActive(false); // Remove previous choices from the screen
            for (int i = 0; i < ChoicesUI.transform.childCount; i++)
            {
                ChoicesUI.transform.GetChild(i).gameObject.SetActive(false);
            }

            // TODO: send choice value somewhere to be stored
        }

        PrintDialogue();
    }

    public void StartDialogue(TextAsset textFile)
    {
        // Example text file format:

        /* Hello! This is the first line of dialogue to be displayed.
         * 
         * This text will appear second, after a click.
         * 
         * This text will appear third. Some choices will also appear.
         * [CHOICE]This is the bottom choice.
         * [CHOICE]This is the middle choice.
         * [CHOICE]This is the top choice.
         * 
         * Finally, this text will appear last.*/

        string delimiter = String.Concat(Environment.NewLine, Environment.NewLine); // Text file will be split by double newlines
        string[] lines = textFile.text.Split(delimiter, StringSplitOptions.RemoveEmptyEntries); // Split text file and remove empties
        foreach (string lineGroup in lines)
        {
            dialogueQueue.Enqueue(lineGroup); // Add each line of dialogue to the queue
        }

        DialogueUI.SetActive(true);
        PrintDialogue();
    }

    void PrintDialogue()
    {

        if (dialogueQueue.Any()) // If dialogue queue is not empty, start printing
        {
            string[] lines = dialogueQueue.Dequeue().Split(Environment.NewLine);
            StartCoroutine(PrintCharacters(lines));
        }
        else // If empty, end dialogue
        {
            DialogueUI.SetActive(false);
        }
    }

    IEnumerator PrintCharacters(string[] lines) // Coroutine for printing each character individually
    {
        dialogueText.text = "";
        isPrinting = true;

        int charIndex = 0;
        while (!instantPrint && (charIndex < lines[0].Length))
        {
            dialogueText.text += lines[0][charIndex];
            charIndex++;
            yield return new WaitForSeconds(0.01f);
        }

        dialogueText.text = lines[0];
        isPrinting = false;
        instantPrint = false;

        if (lines.Length == 1)
        {
            isChoice = false;
        }
        else
        {
            isChoice = true;
            ChoicesUI.SetActive(true);
            for (int i = 1; i < lines.Length; i++)
            {
                GameObject choiceButton = ChoicesUI.transform.GetChild(i - 1).gameObject;
                choiceButton.SetActive(true);
                choiceButton.GetComponentInChildren<TMP_Text>().text = lines[i][(lines[i].IndexOf("]") + 1)..];
            }
        }
    }
}
