using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightController : MonoBehaviour {
	private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

	private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
	private SteamVR_TrackedObject trackedObj;

	private GameObject pickup;

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

		if (controller.GetPressDown(triggerButton) && pickup != null) {
			pickup.transform.parent = this.transform;

		}
		if (controller.GetPressUp(triggerButton) && pickup != null) {
			//pickup.transform.position = pickup.transform.parent.position;

			pickup.transform.parent = null;

			pickup = null;
		}
	}
	private void OnTriggerEnter(Collider collider) {
		if(pickup == null && collider.gameObject.tag == "NOTE")
		{
			pickup = collider.gameObject;
		}

	}

	private void OnTriggerExit(Collider collider) {
		/*
		if(pickup == collider.gameObject)
		{
			pickup = null;
		}
		*/
	}
}
