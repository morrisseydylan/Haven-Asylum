using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject PauseUI;

    CursorLockMode previousMode;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                PauseUI.SetActive(true);
                previousMode = Cursor.lockState;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Resume();
            }
        }
    }

    public void Resume()
    {
        Time.timeScale = 1;
        PauseUI.SetActive(false);
        Cursor.lockState = previousMode;
    }

    public void Options()
    {
        // TODO
    }

    public void Quit()
    {
        Application.Quit();
    }
}
