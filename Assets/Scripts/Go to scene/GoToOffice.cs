using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToOffice : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DataManager.SetStoryChoice("WentToOffice", true);
        FindObjectOfType<FadeCamera>().FadeOut("Office");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
