using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject Tutorial;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<PlayerInteraction>().Disable();
        Tutorial.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndTutorial()
    {
        FindObjectOfType<PlayerInteraction>().Enable();
        Tutorial.SetActive(false);
    }
}
