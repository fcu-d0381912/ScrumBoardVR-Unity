using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR || UNITY_STANDALONE
public class right_Controller : MonoBehaviour {
	private float _mMoveSpeed = 2.5f;
	private const float VERTICAL_LIMIT = 60f;
	private int menuCount = 0;

	private Valve.VR.EVRButtonId appButton = Valve.VR.EVRButtonId.k_EButton_ApplicationMenu;
	private Valve.VR.EVRButtonId touchPad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
	private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
	//private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
	public GameObject camerarig;
	private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
	private SteamVR_TrackedObject trackedObj;
	public GameObject EditorCanvas;
	public GameObject ControllerUIExplainCanvas;
	private Transform vrCamera;
	/*
	private GameObject enter;
	private GameObject board;
	private GameObject pickup;
	*/
	public Image imageUIMove;
	private bool showEditorCanvas = false;
	private bool showControllerUIExplainCanvas = false;
	// Use this for initialization
	void Start () {
		trackedObj = GetComponent<SteamVR_TrackedObject>();

		//vrCamera = GameObject.Find ("MainCamera").GetComponent<SteamVR_Camera>();
	}

	// Update is called once per frame
	void Update () {
		if (controller == null) {
			Debug.Log ("Controller not initialized");
			return;
		} 
		if(controller.GetPressUp(appButton)){
			Switch ();
			AppShowUI ();
		}
		if(controller.GetPressDown(gripButton)){
			GripShowUI ();
		}


		if (controller.GetAxis().x != 0 || controller.GetAxis().y != 0){
			TouchPad();
		}
		/*if (controller.GetPressDown(triggerButton) && enter != null) {
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
			}

			pickup.transform.parent = null;
			pickup = null;
			board = null;
		}*/
	}

	/*private void OnTriggerStay(Collider collider) {
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
	/*private void OpenAppUI(){
		showNoteCanvas = !showNoteCanvas;
		noteCanvas.SetActive (showNoteCanvas);
	}*/
	private void Switch(){
		menuCount++;
		menuCount = menuCount % 2;
	}
	private void Move(){
		Quaternion orientation = Camera.main.transform.rotation;
		var touchPadVector = controller.GetAxis(touchPad);
		Vector3 moveDirection = orientation * Vector3.forward * touchPadVector.y + orientation * Vector3.right * touchPadVector.x;
		Vector3 pos = camerarig.transform.position;
		pos.x += moveDirection.x * _mMoveSpeed * Time.deltaTime;
		pos.z += moveDirection.z * _mMoveSpeed * Time.deltaTime;
		camerarig.transform.position = pos;
	}

	private void TouchPad(){
		if (menuCount == 1){
			Move();

		}else if (menuCount == 0){
			//MENU_2 功能

		}
			
	}
	/*
	private void SetUI(){
		if(menuCount == 1){
			imageUIMove.sprite = Resources.Load<Sprite> ("uiMove2");
		}else if(menuCount == 0){
			imageUIMove.sprite = Resources.Load<Sprite> ("uiNull");
		}

		
	}
	*/
	private void AppShowUI(){
		if(menuCount == 1){
			Debug.Log ("open left ui");
			showEditorCanvas = !showEditorCanvas;
			EditorCanvas.SetActive (showEditorCanvas);
		}
		else if (menuCount == 0){
			Debug.Log ("close left ui");
			showEditorCanvas = !showEditorCanvas;
			EditorCanvas.SetActive (showEditorCanvas);
			//關UI
		}
	}
	private void GripShowUI(){
		Debug.Log ("explain ui");
		showControllerUIExplainCanvas = !showControllerUIExplainCanvas;
		ControllerUIExplainCanvas.SetActive (showControllerUIExplainCanvas);
	}

}
#endif