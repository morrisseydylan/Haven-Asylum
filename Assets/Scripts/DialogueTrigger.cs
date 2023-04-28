using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("DialogueTrigger")]
    public bool StartDialogueOnAwake = false;
    public TextAsset[] DialogueScript;
    public int ChoiceForProgression = -1;
    public CinemachineVirtualCamera[] ObjectsToActivate;
    public GameObject[] ScriptsToRun;
    public string NextScene;

    protected DialogueManager manager;
    protected int index = 0;
    GameObject ui;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<DialogueManager>();
        if (StartDialogueOnAwake)
        {
            StartDialogue(null);
        }
    }

    public void StartDialogue(GameObject uiToDisable)
    {
        if (DialogueScript.Length == 0)
        {
            return;
        }

        ui = uiToDisable;
        if (ui != null)
        {
            ui.SetActive(false);
        }
        manager.StartDialogue(this);
    }

    public string GetDialogue()
    {
        return DialogueScript[index].text;
    }

    public virtual void EndDialogue(int choice)
    {
        if (ui != null)
        {
            ui.SetActive(true);
        }
        ui = null;
        if (++index < DialogueScript.Length)
        {
            if (ChoiceForProgression == choice || ChoiceForProgression == -1)
            {
                manager.StartDialogue(this);
            }
            return;
        }

        index = 0;

        foreach (CinemachineVirtualCamera cm in ObjectsToActivate)
        {
            cm.gameObject.SetActive(false);
        }

        if (NextScene != "")
        {
            FindObjectOfType<FadeCamera>().FadeOut(NextScene);
        }
    }
}
