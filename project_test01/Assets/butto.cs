using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class butto : MonoBehaviour {
    Button button;
    public mananger boolmanager;
    
	// Use this for initialization
	void Start () {
        button = this.GetComponent<Button>();


    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter(Collision c)
    {
        //button = this.GetComponent<Button>();
        if(boolmanager.havetext == false)
        {
            button.onClick.Invoke();
            Debug.Log("button click");
            boolmanager.havetext = true;
        }
        
        
        //s.gameObject.SetActive=true;

    }
	void OnCollisionExit(Collision c)
	{
		//button = this.GetComponent<Button>();
		boolmanager.havetext = false;
		Debug.Log("LEAVE");


		//s.gameObject.SetActive=true;

	}
}
