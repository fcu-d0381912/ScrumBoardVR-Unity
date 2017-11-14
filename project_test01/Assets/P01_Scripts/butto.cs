
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class butto : MonoBehaviour {
	public GameObject passwordCanvas;
	public GameObject LoginCanvas;
	Button button;

	public  drawline line;
	private GameObject enter;
	private GameObject cubeNum;
	public GameObject errorPanel;
	GameObject gg;
	//bool check=false;
	//public mananger boolmanager;
	private GameObject errortext;
	private string account="";
	public Login login;
	private bool passwordLock = false;
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
			passwordLock = true;

			//if(!account.Equals("")&&!login.GetLoginSuccess()){

			//	errortext.SetActive (true);

			//}

		}else if(controller.GetPressUp(gripButton)){
			passwordLock = false;
			Debug.Log (line.GetPassword ());
			login.SetPassword (line.GetPassword ());
			login.login ();
			line.Clean ();

		}

		//gg.GetComponent<RectTransform> ().offsetMin =new Vector2(164+(this.transform.position.x*50), 0.02129008f);
		//gg.GetComponent<RectTransform> ().offsetMax =new Vector2((-164)+(this.transform.position.x*50), 0.02869793f);
		//Debug.Log (this.transform.position.x);
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
		if (passwordLock&&collider.gameObject.tag.Equals("Password")) {
			Debug.Log ("Start");
			Vector3 v3 = new Vector3(collider.transform.position.x,collider.transform.position.y,collider.transform.position.z+0.01f);
			line.AddPosition(v3,collider.gameObject.name);
			Debug.Log ("Start2");

		}
		if (controller.GetPressDown(triggerButton)&&collider.gameObject.tag.Equals("PersonCard")) {
			Debug.Log ("account");
			account = collider.gameObject.GetComponentInChildren<Text> ().text;
			login.SetAccount (account);
			LoginCanvas.SetActive (false);
			passwordCanvas.SetActive(true);
			Text accountName = errorPanel.GetComponentInChildren<Text>();
			errortext = errorPanel.transform.GetChild (1).gameObject;
			accountName.text = "使用者為:"+account;
			//gg= collider.gameObject;
			Debug.Log (collider.gameObject.GetComponent<RectTransform>().anchoredPosition);

		}
		if (controller.GetPressDown(triggerButton)&&collider.gameObject.tag.Equals("Back")) {
			account="";
			login.SetAccount (account);
			line.Clean ();
			passwordCanvas.SetActive(false);
			LoginCanvas.SetActive (true);
			errortext.SetActive (false);

		}

	}

	public void ShowError(){
		if(errortext!=null){
			errortext.SetActive (true);
		}

	}
}
