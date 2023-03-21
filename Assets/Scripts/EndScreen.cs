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
        if (DataManager.Instance.WentToAsylum)
        {
            // TODO
        }
        else
        {
            text.text = "You decided to have a board game night instead of going to the asylum.\nAll of your friends survived.";
        }
    }
}
