using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LeaveRoom : MonoBehaviour {
	// Use this for initialization
	#if UNITY_EDITOR || UNITY_STANDALONE
	public Speech speech;
	public mananger connectManager;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.tag.Equals("Hand")){
			Debug.Log ("exit");

			if(SceneManager.GetActiveScene ().buildIndex==2){
				speech.KillProcess ();
				connectManager.Leave ();
			}
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex-1);
			//Debug.Log (SceneManager.GetActiveScene().buildIndex+"ggggggg");
		}

		//SceneManager.LoadScene (1);
	}
	void OnTriggerExit(Collider collider){
		Debug.Log ("exit end");
	}
	#endif
}
