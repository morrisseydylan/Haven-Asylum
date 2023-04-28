using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CafeteriaCutscene : MonoBehaviour
{
    public GameObject ShowersTrigger;
    public GameObject OfficeTrigger;
    public GameObject FinalTrigger;

    // Start is called before the first frame update
    void Start()
    {
        if (DataManager.GetStoryChoice("WentToShowers"))
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
            ShowersTrigger.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
