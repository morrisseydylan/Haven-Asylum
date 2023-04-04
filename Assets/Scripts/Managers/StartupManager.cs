using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;
using System;
using UnityEngine.Audio;

public class StartupManager : MonoBehaviour
{
    public PlayableDirector IntroDirector;
    public PlayableDirector NameDirector;
    public TMP_InputField NameField;
    public AudioMixer audioMixer;

    bool nameDirectorFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        NameDirector.stopped += OnPlayableDirectorStopped;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (IntroDirector.state == PlayState.Playing)
            {
                IntroDirector.time = IntroDirector.duration;
            }
            else
            {
                if (NameDirector.state == PlayState.Playing)
                {
                    NameDirector.time = NameDirector.duration;
                }
                else if (!nameDirectorFinished)
                {
                    NameDirector.Play();
                }
            }
        }
    }

    void OnPlayableDirectorStopped(PlayableDirector director)
    {
        nameDirectorFinished = true;
    }

    public void NameFieldEndEdit()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            PlayerPrefs.SetString("name", NameField.text);
            StartCoroutine(FadeAudio.StartFade(audioMixer, "MyExposedParam", 3.5f, -80f));
            FindObjectOfType<FadeCamera>().FadeOut("Diner");
        }
    }
}
