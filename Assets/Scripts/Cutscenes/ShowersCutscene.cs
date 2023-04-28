using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowersCutscene : MonoBehaviour
{
    public GameObject CafeteriaTrigger;
    public GameObject OfficeTrigger;
    public GameObject FinalTrigger;

    // Start is called before the first frame update
    void Start()
    {
        if (DataManager.GetStoryChoice("WentToCafeteria"))
        {
            if (DataManager.GetStoryChoice("WentToOffice"))
            {
                FinalTrigger.SetActive(true);
            }
            else
            {
                OfficeTrigger.SetActive(true);
            }
        }
        else
        {
            CafeteriaTrigger.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
