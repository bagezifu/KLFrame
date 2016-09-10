using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DontDestroyTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Space)) SceneManager.LoadScene("test2");
        if (Input.GetKeyUp(KeyCode.Delete)) SceneManager.LoadScene("test");
    }
}
