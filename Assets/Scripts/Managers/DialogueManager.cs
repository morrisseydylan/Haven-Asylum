using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Linq;
using Unity.VisualScripting;
using Cinemachine;

public class DialogueManager : MonoBehaviour
{
    public GameObject DialogueUI;
    public CinemachineVirtualCamera DialogueCamera;
    public GameObject Party; // indices 0-3 respectively: Keith, Rebecca, Andrea, Nick
    public CutsceneManager Manager;

    TMP_Text[] textBoxes; // index 0 = NPC dialogue text box; indices 1-3 = choice text boxes

    Queue<string> dialogueQueue = new();
    bool choicePrompt = false;
    bool multipleResponses = false;
    bool isPrinting = false;
    bool instantPrint = false;
    int choice;

    // Start is called before the first frame update
    void Start()
    {
        textBoxes = DialogueUI.GetComponentsInChildren<TMP_Text>();
    }

    public void OnDialogueClick(int choice) // 0 = NPC dialogue box clicked; 1-3 = choice box clicked
    {
        if (isPrinting) // If the current line is still printing, instantly print the rest of the current line
        {
            instantPrint = true;
            return;
        }

        if (choicePrompt)
        {
            if (choice == 0) // Don't proceed to next dialogue if the player is prompted with a choice but doesn't select one
            {
                return;
            }

            this.choice = choice;
            choicePrompt = false;
        }

        PrintDialogue();
    }

    public void StartDialogue(TextAsset textFile)
    {
        string delimiter = String.Concat(Environment.NewLine, Environment.NewLine); // Text file will be split by double newlines
        string[] lines = textFile.text.Split(delimiter, StringSplitOptions.RemoveEmptyEntries); // Split text file and remove empties
        foreach (string lineGroup in lines)
        {
            dialogueQueue.Enqueue(lineGroup); // Add each bit of dialogue to the queue
        }

        DialogueUI.SetActive(true);
        PrintDialogue();
    }

    void PrintDialogue()
    {
        if (dialogueQueue.Any()) // If dialogue queue is not empty, start printing
        {
            string[] lines = dialogueQueue.Dequeue().Split(Environment.NewLine);

            if (lines.Length == 1)
            {
                string line = lines[0];
                if (line.Contains("[1]"))
                {
                    if (choice == 1)
                    {

                    }
                }
                else if (line.Contains("[2]"))
                {
                    if (choice == 2)
                    {

                    }
                }
                else if (line.Contains("[3]"))
                {
                    if (choice == 3)
                    {

                    }
                }
                else if (line.Contains("[RESUME]"))
                {
                    PrintDialogue();
                }
            }
            if (lines[0].Contains(":"))
            {
                string speaker = lines[0].Substring(0, lines[0].IndexOf(":"));
                int partyMember;
                switch (speaker)
                {
                    case "Keith":
                        {
                            partyMember = 0;
                            break;
                        }
                    case "Rebecca":
                        {
                            partyMember = 1;
                            break;
                        }
                    case "Andrea":
                        {
                            partyMember = 2;
                            break;
                        }
                    case "Nick":
                        {
                            partyMember = 3;
                            break;
                        }
                    default:
                        {
                            partyMember = 4;
                            break;
                        }
                }
                DialogueCamera.LookAt = Party.transform.GetChild(partyMember).transform;
            }

            // Don't show any choices on the screen by default
            for (int i = 1; i < DialogueUI.transform.childCount; i++)
            {
                DialogueUI.transform.GetChild(i).gameObject.SetActive(false);
            }

            // Determine what type of dialogue is being read (choice prompt, different dialogues, or neither)
            if (lines.Length == 1)
            {
                choicePrompt = false;
                if (lines[0].Contains("["))
                {
                    multipleResponses = true;
                }
            }
            else
            {
                choicePrompt = true;
            }

            // Print the line of dialogue
            StartCoroutine(PrintCharacters(lines));
        }
        else // If empty, end dialogue
        {
            DialogueUI.SetActive(false);
            Manager.DialogueFinish(choice);
        }
    }

    // Print the string one character at a time
    IEnumerator PrintCharacters(string[] lines)
    {
        string npcDialogue;
        TMP_Text npcTextBox = textBoxes[0];
        npcTextBox.text = "";
        isPrinting = true;

        // Choose which line will be printed
        if (multipleResponses)
        {
            npcDialogue = lines[choice - 1][3..];
        }
        else
        {
            npcDialogue = lines[0];
        }

        // Print characters to text box individually
        int charIndex = 0;
        while (!instantPrint && (charIndex < npcDialogue.Length))
        {
            npcTextBox.text += npcDialogue[charIndex];
            charIndex++;
            yield return new WaitForSeconds(0.01f);
        }

        npcTextBox.text = npcDialogue;
        isPrinting = false;
        instantPrint = false;

        // After printing, activate choice boxes if applicable
        if (choicePrompt)
        {
            for (int i = 1; i < lines.Length; i++)
            {
                DialogueUI.transform.GetChild(i).gameObject.SetActive(true);
                textBoxes[i].text = lines[i][(lines[i].IndexOf("]") + 1)..];
            }
        }
    }
}
