using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class pick : MonoBehaviour {
	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
	private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
	private SteamVR_TrackedObject trackedObj;

	private GameObject enter;
	private GameObject pickup;
	private GameObject board;
	public TEXT Card;
	// Use this for initialization
	void Start () {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}
	
	// Update is called once per frame
	void Update () {
		if (controller == null) {
			Debug.Log("Controller not initialized");
			return;
		}

		if (controller.GetPressDown(triggerButton) && enter != null) {
			pickup = enter;
			pickup.transform.parent = this.transform;

			pickup.GetComponent<Rigidbody> ().useGravity = false;
			pickup.GetComponent<Rigidbody> ().isKinematic = true;

		}
		else if (controller.GetPressUp(triggerButton) && pickup != null) {
			//pickup.transform.position = pickup.transform.parent.position;
			if(board == null){
				pickup.GetComponent<Rigidbody> ().useGravity = true;
				pickup.GetComponent<Rigidbody> ().isKinematic = false;
				Debug.Log ("2");
			}else{
				//資料庫卡片資料載入

				string cNum = pickup.GetComponentsInChildren<Text>()[1].text;
				float xLocation=pickup.transform.position.x;
				float yLocation=pickup.transform.position.y;
				StartCoroutine(Card.UpdateLocation (cNum, xLocation, yLocation));
			}

			pickup.transform.parent = null;
			pickup = null;
			board = null;
		}
	}

	private void OnTriggerStay(Collider collider) {
		if(collider.gameObject.tag == "NOTE")
		{
			enter = collider.gameObject;
			//changeCard.changeCardToInputfield (enter);//之後要移位置
		}
		else if(collider.gameObject.tag == "BOARD")
		{
			board = collider.gameObject;
			Debug.Log ("1");
		}
	}
	private void OnTriggerExit(Collider collider){
		if( collider.gameObject.tag == "BOARD")
		{
			board = null;
			Debug.Log ("3");
		}
		else if(collider.gameObject.tag == "NOTE"){
			enter = null;
		}
	}
}
