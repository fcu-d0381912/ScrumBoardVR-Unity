using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class password : MonoBehaviour {
	public GameObject[] button;
	/*
	public GameObject button2;
	public GameObject button3;
	public GameObject button4;
	public GameObject button5;
	public GameObject button6;
	public GameObject button7;
	public GameObject button8;
	public GameObject button9;
	*/
	//private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
	private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
	private SteamVR_TrackedObject trackedObj;

	private GameObject enter;
	private GameObject pickup;
	private GameObject board;

	// Use this for initialization
	void Start () {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider collider) {
		if(collider.gameObject.tag == "NOTE" && collider.gameObject != enter)
		{
			enter = collider.gameObject;

			for(int i = 0; i < 3; i++){
				for(int j = 0; j < 3; j++){
					int count = (i * 2) + j;
					if(enter == button[count]){
						Debug.Log ((i) * 2 + j + 1);
						break;
					}
				}

			}
			Debug.Log ("end");
			//changeCard.changeCardToInputfield (enter);//之後要移位置
		}

	}
	private void OnTriggerExit(Collider collider){
		/*
		if( collider.gameObject.tag == "BOARD")
		{
			board = null;
			Debug.Log ("3");
		}
		*/
	}
}
