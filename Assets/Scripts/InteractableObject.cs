using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : DialogueTrigger
{
    [Header("InteractableObject")]
    public bool CanMoveTo = false;
    public int InventoryItemID = -1; // -1 = not an item; 0+ = an item that can be added to the player's inventory
    public string[] ScenesToLoad;

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
                Destroy(gameObject);
            }
        }

        if (ScenesToLoad.Length > 0)
        {
            if (ScenesToLoad[choice - 1] != "")
            {
                FindObjectOfType<FadeCamera>().FadeOut(ScenesToLoad[choice - 1]);
            }
        }
    }
}
