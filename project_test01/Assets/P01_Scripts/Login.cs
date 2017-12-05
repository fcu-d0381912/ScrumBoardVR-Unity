﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;
#if UNITY_EDITOR || UNITY_STANDALONE
public class Login : MonoBehaviour {


	public string inputSSnname;
	public butto LeftshowUi;
	public butto RightshowUi;
	private string account = "";
	private string password;
	private string answer="";
	private bool loginSuccess =false;
	//string LoginURL = ("http://10.22.16.107/scrumboard/Login.php");
	string LoginURL = ("http://140.134.26.71:12345/Login.php");

	// Use this for initialization
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}
	public void login(){
		StartCoroutine(LoginToDB(account, password));
	
	}
	IEnumerator  LoginToDB(string SSnname, string password)
	{
		Debug.Log(3);
		WWWForm form = new WWWForm();
		form.AddField("SSnnamePost", SSnname);
		form.AddField("passwordPost", password);
		WWW www = new WWW(LoginURL, form);
		Debug.Log(4);
		yield return www;
		Debug.Log(5);
		answer = www.text;

		Debug.Log(www.text);

		if(answer.Equals("login success")){
			PlayerPrefs.SetString ("Ssn", account);
			SceneManager.LoadScene (1);
			//loginSuccess = true;
		}
		else{
			Debug.Log ("2-2");
			if(!account.Equals("")){
				LeftshowUi.ShowError ();
				RightshowUi.ShowError ();
			}

		}



	}

	public void SetAccount(string account)
	{
		this.account = account;
	}
	public void SetPassword(string password)
	{
		this.password = password;
	}

	public bool GetLoginSuccess( )
	{
		return loginSuccess;
	}
}
#endif