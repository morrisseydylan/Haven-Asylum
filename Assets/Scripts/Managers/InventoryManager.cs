using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryUI;
    public GameObject Slots;
    public GameObject ItemView;

    public GameObject ItemUI;

    PlayerInteraction interaction;
    bool itemView = false;
    Quaternion savedRotation;

    // Start is called before the first frame update
    void Start()
    {
        interaction = GetComponent<PlayerInteraction>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.timeScale != 0)
        {
            InventoryUI.SetActive(!InventoryUI.activeSelf);
            interaction.enabled = !interaction.enabled;
            if (DataManager.Instance.InventoryItems.Count > 0)
            {
                ItemUI.SetActive(true);
            }
        }

        if (itemView)
        {
            Vector3 currentRotation = ItemUI.transform.rotation.eulerAngles;
            currentRotation.y += Time.deltaTime * 20.0f;
            ItemUI.transform.rotation = Quaternion.Euler(currentRotation);
        }
    }

    public void BeginItemView()
    {
        savedRotation = ItemUI.transform.rotation;
        itemView = true;
        ItemView.SetActive(true);
        Slots.SetActive(false);
    }

    public void EndItemView()
    {
        itemView = false;
        ItemView.SetActive(false);
        Slots.SetActive(true);
        ItemUI.transform.rotation = savedRotation;
    }

    public void ReadItem()
    {
        ItemUI.GetComponent<DialogueTrigger>().StartDialogue(InventoryUI);
    }
}
