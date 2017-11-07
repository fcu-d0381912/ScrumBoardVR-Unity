using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TEXT : MonoBehaviour {
    public GameObject T;
	public Text caedtext;
	//public GameObject cardtext;
    //public string s;
  
    float ss;
   // public string srt;
    // Use this for initialization
    public void ge()
    {
        
        ss+=0.1f;
		//T.gameObject.GetComponent<Text>().text = caedtext.text;
        //GameObject gobj = Instantiate(T, new Vector3(3.29f,ss, 2.39f), Quaternion.Euler(-90f, 0f, 0f)) as GameObject;
        GameObject gobj = Instantiate(T, new Vector3(this.transform.position.x, this.transform.position.y+1, this.transform.position.z+ss), Quaternion.Euler(0f, 0f, 0f)) as GameObject;
		//caedtext= GameObject.Find("").GetComponent<Text> ();
		gobj.GetComponentInChildren<Text>().text = caedtext.text;
		//gobj.GetComponentInChildren<Text>().text = cardtext.GetComponent<Speech>().stringtemp;
        gobj.name = "ni"+ss;

    }
    // public void OnMouseEnter()
    //  {
    //     T.text = tt.text;

    // }


}
