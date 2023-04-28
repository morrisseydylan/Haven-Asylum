using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToLabHall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (DataManager.GetStoryChoice("WentWithAndrea"))
        {
            FindObjectOfType<FadeCamera>().FadeOut("LabHallWithAndrea");
        }
        else
        {
            FindObjectOfType<FadeCamera>().FadeOut("LabHallWithoutAndrea");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
