using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryUI;
    public GameObject Slots;
    public GameObject ItemsUI;

    public GameObject ItemView;
    public RawImage RotatingImage;
    public TMP_Text FlavorText;
    public GameObject ReadButton;
    public ItemLibrary itemLibrary;

    public GameObject ItemModels;

    PlayerInteraction interaction;
    bool itemView = false;
    GameObject currentItem;
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
            if (interaction.enabled)
            {
                interaction.Disable();
            }
            else
            {
                interaction.Enable();
            }
            foreach (int i in DataManager.Instance.InventoryItems)
            {
                ItemsUI.transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        if (itemView)
        {
            Vector3 currentRotation = currentItem.transform.rotation.eulerAngles;
            currentRotation.y += Time.deltaTime * 20.0f;
            currentItem.transform.rotation = Quaternion.Euler(currentRotation);
        }
    }

    public void BeginItemView(int id)
    {
        currentItem = ItemModels.transform.GetChild(id).gameObject;
        savedRotation = currentItem.transform.rotation;
        itemView = true;
        ItemView.SetActive(true);
        Slots.SetActive(false);

        RotatingImage.texture = itemLibrary.itemLibrary[id].renderTexture;
        FlavorText.text = itemLibrary.itemLibrary[id].flavorText;
        ReadButton.SetActive(currentItem.GetComponent<DialogueTrigger>() != null);
    }

    public void EndItemView()
    {
        itemView = false;
        ItemView.SetActive(false);
        Slots.SetActive(true);
        currentItem.transform.rotation = savedRotation;
    }

    public void ReadItem()
    {
        currentItem.GetComponent<DialogueTrigger>().StartDialogue(InventoryUI);
    }
}
