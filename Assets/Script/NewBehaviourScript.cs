using UnityEngine;
using System.Collections;
using KLFrame;
using UnityEngine.SceneManagement;
[Singleton]
public class NewBehaviourScript : KLFrameBase<NewBehaviourScript> {

    public NewBehaviourScript newbehaviour;
    public NewBehaviourScript newbehaviour_2;
    // Use this for initialization
    void Start () {
        Debug.Log("start");

       
       
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.T)) {
            Debug.Log(Test.GetInstance().transform.AcitiveSelf());
        }
        if (Input.GetKeyUp(KeyCode.Space)) {
            SceneManager.LoadScene("test2");
        }
	}
}
