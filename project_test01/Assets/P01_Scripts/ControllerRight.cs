using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerRight : MonoBehaviour {

	private Valve.VR.EVRButtonId appButton = Valve.VR.EVRButtonId.k_EButton_ApplicationMenu;
	private SteamVR_Controller.Device controller;
	private SteamVR_LaserPointer lazerPointer;
	private bool showLazerPointer;
	private 

	// Use this for initialization
	void Start () {
		showLazerPointer = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(controller.GetPressUp(appButton)) {
			Debug.Log ("open steamvr lazer");
			//lazerPointer.active(showLazerPointer);
			//lazerPointer
		}
	}
}
