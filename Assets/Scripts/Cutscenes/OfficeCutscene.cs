using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeCutscene : MonoBehaviour
{
    public GameObject ShowersTrigger;
    public GameObject CafeteriaTrigger;
    public GameObject ChoiceTrigger;
    public GameObject FinalTrigger;

    // Start is called before the first frame update
    void Start()
    {
        if (DataManager.GetStoryChoice("WentToShowers"))
        {
            if (DataManager.GetStoryChoice("WentToCafeteria"))
            {
                FinalTrigger.SetActive(true);
            }
            else
            {
                CafeteriaTrigger.SetActive(true);
            }
        }
        else
        {
            if (DataManager.GetStoryChoice("WentToCafeteria"))
            {
                ShowersTrigger.SetActive(true);
            }
            else
            {
                ChoiceTrigger.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
