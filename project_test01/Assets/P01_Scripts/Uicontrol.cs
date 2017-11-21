using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Uicontrol : MonoBehaviour {
	//bool dragging;
	public SteamVR_TrackedObject rightController;
	public Speech speechenity;
	public GameObject movePanel;
	private int InputFieldNumber = 5;
	private List<InputField> inputArray;
	public Text showText;
	public Collider board;
	// Use this for initialization
	void Start () {
		int i = 0;
		inputArray = new List<InputField>();

		while(i<InputFieldNumber){
			inputArray.Add(movePanel.GetComponentsInChildren<InputField>()[i] );
			Debug.Log (inputArray [i].name);
			i++;
		}


	}
	
	// Update is called once per frame
	void Update () {
		SteamVR_Controller.Device device = SteamVR_Controller.Input ((int)rightController.index);

		if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger)) {
			//dragging = false;
			board.gameObject.SetActive(false);
			Ray ray = new Ray (rightController.transform.position, rightController.transform.forward);
			//var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit ;

			if (Physics.Raycast(ray, out hit)) {
				//hit 是被打到的物件
				if (hit.transform.tag.Equals ("Inputfield")) {
					InputField inputNumber = hit.transform.GetComponent<InputField> ();

					//Debug.Log ( hit.transform.name);
					int Number = inputArray.IndexOf (hit.transform.gameObject.GetComponent<InputField> ());

					//inputNumber.name.Substring(1);
					speechenity.changeField (Number);
					Debug.Log (Number);
				}
				else if(hit.transform.tag.Equals ("NOTE")){
					showText.text=hit.transform.GetComponentInChildren<Text> ().text;
					board.gameObject.SetActive(true);
					Debug.Log ("GetNote");
				}

				//dragging = true;

			}
		}
	}
}
