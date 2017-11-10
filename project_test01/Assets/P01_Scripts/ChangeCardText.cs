using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChangeCardText : MonoBehaviour {

	public InputField inputField;
	private GameObject Temp;
	//public delegate void UserRequest(object sender,EventArgs e);
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void changeCardToInputfield(GameObject obj)
	{
		if(obj!=null){
			Temp = obj;
			Debug.Log (obj);
			inputField.text = obj.GetComponentInChildren<Text>().text;
		}

	}
	public void changeInputfieldToCard()
	{
		if(Temp!=null){
			Temp.GetComponentInChildren<Text>().text = inputField.text ;
		}

	}
}
