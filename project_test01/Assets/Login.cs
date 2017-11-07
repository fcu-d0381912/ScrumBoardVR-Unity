using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Login : MonoBehaviour {


	public string inputSSnname;
	public string inputPassword;
	public InputField account;
	public InputField password;
	private string answer="";
	string LoginURL = ("http://10.22.16.107/scrumboard/Login.php");

	// Use this for initialization
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}
	public void login(){
		StartCoroutine(LoginToDB(account.text, password.text));
		//if(answer.Equals("登入成功")){
			SceneManager.LoadScene (1);
		//}

	}
	IEnumerator LoginToDB(string SSnname, string password)
	{
		WWWForm form = new WWWForm();
		form.AddField("SSnnamePost", SSnname);
		form.AddField("passwordPost", password);
		WWW www = new WWW(LoginURL, form);
		yield return www;
		answer = www.text;
		Debug.Log(www.text);

	}
}
