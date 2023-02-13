using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartupManager : MonoBehaviour
{
    public GameObject UICanvas;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeTitle());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            FindObjectOfType<FadeCamera>().FadeOut(1);
        }
    }

    IEnumerator FadeTitle()
    {
        yield return new WaitForSeconds(10);
        UICanvas.SetActive(true);
    }
}
