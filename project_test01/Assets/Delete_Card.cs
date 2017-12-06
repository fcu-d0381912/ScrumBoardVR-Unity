using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete_Card : MonoBehaviour {
	#if UNITY_EDITOR || UNITY_STANDALONE
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.tag.Equals("NOTE")){
			Debug.Log ("delete note : " + collider.gameObject.GetComponent<Card>().Cnum);
			StartCoroutine(DeleteCard (collider.gameObject.GetComponent<Card> ().Cnum));
		}
	}
	public IEnumerator DeleteCard(int Cnum)
	{
		string CardDeleteURL = "http://140.134.26.71:12345/CardDelete.php";
		WWWForm form = new WWWForm();
		form.AddField("CnumPost", Cnum);
		WWW www = new WWW(CardDeleteURL, form);
		yield return www;
		string CardDeleteboolean = www.text;
		Debug.Log("CardDeleteboolean:" + CardDeleteboolean);
	}
	#endif
}
