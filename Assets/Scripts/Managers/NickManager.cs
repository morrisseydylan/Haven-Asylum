using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NickManager : MonoBehaviour
{
    public GameObject Nick;

    // Start is called before the first frame update
    void Start()
    {
        if (DataManager.GetStoryChoice("NickLeft"))
        {
            Nick.SetActive(false);
        }
    }
}
