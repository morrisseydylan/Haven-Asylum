using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemLibrary : ScriptableObject
{

    public List<ItemInfo> itemLibrary = new List<ItemInfo>();

    [System.Serializable]
    public class ItemInfo
    {
        public string name;
        public string flavorText;
        public Texture renderTexture;
    }
}
