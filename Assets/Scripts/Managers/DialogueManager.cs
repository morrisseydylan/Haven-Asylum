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
    public TextAsset TextFile; // Debugging only--TextAssets should be argued in the StartDialogue method, called elsewhere
    public GameObject DialogueUI;
    public CinemachineVirtualCamera PlayerCamera;

    TMP_Text[] textBoxes; // index 0 = NPC dialogue text box; indices 1-3 = choice text boxes
    CinemachinePOV pov;

    Queue<string> dialogueQueue = new();
    bool isChoice = false;
    bool isPrinting = false;
    bool instantPrint = false;

    // Start is called before the first frame update
    void Start()
    {
        textBoxes = DialogueUI.GetComponentsInChildren<TMP_Text>();
        pov = (CinemachinePOV)PlayerCamera.GetCinemachineComponent(CinemachineCore.Stage.Aim);
    }

    // Debugging only
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartDialogue(TextFile);
        }
    }

    public void OnDialogueClick(int choice) // 0 = NPC dialogue box clicked; 1-3 = choice box clicked
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

            // TODO: send choice value somewhere to be stored
            isChoice = false;
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
            dialogueQueue.Enqueue(lineGroup); // Add each bit of dialogue to the queue
        }

        DialogueUI.SetActive(true);
        pov.m_HorizontalAxis.m_MaxSpeed = 0;
        pov.m_VerticalAxis.m_MaxSpeed = 0;
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
            pov.m_HorizontalAxis.m_MaxSpeed = 300; // Debugging only--should be configurable by the player
            pov.m_VerticalAxis.m_MaxSpeed = 300;
        }
    }

    // Takes in an array of one or more strings. The first string will always be the NPC dialogue;
    // any additional strings will be player choices, if applicable
    IEnumerator PrintCharacters(string[] lines)
    {
        string npcDialogue = lines[0];
        TMP_Text npcTextBox = textBoxes[0];
        npcTextBox.text = "";
        isPrinting = true;

        // Don't show any choices on the screen by default
        for (int i = 1; i < DialogueUI.transform.childCount; i++)
        {
            DialogueUI.transform.GetChild(i).gameObject.SetActive(false);
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

        if (lines.Length == 1)
        {
            isChoice = false;
        }
        else
        {
            isChoice = true;

            // Activate choice boxes
            for (int i = 1; i < lines.Length; i++)
            {
                DialogueUI.transform.GetChild(i).gameObject.SetActive(true);
                textBoxes[i].text = lines[i][(lines[i].IndexOf("]") + 1)..];
            }
        }
    }
}
