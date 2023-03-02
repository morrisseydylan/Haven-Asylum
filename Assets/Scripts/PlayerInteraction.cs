using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    GameObject interaction;
    bool move = false;

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cursor = new(Screen.width / 2, Screen.height / 2, 0); // Debugging only--should be Input.mousePosition along with Cursor.lockState being changed elsewhere
        Ray ray = Camera.main.ScreenPointToRay(cursor);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider.gameObject.tag == "Interactable")
            {
                GameObject hitObj = hit.collider.gameObject;
                if (hitObj != interaction)
                {
                    SetOutline(false);
                }
                interaction = hitObj;
                SetOutline(true);
                if (Input.GetMouseButtonDown(0))
                {
                    move = true;
                }
            }
            else
            {
                SetOutline(false);
            }
        }
        else
        {
            SetOutline(false);
        }
        
        if (move)
        {
            if ((interaction.transform.position - transform.position).magnitude < 3.0f)
            {
                move = false;
            }
            transform.position = Vector3.MoveTowards(transform.position, interaction.transform.position, 0.05f);
        }
    }

    void SetOutline(bool value)
    {
        if (interaction != null)
        {
            Outline outline;
            if ((outline = interaction.GetComponent<Outline>()) != null)
            {
                outline.enabled = value;
            }
        }
    }
}
