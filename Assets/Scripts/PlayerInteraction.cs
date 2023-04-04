using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    CinemachinePOV pov;
    GameObject interaction;
    bool movingToObject = false;

    void Awake()
    {
        pov = (CinemachinePOV)GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent(CinemachineCore.Stage.Aim);
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100))
        {
            InteractableObject properties = hit.collider.gameObject.GetComponentInParent<InteractableObject>();
            if (properties != null)
            {
                GameObject hitObj = properties.gameObject;
                if (hitObj != interaction)
                {
                    SetOutline(false);
                }
                interaction = hitObj;
                SetOutline(true);
                if (Input.GetMouseButtonDown(0))
                {
                    if (properties.CanMoveTo)
                    {
                        movingToObject = true;
                    }
                    properties.StartDialogue(null);
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

        if (movingToObject)
        {
            if ((interaction.transform.position - transform.position).magnitude < 3.0f)
            {
                movingToObject = false;
            }
            transform.position = Vector3.MoveTowards(transform.position, interaction.transform.position, 0.05f);
        }
    }

    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pov.m_HorizontalAxis.m_MaxSpeed = 300;
        pov.m_VerticalAxis.m_MaxSpeed = 300;
    }

    void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        pov.m_HorizontalAxis.m_MaxSpeed = 0;
        pov.m_VerticalAxis.m_MaxSpeed = 0;
        SetOutline(false);
    }

    void SetOutline(bool value)
    {
        if (interaction != null)
        {
            if (interaction.TryGetComponent(out Outline outline))
            {
                outline.enabled = value;
            }
        }
    }
}
