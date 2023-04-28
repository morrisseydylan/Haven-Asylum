using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToFinal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (DataManager.GetStoryChoice("WentWithAndrea"))
        {
            if (DataManager.GetStoryChoice("NickLeft"))
            {
                FindObjectOfType<FadeCamera>().FadeOut("Atrium2WithoutNick");
            }
            else
            {
                FindObjectOfType<FadeCamera>().FadeOut("Atrium2WithNick");
            }
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
