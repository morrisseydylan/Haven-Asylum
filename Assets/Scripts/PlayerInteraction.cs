using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject Player;
    public GameObject Crosshair;

    CinemachinePOV pov;
    GameObject currentObject;
    InteractableObject currentProperties;
    FadeCamera fadeCamera;
    int currentArea = 0;

    void Awake()
    {
        pov = (CinemachinePOV)GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent(CinemachineCore.Stage.Aim);
        fadeCamera = FindObjectOfType<FadeCamera>();
    }

    void Start()
    {
        Enable();
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
                if (properties.enabled)
                {

                    GameObject hitObj = properties.gameObject;
                    if (hitObj != currentObject)
                    {
                        SetOutline(false);
                    }
                    currentObject = hitObj;
                    SetOutline(true);
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (properties.Area == currentArea)
                        {
                            currentProperties = properties;
                            currentProperties.StartDialogue(null);
                        }
                        else
                        {
                            if (currentProperties != null)
                            {
                                if (currentArea != properties.Area)
                                {
                                    foreach (InteractableObject obj in currentProperties.ObjectsInArea)
                                    {
                                        obj.enabled = false;
                                    }
                                    currentProperties.enabled = true;
                                }
                            }
                            currentProperties = properties;
                            currentArea = currentProperties.Area;
                            pov.m_HorizontalAxis.m_MaxSpeed = 0;
                            pov.m_VerticalAxis.m_MaxSpeed = 0;
                            foreach (InteractableObject objs in currentProperties.ObjectsInArea)
                            {
                                objs.enabled = true;
                            }
                            if (currentProperties.DisableWhileInArea)
                            {
                                currentProperties.enabled = false;
                            }
                            fadeCamera.FadeOut(null);
                            StartCoroutine(MovePlayer());
                        }
                        SetOutline(false);
                    }
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

        /*if (movingToObject)
        {
            if ((interaction.transform.position - transform.position).magnitude < 3.0f)
            {
                movingToObject = false;
            }
            transform.position = Vector3.MoveTowards(transform.position, interaction.transform.position, 0.05f);
        }*/
    }

    public void Enable()
    {
        enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Crosshair.SetActive(true);
        pov.m_HorizontalAxis.m_MaxSpeed = 300;
        pov.m_VerticalAxis.m_MaxSpeed = 300;
    }

    public void Disable(bool lockedCursor = false)
    {
        enabled = false;
        if (lockedCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
        Crosshair.SetActive(false);
        pov.m_HorizontalAxis.m_MaxSpeed = 0;
        pov.m_VerticalAxis.m_MaxSpeed = 0;
        SetOutline(false);
    }

    void SetOutline(bool value)
    {
        if (currentObject != null)
        {
            if (currentObject.TryGetComponent(out Outline outline))
            {
                outline.enabled = value;
            }
        }
    }

    IEnumerator MovePlayer()
    {
        yield return new WaitForSeconds(3.7f);
        Player.transform.position = currentProperties.PlayerLocation.transform.position;
        fadeCamera.FadeIn();
        pov.m_HorizontalAxis.m_MaxSpeed = 300;
        pov.m_VerticalAxis.m_MaxSpeed = 300;
        currentProperties.StartDialogue(null);
    }
}
