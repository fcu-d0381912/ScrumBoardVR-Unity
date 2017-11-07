using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class controllerTest : MonoBehaviour {
	private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
	private Valve.VR.EVRButtonId touchPad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
	private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
	private SteamVR_TrackedObject trackedObj;
	//public GameObject speech;
	//private GameObject speech;
	//private Speech SpeechComponent;
	public Speech speechenity;
	private TEXT creatcard;
	// Use this for initialization
	public Image imageUI;
	private GameObject enter;
	private GameObject pickup;
	private GameObject board;

	void Start () {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		//SpeechComponent = speech.GetComponent<Speech> ();
		creatcard = GameObject.Find ("Card").GetComponent<TEXT> ();
		//speech.Find();
	}

	// Update is called once per frame
	void Update () {
		if (controller == null) {
			Debug.Log("Controller not initialized");
			return;
		}

		else if (controller.GetPressDown(gripButton)) {
			Debug.Log ("speechStart");
			imageUI.sprite = Resources.Load<Sprite> ("uiStart");
			speechenity.speechStart();
			Debug.Log ("speechStart2");
		}
		else if (controller.GetPressUp(gripButton)) {
			Debug.Log ("speechStop");
			imageUI.sprite = Resources.Load<Sprite> ("uiPause");
			speechenity.speechStop ();

			Debug.Log ("speechStop2");
		}

		else if(controller.GetTouchDown(touchPad))
		{
			//float angle = Mathf.Sin ( controller.GetAxis (touchPad).y);

			if (controller.GetAxis (touchPad).y > 0 && controller.GetAxis (touchPad).x > 0){
				creatcard.ge ();
				Debug.Log (1);
			}
			else if (controller.GetAxis (touchPad).x < -0 && controller.GetAxis (touchPad).y > 0){
				speechenity.deleteText();
				Debug.Log (2);
			}
			else if (controller.GetAxis (touchPad).y < 0 && controller.GetAxis (touchPad).x < 0){
				speechenity.caretleft();
				Debug.Log (3);
			}
			else if (controller.GetAxis (touchPad).x > 0 && controller.GetAxis (touchPad).y < 0){
				speechenity.caretRight();
				Debug.Log (4);
			}

		}

		else if (controller.GetPressDown(triggerButton) && enter != null) {
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
			}

			pickup.transform.parent = null;
			pickup = null;
			board = null;
		}
	}

	private void OnTriggerEnter(Collider collider) {
		if(collider.gameObject.tag == "NOTE")
		{
			enter = collider.gameObject;
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