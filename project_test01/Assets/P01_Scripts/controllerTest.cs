using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
#if UNITY_EDITOR || UNITY_STANDALONE
public class controllerTest : MonoBehaviour {
	private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
	//private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
	private Valve.VR.EVRButtonId touchPad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
	private Valve.VR.EVRButtonId appButton = Valve.VR.EVRButtonId.k_EButton_ApplicationMenu;
	private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
	private SteamVR_TrackedObject trackedObj;
	//public GameObject speech;
	//private GameObject speech;
	//private Speech SpeechComponent;
	public Speech speechenity;
	public ChangeCardText changeCard;
	public GameObject EditorCanvas;
	private TEXT creatcard;
	// Use this for initialization
	public Image imageUISpeech;
	public Image imageUIBlank;
	public Image imageUIDelete;
	public Image imageUIToLeft;
	public Image imageUIToRight;
	private int menuCount = 0;
	/*
	private GameObject enter;
	private GameObject pickup;
	private GameObject board;
	*/
	private bool showEditorCanvas;

	void Start () {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		//SpeechComponent = speech.GetComponent<Speech> ();
		creatcard = GameObject.Find ("Card").GetComponent<TEXT> ();
		showEditorCanvas = false;

		/*
		imageUIBlank.sprite = Resources.Load<Sprite> ("uiBlank2");
		imageUIDelete.sprite = Resources.Load<Sprite> ("uiDelete2");
		imageUIToLeft.sprite = Resources.Load<Sprite> ("uiToLeft2");
		imageUIToRight.sprite = Resources.Load<Sprite> ("uiToRight2");
		*/
		//speech.Find();

	}

	// Update is called once per frame
	void Update () {
		
		if (controller == null) {
			Debug.Log("Controller not initialized");
			return;
		}

		if (controller.GetPressUp (appButton)) {
			SwitchLeft ();
			AppShowUI ();
		}

		if (controller.GetPressDown(gripButton) && menuCount == 1) {
			Debug.Log ("speechStart");
			imageUISpeech.sprite = Resources.Load<Sprite> ("uiStart");
			speechenity.speechStart();
			Debug.Log ("speechStart2");
		}
		else if (controller.GetPressUp(gripButton) && menuCount == 1) {
			Debug.Log ("speechStop");
			imageUISpeech.sprite = Resources.Load<Sprite> ("uiPause");
			speechenity.speechStop ();

			Debug.Log ("speechStop2");
		}

		else if(controller.GetPressDown(touchPad) && menuCount == 1)
		{
			//float angle = Mathf.Sin ( controller.GetAxis (touchPad).y);


			if (controller.GetAxis (touchPad).y > 0 && controller.GetAxis (touchPad).x > 0){
				imageUIBlank.sprite = Resources.Load<Sprite> ("uiBlank");
				creatcard.Generate ();
				//StartCoroutine(creatcard.ListPnumCard());
				Debug.Log (1);
			}
			else if (controller.GetAxis (touchPad).x < -0 && controller.GetAxis (touchPad).y > 0){
				imageUIDelete.sprite = Resources.Load<Sprite> ("uiDelete");
				speechenity.deleteText();
				Debug.Log (2);
			}
			else if (controller.GetAxis (touchPad).y < 0 && controller.GetAxis (touchPad).x < 0){
				imageUIToLeft.sprite = Resources.Load<Sprite> ("uiToLeft");
				speechenity.caretleft();
				//changeCard.changeInputfieldToCard ();//要改位置
				Debug.Log (3);
			}
			else if (controller.GetAxis (touchPad).x > 0 && controller.GetAxis (touchPad).y < 0){
				imageUIToRight.sprite = Resources.Load<Sprite> ("uiToRight");
				speechenity.caretRight();
				//speechenity.changeField (1);//要改位置
				Debug.Log (4);
			}
		}

		else if (controller.GetPressUp(touchPad) && menuCount == 1){
			
			imageUIBlank.sprite = Resources.Load<Sprite> ("uiBlank2");
			imageUIDelete.sprite = Resources.Load<Sprite> ("uiDelete2");
			imageUIToLeft.sprite = Resources.Load<Sprite> ("uiToLeft2");
			imageUIToRight.sprite = Resources.Load<Sprite> ("uiToRight2");

		}

		
		/*
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
			}else{
				//資料庫卡片資料載入
			}

			pickup.transform.parent = null;
			pickup = null;
			board = null;
		}*/
	}
	/*
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

	*/
	private void SwitchLeft(){
		/*showEditorCanvas = !showEditorCanvas;
		EditorCanvas.SetActive (showEditorCanvas);*/
		menuCount++;
		menuCount = menuCount % 2;
	}

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

}
#endif