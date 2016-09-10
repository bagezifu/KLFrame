using UnityEngine;
using System.Collections;
using UnityEditor;

public class CreateMaterialExample : ScriptableObject
{

    [MenuItem("GameObject/Create Material")]
    static void CreateMaterial()
    {
        // Create a simple material asset

        Material material = new Material(Shader.Find("Specular"));
        AssetDatabase.CreateAsset(material, "Assets/MyMaterial.mat");

        // Print the path of the created asset
        Debug.Log(AssetDatabase.GetAssetPath(material));
    }
}
