using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Uicontrol : MonoBehaviour {
	bool dragging;
	public SteamVR_TrackedObject rightController;
	public Speech speechenity;
	// Use this for initialization
	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {
		SteamVR_Controller.Device device = SteamVR_Controller.Input ((int)rightController.index);

		if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger)) {
			dragging = false;

			Ray ray = new Ray (rightController.transform.position, rightController.transform.forward);
			//var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (GetComponent<Collider>().Raycast(ray, out hit, 100)) {
				//hit 是被打到的物件
				InputField inputNumber=hit.transform.GetComponent<InputField> ();
				int Number = int.Parse (inputNumber.name.Substring (12, 1));

				//inputNumber.name.Substring(1);
				speechenity.changeField(Number);
				Debug.Log (Number);
				dragging = true;

			}
		}
	}
}
