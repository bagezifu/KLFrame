using UnityEngine;
using System.Collections;
using UnityEditor;
using KLFrame;
using System.Collections.Generic;

public class Preofds : ScriptableObject
{
    public List<AudioAttribute> atts;
   
    [MenuItem("Assets/Create/YourClass")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<Preofds>();
        
    }

    public static void Init() {
        Debug.Log("init");
    }

    void OnInspectorGUI() {
        GUILayout.Button("test");
    }

    void OnEnable() {
        foreach (AudioAttribute a in atts) {
            if (a.volume == 0) {
                a.volume = 1;
            }
        }
    }
    /* [RuntimeInitializeOnLoadMethod]
   static void Initialize()
    {
        GameObject.DontDestroyOnLoad(new GameObject("Instance", typeof(Instance))
        {
            hideFlags = HideFlags.HideInHierarchy
        });
        Debug.Log("RuntimeInitializeOnLoadMethod");
    }*/
}
