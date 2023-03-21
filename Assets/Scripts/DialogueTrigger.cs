using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("DialogueTrigger")]
    public bool StartDialogueOnAwake = false;
    public TextAsset DialogueScript;
    public CinemachineVirtualCamera[] ObjectsToActivate;

    GameObject ui;

    // Start is called before the first frame update
    void Start()
    {
        if (StartDialogueOnAwake)
        {
            StartDialogue();
        }
    }

    public void StartDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(this);
    }

    public void StartDialogue(GameObject uiToDisable)
    {
        ui = uiToDisable;
        ui.SetActive(false);
        FindObjectOfType<DialogueManager>().StartDialogue(this);
    }

    public virtual void EndDialogue(int choice)
    {
        if (ui != null)
        {
            ui.SetActive(true);
        }
        ui = null;
    }
}
