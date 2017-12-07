using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;
public class ChangeText : PunBehaviour {
	public string text="";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Text> ().text = text;
	}
		

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			
			stream.Serialize(ref text);
		}
		else
		{
			text="";
			stream.Serialize(ref text);  // pos被填充。必须在某个地方使用
		}
	}
}
