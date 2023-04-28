using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : DialogueTrigger
{
    [Header("InteractableObject")]
    public int Area = 0;
    public bool DisableWhileInArea = false;
    public InteractableObject[] ObjectsInArea;
    public GameObject PlayerLocation;
    public int InventoryItemID = -1; // -1 = not an item; 0+ = an item that can be added to the player's inventory
    public GameObject InventoryItemMesh;
    public bool Proceed = true;

    public override void EndDialogue(int choice)
    {
        if (InventoryItemID != -1)
        {
            if (choice == 2) // Yes
            {
                DataManager.Instance.InventoryItems.Add(InventoryItemID);
                if (TryGetComponent(out AudioSource audio))
                {
                    audio.Play();
                }
                if (InventoryItemMesh == null)
                {
                    Destroy(gameObject);
                }
                else
                {

                    InventoryItemMesh.SetActive(false);
                }
            }
            if (choice == 1)
            {
                return;
            }
        }

        if (choice == 5)
        {
            return;
        }

        if (!Proceed)
        {
            index++;
            return;
        }

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
