
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR || UNITY_STANDALONE
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
		errortext = errorPanel.transform.GetChild (1).gameObject;
		//	cubeNum = this.gameObject;

	}

	// Update is called once per frame
	void Update () {
		if(controller.GetPressDown(triggerButton)){
			PressDownTriggerButton ();
		}
		else if (controller.GetPressDown(gripButton)) {
			PressDownGripButton ();
			/*
			passwordLock = true;

			//if(!account.Equals("")&&!login.GetLoginSuccess()){

			//	errortext.SetActive (true);
				
			//}
			*/
		}else if(controller.GetPressUp(gripButton)){
			PressUpGripButton ();
			/*
			passwordLock = false;
			Debug.Log (line.GetPassword ());
			login.SetPassword (line.GetPassword ());
			login.login ();
			line.Clean ();
			*/
		}else if(enter != null && passwordLock && enter.gameObject.tag.Equals("Password")){
			Debug.Log (enter.gameObject.name);
			Vector3 v3 = new Vector3(enter.transform.position.x,enter.transform.position.y,enter.transform.position.z+0.01f);
			line.AddPosition(v3,enter.gameObject.name);
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
	/*
	void OnTriggerEnter(Collider collider)
	{
		/*
		if(collider.gameObject.tag.Equals("PersonCard") || collider.gameObject.tag.Equals("Password") || collider.gameObject.tag.Equals("Back")){
			enter = collider.gameObject;
			Debug.Log ("personcard");
		}

		Debug.Log ("colider");

		if (passwordLock&&collider.gameObject.tag.Equals("Password")) {
			Debug.Log ("Start");
			Vector3 v3 = new Vector3(collider.transform.position.x,collider.transform.position.y,collider.transform.position.z+0.01f);
			line.AddPosition(v3,collider.gameObject.name);
			Debug.Log ("Start2");

		}
		if (controller.GetPressDown(triggerButton) && enter != null) {
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
		if (controller.GetPressDown(triggerButton)&& enter != null) {
			account="";
			login.SetAccount (account);
			line.Clean ();
			passwordCanvas.SetActive(false);
			LoginCanvas.SetActive (true);
			errortext.SetActive (false);

		}

	}*/

	private void OnTriggerStay(Collider collider){
		enter = collider.gameObject;
		Debug.Log (enter.gameObject.tag);
	}
	private void OnTriggerExit(Collider collider){
		enter = null;
		Debug.Log ("null");
	}

	private void PressDownTriggerButton(){
		if(enter != null){
			switch(enter.gameObject.tag){
				case "PersonCard":
					Debug.Log ("PersonCard");
					account = enter.gameObject.GetComponentInChildren<Text> ().text;
					login.SetAccount (account);
					LoginCanvas.SetActive (false);
					passwordCanvas.SetActive (true);
					Text accountName = errorPanel.GetComponentInChildren<Text> ();

					accountName.text = "使用者為:" + account;
						//gg= collider.gameObject;
					Debug.Log (enter.gameObject.GetComponent<RectTransform> ().anchoredPosition);
						break;
							/*
					case "Password":
						Debug.Log ("Password");
						Vector3 v3 = new Vector3(enter.transform.position.x,enter.transform.position.y,enter.transform.position.z+0.01f);
						line.AddPosition(v3,enter.gameObject.name);
						break;
						*/
				case "Back":
					Debug.Log ("Back");
					account = "";
					login.SetAccount (account);
					line.Clean ();
					passwordCanvas.SetActive (false);
					LoginCanvas.SetActive (true);
					errortext.SetActive (false);
					break;
				default:
					Debug.Log (enter.gameObject.tag);
					break;
			}
		}
		
	}
	private void PressDownGripButton(){
		passwordLock = true;

	}
	private void PressUpGripButton(){
		Debug.Log (line.GetPassword ());
		login.SetPassword (line.GetPassword ());
		login.login ();
		line.Clean ();
		passwordLock = false;
	}
	/*
	void OnTriggerExit(Collider collider)
	{
		Debug.Log ("Exit");
		enter = null;
	}
	*/
	public void ShowError(){
		if(errortext!=null){
			errortext.SetActive (true);
		}

	}
}
#endif