using UnityEngine;
using KLFrame;
using UnityEngine.SceneManagement;



public class Test : KLFrameBase<Test> {
    [AutoAssign(true)]
    public AudioSource audiosource;
    public AudioHandler handler;
    public Test test;
    public Test get;
    
    // Use this for initialization
    void Start () {
      
        //  Debug.Log(SingletonManager.GetInstance<Test>());  
        Debug.Log("".GetCurrentTime());
        gameObject.GetPosition();
        gameObject.SetPosition(new Vector3(10,10,10));
       // gameObject.SetParent(Camera.main.gameObject);
        transform.AcitiveSelf();
       // Debug.Log((9).SecondToMinute());
        //Debug.Log(this.GetPosition());
       // StartCoroutine("Player".Blink("_",.5f));
    }

    
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            Debug.Log(NewBehaviourScript.GetInstance());
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SceneManager.LoadScene("test2");
        }
    }

   


}
