using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollProject : MonoBehaviour {
	
	public GameObject movePanel;
	private Vector2 centerLeft;
	private Vector2 centerRight;
	public Button[] buttonArray; 
	private Vector2 newCenterLeft;
	private Vector2 newCenterRight;
	int i=0;
	float tempX;
	float tempY;

	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
	private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
	private SteamVR_TrackedObject trackedObj;


	// Use this for initialization
	void Start () {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		centerLeft = movePanel.GetComponent<RectTransform> ().position;
		centerRight = movePanel.GetComponent<RectTransform> ().offsetMax;
		newCenterLeft = movePanel.GetComponent<RectTransform> ().offsetMin;
		newCenterRight = movePanel.GetComponent<RectTransform> ().offsetMax;
		tempX=buttonArray [4].GetComponent<RectTransform> ().anchoredPosition.x;
		tempY=buttonArray [4].GetComponent<RectTransform> ().anchoredPosition.y;

	
	}

	// Update is called once per frame
	void Update () {


		for(i=0;i<buttonArray.Length;i++){
			if((buttonArray[i].transform.position.x-centerLeft.x)>5){
				Debug.Log (buttonArray [i].transform.position + " " + " " + centerLeft.x);
				buttonArray [i].GetComponent<RectTransform> ().position = new Vector3 (-3, centerLeft.y,2);
			}
		}

		if (controller.GetPressDown(triggerButton)) {

			movePanel= movePanel.gameObject;
			//Vector2 min = movePanel.GetComponent<RectTransform> ().offsetMin;
			//Vector2 max = movePanel.GetComponent<RectTransform> ().offsetMax;

				
			movePanel.GetComponent<RectTransform> ().offsetMin =new Vector2(newCenterLeft.x+(this.transform.position.x*50), 0);
			movePanel.GetComponent<RectTransform> ().offsetMax =new Vector2(newCenterRight.x+(this.transform.position.x*50), 0);

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
			newCenterLeft = movePanel.GetComponent<RectTransform> ().offsetMin;
			newCenterRight = movePanel.GetComponent<RectTransform> ().offsetMax;
		}

		//Debug.Log (this.transform.position.x);
	}


	/*void OnTriggerEnter(Collider collider)
	{
		Debug.Log ("colider");

		if (controller.GetPressDown(triggerButton)) {
			
			movePanel= movePanel.gameObject;
			//Vector2 min = movePanel.GetComponent<RectTransform> ().offsetMin;
			//Vector2 max = movePanel.GetComponent<RectTransform> ().offsetMax;
			//Debug.Log (min.x);
			//Debug.Log (max.x);
			movePanel.GetComponent<RectTransform> ().offsetMin =new Vector2(newCenterLeft.x+(this.transform.position.x*50), 0);
			movePanel.GetComponent<RectTransform> ().offsetMax =new Vector2(newCenterRight.x+(this.transform.position.x*50), 0);
			Debug.Log (collider.gameObject.GetComponent<RectTransform>().anchoredPosition);

		}


	}

	void OnTriggerExit(Collider collider)
	{
		//Debug.Log ("colider");

		if (controller.GetPressDown(triggerButton)) {

			newCenterLeft = movePanel.GetComponent<RectTransform> ().offsetMin;
			newCenterRight = movePanel.GetComponent<RectTransform> ().offsetMax;

		}


	}*/

}