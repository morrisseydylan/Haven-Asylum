using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sacrifice : MonoBehaviour
{
    public string sacrifice;

    // Start is called before the first frame update
    void Start()
    {
        DataManager.Instance.sacrifice = sacrifice;
        DataManager.SetStoryChoice("WentToAsylum", true);
        FindObjectOfType<FadeCamera>().FadeOut("End");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
