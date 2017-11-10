using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawline : MonoBehaviour {
	public Color c1 = Color.red;
	public Color c2 = Color.red;
	List<Vector3> positionList;
	LineRenderer lineRenderer = null;
	string passwordArray="";
	int positionCount = 0;
	// Use this for initialization
	void Start () {
		//drsawline = this.GetComponent<LineRenderer> ();
		//poisionArray = new Vector3[9];
		positionList = new List<Vector3>();

		lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		lineRenderer.widthMultiplier = 0.02f;
		//lineRenderer.positionCount = 9;
		float alpha = 1.0f;
		Gradient gradient = new Gradient();
		gradient.SetKeys(
			new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
			new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
		);
		lineRenderer.colorGradient = gradient;

		//lineRenderer.positionCount = lengthOfLineRenderer;

	}

	// Update is called once per frame
	void Update () {

		//lineRenderer.SetPosition (0,new Vector3(0,0,0));
		//lineRenderer.SetPosition (1,new Vector3(0,2,0));


	}


	public void AddPosition(Vector3 passwordPoint,string password){
		if(!positionList.Contains(passwordPoint)){
			
			passwordArray += password;
			positionList.Add(passwordPoint);
			lineRenderer.positionCount = positionList.Count;

		}
		if(positionList.Count>1){
			lineRenderer.SetPositions (positionList.ToArray());

		}

	}

	public string GetPassword(){
		return passwordArray;
	}

	public void Clean(){
		passwordArray = "";
		positionList.Clear ();

		lineRenderer.positionCount=0;

	}
}
