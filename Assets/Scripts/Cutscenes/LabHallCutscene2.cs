using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LabHallCutscene2 : MonoBehaviour
{
    public GameObject rebeccaCam;
    public PlayableDirector cutscene;

    // Start is called before the first frame update
    void Start()
    {
        rebeccaCam.SetActive(true);
        cutscene.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
