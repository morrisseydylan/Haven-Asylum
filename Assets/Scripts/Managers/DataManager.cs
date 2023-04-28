using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public List<int> InventoryItems;

    Dictionary<string, bool> StoryChoices = new Dictionary<string, bool>();

    public string sacrifice;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        StoryChoices.Add("WentToAsylum", false);
        StoryChoices.Add("NickLeft", false);
        StoryChoices.Add("WentToOffice", false);
        StoryChoices.Add("WentToCafeteria", false);
        StoryChoices.Add("WentToShowers", false);
        StoryChoices.Add("WentWithAndrea", false);
    }

    public static bool GetStoryChoice(string key)
    {
        return Instance.StoryChoices[key];
    }

    public static void SetStoryChoice(string key, bool value)
    {
        Instance.StoryChoices[key] = value;
    }
}
