using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TMP_Text text = GetComponent<TMP_Text>();
        if (!DataManager.GetStoryChoice("WentToAsylum"))
        {
            text.text = "You decided to have a board game night instead of going to the asylum.\nAll of your friends survived.";
        }
        else
        {
            string survivors;
            switch (DataManager.Instance.sacrifice)
            {
                case "Keith":
                    {
                        survivors = "You, Andrea and Rebecca";
                        break;
                    }
                case "Rebecca":
                    {
                        survivors = "You, Andrea and Keith";
                        break;
                    }
                case "Andrea":
                    {
                        survivors = "You, Keith and Rebecca";
                        break;
                    }
                default:
                    {
                        survivors = "Andrea, Keith and Rebecca";
                        break;
                    }
            }
            text.text = "You decided to sacrifice " + DataManager.Instance.sacrifice + ".\n" + survivors + " made it out alive.";
        }
    }
}
