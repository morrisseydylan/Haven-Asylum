using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LabHallCutscene : MonoBehaviour
{
    public GameObject andreaCam;
    public PlayableDirector cutscene;

    // Start is called before the first frame update
    void Start()
    {
        andreaCam.SetActive(true);
        cutscene.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
