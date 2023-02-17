using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// https://forum.unity.com/threads/free-basic-camera-fade-in-script.509423/

public class FadeCamera : MonoBehaviour
{
    public AnimationCurve FadeCurve = new(new Keyframe(0, 1), new Keyframe(0.6f, 0.7f, -1.8f, -1.2f), new Keyframe(1, 0));

    Texture2D texture;
    bool reverse;
    bool done;
    float alpha;
    float time;
    int sceneToLoad;

    [RuntimeInitializeOnLoadMethod]
    public void FadeIn()
    {
        reverse = false;
        done = false;
        alpha = 1;
        time = 0;
    }

    public void FadeOut(int sceneToLoad)
    {
        reverse = true;
        done = false;
        alpha = 0;
        time = 1;
        this.sceneToLoad = sceneToLoad;
    }

    public void OnGUI()
    {
        if (texture == null)
        {
            texture = new Texture2D(1, 1);
        }

        texture.SetPixel(0, 0, new Color(0, 0, 0, alpha));
        texture.Apply();

        if (!reverse)
        {
            if (!done)
            {
                time += Time.deltaTime / 10;
                alpha = FadeCurve.Evaluate(time);
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture);
            }

            if (alpha <= 0)
            {
                done = true;
            }
        }
        else
        {
            if (!done)
            {
                time -= Time.deltaTime / 10;
                alpha = FadeCurve.Evaluate(time);
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture);
            }
            else
            {
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture);
            }

            if (alpha >= 1)
            {
                done = true;
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }
}