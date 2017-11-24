using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollProject : MonoBehaviour {
	
	public GameObject movePanel;
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
		buttonArray = new Button[movePanel.transform.childCount];
		//tempX=buttonArray [4].GetComponent<RectTransform> ().anchoredPosition.x;
		//tempY=buttonArray [4].GetComponent<RectTransform> ().anchoredPosition.y;
		while(i < movePanel.transform.childCount ){
			buttonArray[i]=movePanel.GetComponentsInChildren<Button>()[i];
			i++;
		}
		i = 0;
		
	}

	// Update is called once per frame
	void Update () {


		for(i=0;i<buttonArray.Length;i++){
			//Debug.Log (centerLeft.x + "," + buttonArray [0].transform.position.x);
			if((buttonArray[i].transform.position.x - centerLeft.x)>6){
				Debug.Log (buttonArray [i].transform.position + " " + " " + centerLeft.x);
				newCenterLeft = movePanel.GetComponent<RectTransform> ().offsetMin;
				buttonArray [i].GetComponent<RectTransform> ().anchoredPosition = new Vector2(((-1)* newCenterLeft.x)-50,buttonArray [i].GetComponent<RectTransform> ().anchoredPosition.y);
			}
			else if((buttonArray[i].transform.position.x + centerLeft.x)<-6){
			//	Debug.Log (buttonArray [i].transform.position + " " + " " + centerLeft.x);
				buttonArray [i].GetComponent<RectTransform> ().anchoredPosition = new Vector2(((-1)* newCenterRight.x)+50,buttonArray [i].GetComponent<RectTransform> ().anchoredPosition.y);
			}
		}

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
			}*/


			//Debug.Log (newCenterRight);
			//Debug.Log (this.transform.position.x);

		}
		else if(controller.GetHairTriggerUp()){
			//newCenterLeft = movePanel.GetComponent<RectTransform> ().offsetMin;
			//newCenterRight = movePanel.GetComponent<RectTransform> ().offsetMax;


		}

		else if(controller.GetPressDown(gripButton)){
			gripPressDown = true;
			controllerPosition.x = transform.position.x;
		}


		else if(controller.GetPressUp(gripButton)){
			gripPressDown = false;


		}
		else if(gripPressDown){
			
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
				
		}

		/*else if(gripPressDown){
			
		}
		*/


		//Debug.Log (this.transform.position.x);
	}


	void OnTriggerEnter(Collider collider)
	{
		//Debug.Log (collider.name);

		if (controller.GetPressDown(triggerButton)) {
			int pNum = int.Parse(collider.gameObject.name);
			projectRoom.GetRoom (pNum);

		}


	}

	/*void OnTriggerExit(Collider collider)
	{
		//Debug.Log ("colider");

		if (controller.GetPressDown(triggerButton)) {

			newCenterLeft = movePanel.GetComponent<RectTransform> ().offsetMin;
			newCenterRight = movePanel.GetComponent<RectTransform> ().offsetMax;

		}


	}*/

}