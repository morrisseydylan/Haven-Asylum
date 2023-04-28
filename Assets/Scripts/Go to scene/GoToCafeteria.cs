using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToCafeteria : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DataManager.SetStoryChoice("WentToCafeteria", true);
        FindObjectOfType<FadeCamera>().FadeOut("Cafeteria");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
