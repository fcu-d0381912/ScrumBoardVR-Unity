using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointToOpen : MonoBehaviour {

	public SteamVR_TrackedObject rightController;
	bool dragging;
	public GameObject ShowUICanvas;

	private int touchCount;
	private bool showShowUICanvas;

	// Use this for initialization
	void Start () {
		showShowUICanvas = false;
	}
	
	// Update is called once per frame
	void Update () {
		SteamVR_Controller.Device device = SteamVR_Controller.Input ((int)rightController.index);

		if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger)) {
			dragging = false;
			touchCount = 0;
			showuiPanel ();

			Ray ray = new Ray (rightController.transform.position, rightController.transform.forward);
			//var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (GetComponent<Collider>().Raycast(ray, out hit, 100) ) {
				dragging = true;
				touchCount = 1;
				showuiPanel ();
			}
		}
	}
	//private void On


	private void showuiPanel(){
		if(touchCount == 1){
			Debug.Log ("open ui panel");
			showShowUICanvas = !showShowUICanvas;
			ShowUICanvas.SetActive (showShowUICanvas);
		}
		else if (touchCount == 0){
			Debug.Log ("close ui panel");
			showShowUICanvas = !showShowUICanvas;
			ShowUICanvas.SetActive (showShowUICanvas);
			//關UI
		}
	}
}
