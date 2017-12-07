using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;
#if UNITY_EDITOR || UNITY_STANDALONE
public class Uicontrol :PunBehaviour {
	//bool dragging;
	public SteamVR_TrackedObject rightController;
	public Speech speechenity;
	public GameObject movePanel;
	private int InputFieldNumber = 5;
	private List<InputField> inputArray;
	public ChangeText showText;
	public ChangeText showTextContent;
	public ChangeText showAssn;
	public ChangeText showEtime;
	public ChangeText showEstimate;
	public Collider[] boardColiderArray;
	public GameObject board;
	private int boardColiderCount = 5;
	// Use this for initialization
	void Start () {
		int i = 0;
		inputArray = new List<InputField>();

		while(i<InputFieldNumber){
			inputArray.Add(movePanel.GetComponentsInChildren<InputField>()[i] );
			Debug.Log (inputArray [i].name);
			i++;
		}
		boardColiderArray = new Collider[boardColiderCount];
		int countColiderArray = 0;
		while(countColiderArray < boardColiderCount){
			boardColiderArray [countColiderArray] = board.GetComponentsInChildren<Collider> ()[countColiderArray] ;
			countColiderArray++;
		}


	}
	
	// Update is called once per frame
	void Update () {
		SteamVR_Controller.Device device = SteamVR_Controller.Input ((int)rightController.index);

		if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger)) {
			//dragging = false;
			BoardColiderSetFales();
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
					showText.text=hit.transform.GetComponentInChildren<Card> ().Ctitle;
					showTextContent.text = hit.transform.GetComponent<Card>().Ctext;
					showAssn.text=hit.transform.GetComponentInChildren<Card> ().ASsn;
					showEtime.text = hit.transform.GetComponent<Card>().Etime.ToString();
					showEstimate.text = hit.transform.GetComponent<Card>().Estimate.ToString();

					Debug.Log ("GetNote");
				}

				//dragging = true;
			}
			BoardColiderSetTrue();
		}
	}

	void BoardColiderSetFales(){
		int count;
		for(count=0;count<boardColiderCount;count++){
			boardColiderArray[count].gameObject.SetActive(false);
		}
	}

	void BoardColiderSetTrue(){
		int count;
		for(count=0;count<boardColiderCount;count++){
			boardColiderArray[count].gameObject.SetActive(true);
		}
	}
}
#endif