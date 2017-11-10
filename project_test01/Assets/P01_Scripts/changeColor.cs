using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeColor : MonoBehaviour {

	public Color originalColor;
	public Color touchColor;
	public Material[] material;
	public Renderer rend;
	//bool touchObject = false;

	void Start () {
		//originalColor = GetComponent<Renderer> ().material.color;
		rend = GetComponent<Renderer> ();
		rend.enabled = true;
		//rend.sharedMaterial = material [0];

	}

	void Update () {
		
	}

	void OnCollosinEnter (Collision other) {
		//touchObject = true;

		/*if(other.gameObject.name == "Controller (left)"){
			rend.sharedMaterial = material [1];
		}*/
		if(other.gameObject.name == "Controller (left)"){
			GetComponent<Renderer> ().material.SetColor ("_Color", touchColor);
		}
		else{
			GetComponent<Renderer> ().material.SetColor ("_Color", originalColor);
		}
	}
	void OnCollosinExit (Collision other) {
		//touchObject = false;

		/*if(other.gameObject.name == "Controller (left)"){
			rend.sharedMaterial = material [1];
		}*/
		if(other.gameObject.name == "Controller (left)"){
			GetComponent<Renderer> ().material.SetColor ("_Color", originalColor);
		}
		else{
			GetComponent<Renderer> ().material.SetColor ("_Color", touchColor);
		}
	}
}
