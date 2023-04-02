using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject DialogueUI;
    public GameObject Player;
    public Image NPCBox;
    public Sprite[] NPCBoxSprites;

    DialogueTrigger trigger;
    PlayerInteraction interaction;
    bool interactionWasEnabled = false;
    TMP_Text[] textBoxes; // index 0 = NPC dialogue text box; indices 1-3 = choice text boxes
    GameObject[] partyMemberCameras = new GameObject[5]; // indices 0-3 respectively: Keith, Rebecca, Andrea, Nick; index 4 = player POV
    int currentCameraIndex;
    string playerName;
    bool isChoice = false;
    bool isPrinting = false;
    bool instantPrint = false;
    int choice = 0;

    readonly Queue<string> dialogueQueue = new();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            partyMemberCameras[i] = Player.transform.GetChild(i).gameObject;
        }
        interaction = partyMemberCameras[4].GetComponent<PlayerInteraction>();
        textBoxes = DialogueUI.GetComponentsInChildren<TMP_Text>();
        playerName = PlayerPrefs.GetString("name");
    }

    public void OnDialogueClick(int choice) // 0 = NPC dialogue box clicked; 1-3 = choice box clicked
    {
        if (isPrinting) // If the current line is still printing, stop the coroutine
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

            this.choice = choice;
            isChoice = false;
        }

        StartCoroutine(PopDialogueQueue());
    }

    public void StartDialogue(DialogueTrigger trigger)
    {
        this.trigger = trigger;
        string delimiter = String.Concat(Environment.NewLine, Environment.NewLine); // Text file will be split by double newlines
        string[] lines = trigger.GetDialogue().Split(delimiter, StringSplitOptions.RemoveEmptyEntries); // Split text file and remove empties
        for (int i = 0; i < lines.Length; i++)
        {
            lines[i] = lines[i].Replace("%PLAYER%", playerName);
            dialogueQueue.Enqueue(lines[i]); // Add each bit of dialogue to the queue
        }

        DialogueUI.SetActive(true);
        if (interaction.enabled)
        {
            interactionWasEnabled = true;
            interaction.enabled = false;
        }
        else
        {
            interactionWasEnabled = false;
        }
        StartCoroutine(PopDialogueQueue());
    }

    IEnumerator PopDialogueQueue()
    {
        if (dialogueQueue.Any()) // If dialogue queue is not empty, dequeue
        {
            // Most of the time only an array of size 1 will be dequeued;
            // however if there are any choices to prompt, they will be included in subsequent entries in the array
            string[] lines = dialogueQueue.Dequeue().Split(Environment.NewLine);
            string line = lines[0];

            // SKIP dialogue if it corresponds to a choice the player didn't make
            if ((line.Contains("[RESPONSE 1]") && choice != 1) || (line.Contains("[RESPONSE 2]") && choice != 2) || (line.Contains("[RESPONSE 3]") && choice != 3))
            {
                StartCoroutine(PopDialogueQueue());
                yield break;
            }

            // SKIP line if it is simply a command to activate an object in the scene
            if (line.Contains("[OBJECT"))
            {
                CinemachineVirtualCamera obj = trigger.ObjectsToActivate[int.Parse(line.Substring(8, 1)) - 1]; // Determine which CM to activate
                obj.gameObject.SetActive(true); // Activate the CM
                obj.LookAt.gameObject.SetActive(true); // Activate the object you are looking at
                DialogueUI.SetActive(false);
                yield return new WaitForSeconds(3);
                DialogueUI.SetActive(true);
                StartCoroutine(PopDialogueQueue());
                yield break;
            }

            // Determine if the dialogue is a choice prompt
            if (lines.Length == 1)
            {
                isChoice = false;
            }
            else
            {
                isChoice = true;
            }

            // Hide choice UI by default (if applicable, they will be activated after printing dialogue)
            for (int i = 1; i < DialogueUI.transform.childCount; i++)
            {
                DialogueUI.transform.GetChild(i).gameObject.SetActive(false);
            }

            // Face camera to speaker
            int partyMember;
            if (line.Contains("Keith:"))
            {
                partyMember = 0;
            }
            else if (line.Contains("Rebecca:"))
            {
                partyMember = 1;
            }
            else if (line.Contains("Andrea:"))
            {
                partyMember = 2;
            }
            else if (line.Contains("Nick:"))
            {
                partyMember = 3;
            }
            else
            {
                partyMember = -1;
                NPCBox.sprite = NPCBoxSprites[4];
            }
            if (partyMember != -1)
            {
                partyMemberCameras[currentCameraIndex].SetActive(false);
                partyMemberCameras[partyMember].SetActive(true);
                NPCBox.sprite = NPCBoxSprites[partyMember];
                currentCameraIndex = partyMember;
            }

            // Print dialogue
            StartCoroutine(PrintCharacters(lines));
        }
        else // If empty, end dialogue
        {
            DialogueUI.SetActive(false);
            partyMemberCameras[currentCameraIndex].SetActive(false);
            partyMemberCameras[4].SetActive(true);
            if (interactionWasEnabled)
            {
                interaction.enabled = true;
            }
            trigger.EndDialogue(choice);
        }
    }

    // Print the string one character at a time
    IEnumerator PrintCharacters(string[] lines)
    {
        string npcDialogue = lines[0];

        // Remove "[RESPONSE x] " from the string if applicable
        if (npcDialogue.Contains("[RESPONSE"))
        {
            npcDialogue = npcDialogue.Remove(0, 13);
        }

        // Print characters to text box individually
        TMP_Text npcTextBox = textBoxes[0];
        npcTextBox.text = "";
        isPrinting = true;
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
        if (isChoice)
        {
            for (int i = 1; i < lines.Length; i++)
            {
                DialogueUI.transform.GetChild(i).gameObject.SetActive(true);
                textBoxes[i].text = lines[i].Remove(0, 11); // Remove "[CHOICE x] " from the string
            }
        }
    }
}
