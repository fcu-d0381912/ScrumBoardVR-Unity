
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class butto : MonoBehaviour {
	Button button;
	public  drawline line;
	private GameObject enter;
	private GameObject cubeNum;
	//bool check=false;
	//public mananger boolmanager;
	public Login login;
	public InputField inputField;
	private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
	//private Valve.VR.EVRButtonId touchPad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
	//private Valve.VR.EVRButtonId appButton = Valve.VR.EVRButtonId.k_EButton_ApplicationMenu;
	private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
	private SteamVR_TrackedObject trackedObj;
	// Use this for initialization
	void Start () {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		button = this.GetComponent<Button>();
		//	cubeNum = this.gameObject;

	}

	// Update is called once per frame
	void Update () {
		if (controller.GetPressDown(gripButton)) {
			
			Debug.Log (line.GetPassword ());
			login.SetPassword (line.GetPassword ());
			login.login ();
			if(!login.GetLoginSuccess()){
				line.Clean ();
			}

		}
	}
	/*
	void OnCollisionStay(Collision c)
	{
		Debug.Log ("colider");
		if (controller.GetPressDown(triggerButton)&&c.gameObject.tag.Equals("Password")) {
			Debug.Log ("Start");
			Vector3 v3 = new Vector3(c.transform.position.x,c.transform.position.y,c.transform.position.z+0.01f);
			line.AddPosition(v3,c.gameObject.name);
			Debug.Log ("Start2");

		}
		if (controller.GetPressDown(triggerButton)&&c.gameObject.tag.Equals("PersonCard")) {
			Debug.Log ("account");
			string account = c.gameObject.GetComponentInChildren<Text> ().text;
			login.SetAccount (account);
			Debug.Log ("account2");

		}


	}
	*/

	void OnTriggerEnter(Collider collider)
	{
		//Debug.Log ("colider");
		if (controller.GetPressDown(triggerButton)&&collider.gameObject.tag.Equals("Password")) {
			Debug.Log ("Start");
			Vector3 v3 = new Vector3(collider.transform.position.x,collider.transform.position.y,collider.transform.position.z+0.01f);
			line.AddPosition(v3,collider.gameObject.name);
			Debug.Log ("Start2");

		}
		if (controller.GetPressDown(triggerButton)&&collider.gameObject.tag.Equals("PersonCard")) {
			Debug.Log ("account");
			string account = collider.gameObject.GetComponentInChildren<Text> ().text;
			login.SetAccount (account);
			Debug.Log ("account2");

		}


	}

}
