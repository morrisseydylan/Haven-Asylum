using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    GameObject interaction;

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
                return;
            }
        }
        SetOutline(false);
    }

    void SetOutline(bool outline)
    {
        if (interaction != null)
        {
            interaction.GetComponent<Outline>().enabled = outline;
        }
    }
}
