using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour {

	Color color = Color.white;
	public ChangeColor cardCube;
	public void onColorChange (HSBColor color) {
		this.color = color.ToColor();
		cardCube.changeColor(this.color);
		Debug.Log ("send color to cube.");
	}
}
