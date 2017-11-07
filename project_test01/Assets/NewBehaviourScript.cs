using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NewBehaviourScript : MonoBehaviour {
    
    InputField s;
    public mananger boolmanager;
    // Use this for initialization
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter(Collision c)
    {
        
        if (boolmanager.first == false)
        {
            boolmanager.first = true;
            s = this.GetComponent<InputField>();
            
            s.ActivateInputField();
            Debug.Log("nbbb");
        }
        //s.gameObject.SetActive=true;
        
    }
}
