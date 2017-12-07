using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ScrollProject : MonoBehaviour {
	
	public GameObject movePanel;
	private GameObject enter;
	private Vector2 centerLeft;
	private Vector2 centerRight;
	List<Button> buttonList;
	public Button[] buttonArray; 
	private Vector2 newCenterLeft;
	private Vector2 newCenterRight;
	public RoomConnect projectRoom;
	int i=0;
	float tempX;
	float tempY;

	float finalPosition;
	private float controllerX;
	private float controllerY;
	private Vector3 controllerPosition = new Vector3(0,0,0);
	private bool gripPressDown = false;
	private int moveCount = 0;

	private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
	private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
	private SteamVR_TrackedObject trackedObj;


	// Use this for initialization
	void Start () {
		buttonList = new List<Button> ();
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		centerLeft = movePanel.GetComponent<RectTransform> ().position;
		centerRight = movePanel.GetComponent<RectTransform> ().offsetMin;
		newCenterLeft = movePanel.GetComponent<RectTransform> ().offsetMin;
		newCenterRight = movePanel.GetComponent<RectTransform> ().offsetMax;
		//tempX=buttonArray [4].GetComponent<RectTransform> ().anchoredPosition.x;
		//tempY=buttonArray [4].GetComponent<RectTransform> ().anchoredPosition.y;
	
		
	}

	// Update is called once per frame
	void Update () {
		/*if(buttonArray!=null){
			Debug.Log (centerLeft.x + "," + buttonArray[0].transform.position.x);
		
			for(i=0;i<buttonArray.Length;i++){
				//Debug.Log (centerLeft.x + "," + buttonArray[0].transform.position.x);

				if((buttonArray[i].transform.position.x - centerLeft.x)>buttonArray.Length){
					Debug.Log ("poinot"+buttonArray [i].transform.position + " " + " " + centerLeft.x);
					newCenterLeft = movePanel.GetComponent<RectTransform> ().offsetMin;
					buttonArray [i].GetComponent<RectTransform> ().anchoredPosition = new Vector2(((-1)* newCenterLeft.x)-80,buttonArray [i].GetComponent<RectTransform> ().anchoredPosition.y);
				}
				else if((buttonArray[i].transform.position.x + centerLeft.x)<((-1)*buttonArray.Length)){
				//	Debug.Log (buttonArray [i].transform.position + " " + " " + centerLeft.x);
					buttonArray [i].GetComponent<RectTransform> ().anchoredPosition = new Vector2(((-1)* newCenterRight.x)+80,buttonArray [i].GetComponent<RectTransform> ().anchoredPosition.y);
				}
			}
		}*/
		/*
		if (controller.GetPressDown(triggerButton)) {

			//movePanel= movePanel.gameObject;
			//Vector2 min = movePanel.GetComponent<RectTransform> ().offsetMin;
			//Vector2 max = movePanel.GetComponent<RectTransform> ().offsetMax;

				
			//movePanel.GetComponent<RectTransform> ().offsetMin =new Vector2(newCenterLeft.x+(this.transform.position.x*50), 0);
			//movePanel.GetComponent<RectTransform> ().offsetMax =new Vector2(newCenterRight.x+(this.transform.position.x*50), 0);

			/*if(movePanel.GetComponent<RectTransform> ().offsetMin.x>300){
				movePanel.GetComponent<RectTransform> ().offsetMin =new Vector2(centerLeft.x, 0);
				movePanel.GetComponent<RectTransform> ().offsetMax =new Vector2(centerRight.x, 0);
				newCenterLeft = movePanel.GetComponent<RectTransform> ().offsetMin;
				newCenterRight = movePanel.GetComponent<RectTransform> ().offsetMax;
			}


			//Debug.Log (newCenterRight);
			//Debug.Log (this.transform.position.x);

		}
		else if(controller.GetHairTriggerUp()){
			//newCenterLeft = movePanel.GetComponent<RectTransform> ().offsetMin;
			//newCenterRight = movePanel.GetComponent<RectTransform> ().offsetMax;


		}
		*/
		if(controller.GetPressDown(gripButton)){
			gripPressDown = true;
			controllerPosition.x = transform.position.x;
		}


		else if(controller.GetPressUp(gripButton)){
			gripPressDown = false;


		}
		else if(gripPressDown){
			PressGripButton ();
			/*
			if(controllerPosition.x - transform.position.x < - 0.15){
				movePanel= movePanel.gameObject;

				movePanel.GetComponent<RectTransform> ().offsetMin =new Vector2(newCenterLeft.x + 100, 0);
				movePanel.GetComponent<RectTransform> ().offsetMax =new Vector2(newCenterRight.x + 100, 0);
				newCenterLeft = movePanel.GetComponent<RectTransform> ().offsetMin;
				newCenterRight = movePanel.GetComponent<RectTransform> ().offsetMax;
				moveCount = moveCount + 1;
				Debug.Log ("move" + moveCount);
				controllerPosition.x = transform.position.x;
			}else if( controllerPosition.x - transform.position.x > 0.15){
				movePanel= movePanel.gameObject;

				movePanel.GetComponent<RectTransform> ().offsetMin =new Vector2(newCenterLeft.x - 100, 0);
				movePanel.GetComponent<RectTransform> ().offsetMax =new Vector2(newCenterRight.x - 100, 0);
				newCenterLeft = movePanel.GetComponent<RectTransform> ().offsetMin;
				newCenterRight = movePanel.GetComponent<RectTransform> ().offsetMax;
				moveCount = moveCount + 1;
				Debug.Log ("move" + moveCount);
				controllerPosition.x = transform.position.x;

			}
			*/

		}else if(controller.GetPressDown(triggerButton)){
			PressDownTriggerButton ();
		}

		/*else if(gripPressDown){
			
		}
		*/


		//Debug.Log (this.transform.position.x);
	}


	private void OnTriggerStay(Collider collider)
	{
		//Debug.Log (collider.name);
			if(collider.transform.gameObject.tag.Equals("Project")){
				enter = collider.gameObject;

			}
		/*
		if (controller.GetPressDown(triggerButton)) {
			int pNum = int.Parse(collider.gameObject.name);
			string projectName = collider.GetComponentInChildren<Text> ().text;
			PlayerPrefs.SetString ("projectName", projectName);
			projectRoom.GetRoom (pNum);

		}
		*/

	}
	private void OnTriggerExit(Collider collider)
	{
		//Debug.Log (collider.name);
		enter = null;
	}
	private void PressDownTriggerButton(){
		if(enter != null){
			int pNum = int.Parse(enter.gameObject.name);
			string projectName = enter.GetComponentInChildren<Text> ().text;
			PlayerPrefs.SetString ("projectName", projectName);
			projectRoom.GetRoom (projectName,pNum);
		}

	}
	private void PressGripButton(){
		if(controllerPosition.x - transform.position.x < - 0.12){
			movePanel= movePanel.gameObject;

			movePanel.GetComponent<RectTransform> ().offsetMin =new Vector2(newCenterLeft.x + 100, 0);
			movePanel.GetComponent<RectTransform> ().offsetMax =new Vector2(newCenterRight.x + 100, 0);
			newCenterLeft = movePanel.GetComponent<RectTransform> ().offsetMin;
			newCenterRight = movePanel.GetComponent<RectTransform> ().offsetMax;
			moveCount = moveCount + 1;
			Debug.Log ("moveleft" + moveCount);
			controllerPosition.x = transform.position.x;

			if((buttonArray[i].transform.position.x - centerLeft.x) > (finalPosition)){
				buttonArray [i].GetComponent<RectTransform> ().anchoredPosition = new Vector2(((-1)* newCenterLeft.x)-100,buttonArray [i].GetComponent<RectTransform> ().anchoredPosition.y);
				Debug.Log ("IS VALUE:" + i);
				if(i==0){
					i = buttonArray.Length;
				}
				i--;
				
			}
			

		}else if( controllerPosition.x - transform.position.x > 0.12){
			movePanel= movePanel.gameObject;

			movePanel.GetComponent<RectTransform> ().offsetMin =new Vector2(newCenterLeft.x - 100, 0);
			movePanel.GetComponent<RectTransform> ().offsetMax =new Vector2(newCenterRight.x - 100, 0);
			newCenterLeft = movePanel.GetComponent<RectTransform> ().offsetMin;
			newCenterRight = movePanel.GetComponent<RectTransform> ().offsetMax;
			moveCount = moveCount + 1;
			Debug.Log ("move" + moveCount);
			controllerPosition.x = transform.position.x;

				if(Math.Abs(Math.Abs(buttonArray[i].transform.position.x) - Math.Abs(centerLeft.x)) > finalPosition){
					buttonArray [i].GetComponent<RectTransform> ().anchoredPosition = new Vector2(((400-newCenterLeft.x))+((buttonArray.Length-1)*100),buttonArray [i].GetComponent<RectTransform> ().anchoredPosition.y);
				i++;
				if(i==buttonArray.Length){
					i = 0;
				}
			}
			
		}
		//CheckPosition ();
	}
	/*void OnTriggerExit(Collider collider)
	{
		//Debug.Log ("colider");

		if (controller.GetPressDown(triggerButton)) {

			newCenterLeft = movePanel.GetComponent<RectTransform> ().offsetMin;
			newCenterRight = movePanel.GetComponent<RectTransform> ().offsetMax;

		}


	}*/
	/*private void CheckPosition(){
		if(buttonArray!=null){
				Debug.Log (newCenterLeft.x);

			for(i=0;i<buttonArray.Length;i++){
				//Debug.Log (centerLeft.x + "," + buttonArray[0].transform.position.x);

					if((buttonArray[i].transform.position.x - centerLeft.x) > (finalPosition)){
					Debug.Log ("poinot"+buttonArray [i].transform.position + " " + " " + centerLeft.x);
					//newCenterLeft = movePanel.GetComponent<RectTransform> ().offsetMin;
						buttonArray [i].GetComponent<RectTransform> ().anchoredPosition = new Vector2(((-1)* newCenterLeft.x)-65,buttonArray [i].GetComponent<RectTransform> ().anchoredPosition.y);
				}
					else if(buttonArray[i].transform.position.x - centerLeft.x < ((-1)*(finalPosition))){
					//	Debug.Log (buttonArray [i].transform.position + " " + " " + centerLeft.x);
						//newCenterRight = movePanel.GetComponent<RectTransform> ().offsetMax;
						buttonArray [i].GetComponent<RectTransform> ().anchoredPosition = new Vector2(((-1)* newCenterRight.x)+65,buttonArray [i].GetComponent<RectTransform> ().anchoredPosition.y);
				}
			}
		}
	}*/
	public void SetButton(){
		buttonArray = new Button[movePanel.transform.childCount];
		i = 0;
		while(i < movePanel.transform.childCount ){
			buttonArray[i]=movePanel.GetComponentsInChildren<Button>()[i];
			finalPosition = buttonArray [i].transform.position.x;
			finalPosition = Math.Abs(centerLeft.x - finalPosition);
			i++;
			
		}
		i--;

	}
}