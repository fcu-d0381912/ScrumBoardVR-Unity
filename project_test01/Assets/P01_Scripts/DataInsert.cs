using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class DataInsert : MonoBehaviour
{

	public string inputFname;
	public string inputLname;
	public string inputSSnname;
	public string inputPassword;


	public InputField Fname;
	public InputField Lname;
	public InputField account;
	public InputField password;

	//string CreateUserURL = ("http://10.22.16.107/scrumboard/InsertUser.php");
	string CreateUserURL = ("http://localhost/scrumboard/InsertUser.php");
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		
	}
	public void register(){
		CreateUser (Fname.text, Lname.text, account.text, password.text);
	}

	public void CreateUser(string Fname, string Lname, string SSnname, string password)
	{
		WWWForm form = new WWWForm();
		form.AddField("FnamePost", Fname);
		form.AddField("LnamePost", Lname);
		form.AddField("SSnnamePost", SSnname);
		form.AddField("passwordPost", password);



		WWW www = new WWW(CreateUserURL, form);
	}
}
