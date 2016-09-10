using UnityEngine;
using System.Collections;
using KLFrame;
public class AudioTest : MonoBehaviour {

    public AudioAttribute[] atts;
    public AudioAttribute[] test;

	// Use this for initialization
	void Start () {

        AudioUtility.PlaySound(atts[0]);
     // Debug.Log( Resources.Load<Preofds>("Audio/Preofds_1").atts.Count);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnValidate()
    {

    }
}
