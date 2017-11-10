using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour {


	// Update is called once per frame
	public void changeColor (Color color) {
		
		GetComponent<Renderer>().material.color = color;
	}
}
