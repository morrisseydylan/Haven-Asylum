using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CourtyardCutscene : MonoBehaviour
{
    public PlayableDirector nickLeaving;
    public PlayableDirector sledgehammer;

    public GameObject nickCamera;
    public GameObject keithCamera;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<PlayerInteraction>().Disable(true);
        if (DataManager.GetStoryChoice("NickLeft"))
        {
            nickCamera.SetActive(true);
            nickLeaving.Play();
        }
        StartCoroutine(Sledgehammer());
    }

    IEnumerator Sledgehammer()
    {
        if (DataManager.GetStoryChoice("NickLeft"))
        {
            yield return new WaitForSeconds(7.5f);
        }
        nickCamera.SetActive(false);
        keithCamera.SetActive(true);
        sledgehammer.Play();
    }
}
