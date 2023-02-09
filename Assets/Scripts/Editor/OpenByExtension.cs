using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// https://answers.unity.com/questions/1856713/how-do-i-change-the-default-program-for-text-files.html

public class OpenByExtension : MonoBehaviour
{
    [MenuItem("Assets/Open with default program")]
    private static void OpenAssetByDefaultProgram()
    {
        var selected = Selection.activeObject;
        Application.OpenURL("File:" + AssetDatabase.GetAssetPath(selected));
    }
}
