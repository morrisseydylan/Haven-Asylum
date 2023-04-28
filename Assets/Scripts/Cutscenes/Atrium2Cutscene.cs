using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Atrium2Cutscene : MonoBehaviour
{
    public GameObject nickCam;
    public PlayableDirector NickDies;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<PlayerInteraction>().Disable(true);
        nickCam.SetActive(true);
        NickDies.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
