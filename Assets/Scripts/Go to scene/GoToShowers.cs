using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToShowers : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DataManager.SetStoryChoice("WentToShowers", true);
        FindObjectOfType<FadeCamera>().FadeOut("Showers");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
