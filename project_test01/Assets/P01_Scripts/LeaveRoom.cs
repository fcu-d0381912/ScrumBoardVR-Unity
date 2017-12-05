using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LeaveRoom : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.tag.Equals("Hand")){
			Debug.Log ("exit");
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex-1);
			//Debug.Log (SceneManager.GetActiveScene().buildIndex+"ggggggg");
		}

		//SceneManager.LoadScene (1);
	}
	void OnTriggerExit(Collider collider){
		Debug.Log ("exit end");
	}
}
