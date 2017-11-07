using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;
using UnityEngine.EventSystems;
using System;
using UnityEngine.SceneManagement;
public class RoomConnect : PunBehaviour {
	string TEMP;
	public string[] Items;
	public string person;
	int x;

	public Button showCreat;
	public GameObject creatRoomUi;

	public GameObject copyGameObject;//要被複製的物件
	public GameObject superGameObject;//要被放置在哪個物件底下
	private GameObject childGameObject;
	private string projectName;




	public GameObject copyPerson;//要被複製的物件
	public GameObject PersonList;//要被放置在哪個物件底下
	private GameObject personGameObject;
	private string personName;
	// Use this for initialization
	void Start () 
	{
		projectName = "test";
		PhotonNetwork.ConnectUsingSettings("1.0");


		StartCoroutine(InstantPerson ());
		LoadDatabase ();
	}

	// Update is called once per frame
	void Update () 
	{
		if (TEMP != PhotonNetwork.connectionStateDetailed.ToString())
		{
			TEMP = PhotonNetwork.connectionStateDetailed.ToString();
			if (PhotonNetwork.insideLobby)
			{
				Debug.Log("inlobby");

			}
			else if(PhotonNetwork.inRoom){
				Debug.Log(PhotonNetwork.room.Name);
			}
			Debug.Log(TEMP);
		}


	}

	public override void OnConnectedToMaster()
	{
		//OnJoinedLobby();
		PhotonNetwork.JoinLobby();
	}

	public void ShowUpdate()
	{
		creatRoomUi.SetActive (true);
		showCreat.gameObject.SetActive (false);
	}


	//專案名讀取
	private void LoadDatabase()
	{
		int i = 0;
		while( i<=3)
		{
			projectName = i.ToString();
			InstantProject(projectName);
			i++;
		}
	}

	//創建專案
	public void CreatRoom()
	{
		projectName = creatRoomUi.GetComponentInChildren<InputField> ().text;
		//projectName丟到資料庫
		InstantProject(projectName);
	}


	private void InstantProject(string projectName)
	{
		childGameObject = Instantiate (copyGameObject);
		childGameObject.transform.SetParent(superGameObject.transform);
		childGameObject.GetComponentInChildren<Text>().text = projectName;
		Button s = childGameObject.GetComponentInChildren<Button> ();
		s.onClick.AddListener(delegate {
			GetRoom(s);
		});
	}
		
	private void GetRoom(Button thisGameObject)
	{
		string roomName = "default";
		GameObject game = thisGameObject.gameObject;
		roomName = game.transform.parent.GetComponentInChildren<Text>().text;
		RoomOptions option = new RoomOptions ();
		PhotonNetwork.JoinOrCreateRoom (roomName,option,TypedLobby.Default);
		SceneManager.LoadScene (2);
	}


	//載入員工列表
	private IEnumerator InstantPerson()
	{
		WWW ItemsData = new WWW("http://10.22.28.42/scrumboard/ItemsData.php");
		yield return ItemsData;
		string ItemsDataString = ItemsData.text;
		Items = ItemsDataString.Split (';');
		int i = 0;
		while (!Items [i].Equals ("NULL")) {
			person = GetDataValue (Items [i], "Lname:", "Fname:", "SSn:");


			personName = person;

			personGameObject = Instantiate (copyPerson);
			personGameObject.transform.SetParent (PersonList.transform);
			personGameObject.GetComponentInChildren<Text> ().text = personName;
			i++;
		}

		
	}

	//取員工編號，姓和名
	private string GetDataValue(string data,string LnameIndex,string FnameIndex,string SSnindex)
	{
		string Lname = data.Substring (data.IndexOf (LnameIndex) + LnameIndex.Length);
		string Fname = data.Substring (data.IndexOf (FnameIndex) + FnameIndex.Length);
		string SSn = data.Substring (data.IndexOf (SSnindex) + SSnindex.Length);
		if(Lname.Contains("|")){
			Lname = Lname.Remove (Lname.IndexOf ("|"));
		}
		if(Fname.Contains("|")){
			Fname = Fname.Remove (Fname.IndexOf ("|"));
		}

		if(SSn.Contains("|")){
			SSn = SSn.Remove (SSn.IndexOf ("|"));
		}
		//print (value);
		//print (value2);
		return SSn+Lname+Fname;
	}

	/*private IEnumerator GetPerson(string person){

		WWW ItemsData = new WWW("http://10.22.28.42/scrumboard/ItemsData.php");
		yield return ItemsData;
		string ItemsDataString = ItemsData.text;
		Items = ItemsDataString.Split (';');
		person=GetDataValue(Items[0],"Lname:","Fname:","SSn:");


	}*/
}