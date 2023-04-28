using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinerCutscene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string nextScene = "End";
        if (DataManager.GetStoryChoice("WentToAsylum"))
        {
            nextScene = "Courtyard";
        }
        FindObjectOfType<FadeCamera>().FadeOut(nextScene);
    }
}
