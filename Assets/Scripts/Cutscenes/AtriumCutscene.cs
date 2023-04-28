using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtriumCutscene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string nextScene = "";
        if (DataManager.GetStoryChoice("WentToOffice"))
        {
            nextScene = "Office";
        }
        else if (DataManager.GetStoryChoice("WentToCafeteria"))
        {
            nextScene = "Cafeteria";
        }
        else if (DataManager.GetStoryChoice("WentToShowers"))
        {
            nextScene = "Showers";
        }
        else
        {
            Debug.Log("Error: no scene selected");
        }
        FindObjectOfType<FadeCamera>().FadeOut(nextScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
