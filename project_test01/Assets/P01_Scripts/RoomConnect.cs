using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;
using UnityEngine.EventSystems;
using System;
using UnityEngine.SceneManagement;
using LitJson;
public class RoomConnect : PunBehaviour
{
    string TEMP;
    //public string[] Items;
    private string person;
    int x;

    public Button showCreat;
    public GameObject creatRoomUi;
    public GameObject employeeList;

    public GameObject ProjectList;


    public GameObject copyGameObject;//要被複製的物件
    public GameObject superGameObject;//要被放置在哪個物件底下
    private GameObject childGameObject;
    private string projectName;

    //string CreateProjectURL = ("http://10.22.28.42/scrumboard/ProjectInsert.php");
	string CreateProjectURL = ("http://140.134.26.71:12345/ProjectInsert.php");
    public string[] ProjectItems;
    public string[] ProjectListItems;
    public string[] ManagerProjectItems;
    //public int projectnumber;
    //public int project;

	private string UpdateProjectAuthorityURL = ("http://140.134.26.71:12345/UpdateProjectManagerAuthority.php");
	private string InsertProjectCharacterURL = ("http://140.134.26.71:12345/InsertProjectCharacter.php");

	private string ShowCanGoToProjectURL = ("http://140.134.26.71:12345/ShowCanGoToProject.php");
	private string ShowManagerProjectURL = ("http://140.134.26.71:12345/ShowManagerProject.php");
    private string LoginSsn;
    

    public GameObject copyPerson;//要被複製的物件
    public GameObject PersonList;//要被放置在哪個物件底下
    private GameObject personGameObject;

    
    private string personName;
    // Use this for initialization

    //
    private int Pnum;
    private string Pname;
    private string Super_SSn;
    private JsonData Json;
    private int jsonProjectlength = 0;

	private JsonData LoadCanGoToJson;
	private int jsonLoadCanGoToProjectlength= 0;
    //
    private int Authority;
    private string Fname;
    private string Lname;
    private string Ssn;
    private string Password;
    private int jsonEmployeelength=0;
    private JsonData JsonEmployee;
    //
    private int Pnumber;
    private string PSsn;
    private string PCharacter;
    private int jsonProjectEmployeelength = 0;
    private JsonData JsonProjectEmployee;
    private ProjectEmployeeData[] CanGoToProject=new ProjectEmployeeData[20];

    private int jsonProjectManagerlength = 0;
    private JsonData JsonProjectManager;
    private ProjectEmployeeData[] CanAddEmployee = new ProjectEmployeeData[20];

	public GameObject movePanel;

	public ScrollProject leftScroll;
	public ScrollProject rightScroll;
    void Start()
    {
        projectName = "test";
        PhotonNetwork.ConnectUsingSettings("1.0");
        //StartCoroutine(CountPerson());
        //StartCoroutine(ShowCanGoToProject());
        StartCoroutine(ShowManagerProject());
        
        StartCoroutine(InstantPerson());
		StartCoroutine(LoadCanGoToDatabase());
       

    }

    // Update is called once per frame
    void Update()
    {
        if (TEMP != PhotonNetwork.connectionStateDetailed.ToString())
        {
            TEMP = PhotonNetwork.connectionStateDetailed.ToString();
            if (PhotonNetwork.insideLobby)
            {
                Debug.Log("inlobby");

            }
            else if (PhotonNetwork.inRoom)
            {
				
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
        creatRoomUi.SetActive(true);
        showCreat.gameObject.SetActive(false);
    }

    
    //專案名讀取
    public IEnumerator LoadDatabase()
    {
		WWW ItemsProjectJson = new WWW("http://140.134.26.71:12345/ItemsProject.php");


        yield return ItemsProjectJson;
        string ItemsDataString = ItemsProjectJson.text;
        if (ItemsDataString.Equals("NULL"))
        {
            Debug.Log("No project");
        }
        else
        {
            Json = JsonMapper.ToObject<JsonData>(ItemsDataString);
            jsonProjectlength= Json.Count;
        }

        //Json.Count
        //string ProjectJsonData = Json[1]["Pnum"].ToString();

      
        int i = 0; 
        while (i < (jsonProjectlength))
        {

            //PnumString = ProjectItems[i].Substring(ProjectItems[i].IndexOf("id\":\"") + "id\":\"".Length);

            string ProjectPnumJsonData = Json[i]["Pnum"].ToString();
            string ProjectPnameJsonData = Json[i]["Pname"].ToString();
            string ProjectSuperSSnJsonData = Json[i]["Super_SSn"].ToString();
            Pnum = int.Parse(ProjectPnumJsonData);
            Pname = ProjectPnameJsonData;
            Super_SSn = ProjectSuperSSnJsonData;
            
            projectName = "NO." + Pnum + "   " + Pname + "\nProject Manager:" + Super_SSn;
            Debug.Log("projectName:" + projectName);
            
            //InstantProjectEmployee(projectName, Super_SSn, Pnum);
			InstantProject(Pname,Pnum,Super_SSn);
			//GoToProject set


            i++;
            //}
        }
		leftScroll.SetButton ();
		rightScroll.SetButton ();
    }
		
	//讀取能進入的專案
	public IEnumerator LoadCanGoToDatabase()
	{

		string LoadCanGoToDatabaseURL = "http://140.134.26.71:12345/LoadCanGoToDatabase.php";
		LoginSsn = PlayerPrefs.GetString("Ssn");
		WWWForm form = new WWWForm();
		form.AddField("SSnnamePost", LoginSsn);
		WWW www = new WWW(LoadCanGoToDatabaseURL, form);
		yield return www;
		string ItemsDataString = www.text;
		Debug.Log("ItemsDataString:" + ItemsDataString);
		if (ItemsDataString.Equals("NULL"))
		{
			Debug.Log("No project");
		}
		else
		{
			LoadCanGoToJson = JsonMapper.ToObject<JsonData>(ItemsDataString);
			jsonLoadCanGoToProjectlength= LoadCanGoToJson.Count;
		}

		int i = 0;
		while (i < (jsonLoadCanGoToProjectlength))
		{

			//PnumString = ProjectItems[i].Substring(ProjectItems[i].IndexOf("id\":\"") + "id\":\"".Length);

			string LoadCanGoToProjectPnumJsonData = LoadCanGoToJson[i]["Pnum"].ToString();
			string LoadCanGoToProjectPnameJsonData = LoadCanGoToJson[i]["Pname"].ToString();
			string LoadCanGoToProjectSuperSSnJsonData = LoadCanGoToJson[i]["Super_SSn"].ToString();
			Pnum = int.Parse(LoadCanGoToProjectPnumJsonData);
			Pname = LoadCanGoToProjectPnameJsonData;
			Super_SSn = LoadCanGoToProjectSuperSSnJsonData;

			projectName = "NO." + Pnum + "   " + Pname + "\nProject Manager:" + Super_SSn;
			Debug.Log("projectName:" + projectName);

			//InstantProjectEmployee(projectName, Super_SSn, Pnum);
			InstantProject(Pname,Pnum,Super_SSn);
			//GoToProject set
			i++;
			//}
		}
		leftScroll.SetButton ();
		rightScroll.SetButton ();
	}
    //創建專案
    public void CreatRoom()
    {

        projectName = creatRoomUi.GetComponentInChildren<InputField>().text;
        //projectName丟到資料庫

        //StartCoroutine(Projectnumber(projectName));

       // StartCoroutine(ProjectnumberJson(projectName));

        creatRoomUi.SetActive(false);
        showCreat.gameObject.SetActive(true);

    }

    //創建專案
    public IEnumerator CreateProject(string Pname, int Pnum, string Super_SSn)
    {
        WWWForm form = new WWWForm();
        form.AddField("PnamePost", Pname);
        form.AddField("PnumPost", Pnum);
        form.AddField("SuperSSnPost", Super_SSn);
        WWW www = new WWW(CreateProjectURL, form);
        yield return www;

        string CreateProjectboolean = www.text;
        Debug.Log("CreateProjectboolean:" + CreateProjectboolean);
    }
 
    //權限升級
    public IEnumerator UpdateProjectAuthority(string Ssn)
    {
        WWWForm form = new WWWForm();
        form.AddField("SSnnamePost", Ssn);
        WWW www = new WWW(UpdateProjectAuthorityURL, form);

        yield return www;

        string UpdateProjectAuthorityboolean = www.text;
        Debug.Log("UpdateProjectAuthorityboolean:" + UpdateProjectAuthorityboolean);

    }
    //專案角色改變
    public IEnumerator InsertProjectCharacter(string PSsn, int Pnumber, string PCharacter)
    {
        WWWForm form = new WWWForm();
        form.AddField("PSsnPost", PSsn);
        form.AddField("PnumPost", Pnumber);
        form.AddField("PCharacterPost", PCharacter);

        WWW www = new WWW(InsertProjectCharacterURL, form);
        yield return www;

        string ProjectCharacterboolean = www.text;
        Debug.Log("ProjectCharacterboolean:" + ProjectCharacterboolean);

    }

    //新增專案用
	private void InstantProject(String Pname,int pNum,string Super_SSn)
    {
		childGameObject = Instantiate(copyGameObject);
		childGameObject.transform.SetParent(movePanel.transform);
		childGameObject.transform.localScale=new Vector3(1,1,1);
		childGameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(pNum*100,0,0);

        //考慮要不要加PNUM
		childGameObject.GetComponentsInChildren<Text>()[0].text = Pname;
		childGameObject.GetComponentsInChildren<Text>()[1].text = Super_SSn;
		childGameObject.name = pNum.ToString ();
        //Button GoToProject = childGameObject.GetComponentsInChildren<Button>()[0];
        //Button AddEmployee = childGameObject.GetComponentsInChildren<Button>()[1];

       // AddEmployee.interactable = true;
       // GoToProject.interactable = true;
       // GoToProject.onClick.AddListener(delegate {

		//GetRoom(pNum);
       // });
        //AddEmployee.onClick.AddListener(delegate {

       //     CheckToggle(AddEmployee);
       // });
    }
    //判斷按鈕是否生成用
    /*private void InstantProjectEmployee(string projectName,string Super_SSn,int Pnum)
    {
        LoginSsn = PlayerPrefs.GetString("Ssn");
        childGameObject = Instantiate(copyGameObject);
        childGameObject.transform.SetParent(superGameObject.transform);
        //考慮要不要加PNUM
        childGameObject.GetComponentInChildren<Text>().text = projectName;
        Button GoToProject = childGameObject.GetComponentsInChildren<Button>()[0];
        Button AddEmployee = childGameObject.GetComponentsInChildren<Button>()[1];
        int i = 0;
        while (i < (jsonProjectEmployeelength))
        {

            Debug.Log("Pnum:" + Pnum + " CanGoToProject[i].Pnumber:" + CanGoToProject[i].Pnumber);
            if (Pnum.Equals(CanGoToProject[i].Pnumber) && GoToProject.interactable == false)
            {
                //allows the start button to be used
                GoToProject.interactable = true;
                //break;
            }
            i++;
        }
        i = 0;
        while (i < (jsonProjectManagerlength))
        {

            Debug.Log("Super_SSn:" + Super_SSn + " CanAddEmployee[i].PSsn:" + CanAddEmployee[i].PSsn);
            if (Super_SSn.Equals(CanAddEmployee[i].PSsn) && AddEmployee.interactable == false)
            {
                //allows the start button to be used
                AddEmployee.interactable = true;
                //break;
            }
            i++;
        }
        
        GoToProject.onClick.AddListener(delegate {
            GetRoom(GoToProject);
        });
        AddEmployee.onClick.AddListener(delegate {

            CheckToggle(AddEmployee);
        });
    }*/
    //列出一個人能去的案子
    public IEnumerator ShowCanGoToProject()
    {
        LoginSsn = PlayerPrefs.GetString("Ssn");
        WWWForm form = new WWWForm();
        form.AddField("SSnnamePost", LoginSsn);
        WWW www = new WWW(ShowCanGoToProjectURL, form);
        yield return www;

        string ItemsDataString = www.text;
        Debug.Log("ItemsDataString:" + ItemsDataString);
        if (ItemsDataString.Equals("NULL"))
        {
            Debug.Log("Doesn't enter any Project");
        }
        else
        {
            JsonProjectEmployee = JsonMapper.ToObject<JsonData>(ItemsDataString);
            jsonProjectEmployeelength = JsonProjectEmployee.Count;
            Debug.Log("JsonProjectEmployee:" + JsonProjectEmployee.Count);
        }


            int i = 0;
            while (i < (jsonProjectEmployeelength))
            {
                string ProjectEmployeePnumberJsonData = JsonProjectEmployee[i]["Pnumber"].ToString();
                string ProjectEmployeePSsnJsonData = JsonProjectEmployee[i]["PSsn"].ToString();
                string ProjectEmployeePCharacterJsonData = JsonProjectEmployee[i]["PCharacter"].ToString();
                Pnumber = int.Parse(ProjectEmployeePnumberJsonData);
                PSsn = ProjectEmployeePSsnJsonData;
                PCharacter = ProjectEmployeePCharacterJsonData;
                CanGoToProject[i] = new ProjectEmployeeData(Pnumber, PSsn, PCharacter);
                Debug.Log("PSsn:" + PSsn);
                Debug.Log("Pnumber:" + Pnumber);
                i++;
            }
        

    }
    //列出主管有幾個案子 
    public IEnumerator ShowManagerProject()
    {
        LoginSsn = PlayerPrefs.GetString("Ssn");
        WWWForm form = new WWWForm();
        form.AddField("SSnnamePost", LoginSsn);
        WWW www = new WWW(ShowManagerProjectURL, form);
        yield return www;

        string ItemsDataString = www.text;
        Debug.Log("ItemsDataString:" + ItemsDataString);
        //Debug.Log(ItemsDataString.Equals("\nNULL"));
        if (ItemsDataString.Equals("\nNULL"))
        {
            Debug.Log("You are not Manager");
        }
        else
        {
            JsonProjectManager = JsonMapper.ToObject<JsonData>(ItemsDataString);
            jsonProjectManagerlength = JsonProjectManager.Count;
            Debug.Log("JsonProjectEmployee:" + JsonProjectManager.Count);
        }


            int i = 0;
            while (i < (jsonProjectManagerlength))
            {
                string ProjectEmployeePnumberJsonData = JsonProjectManager[i]["Pnumber"].ToString();
                string ProjectEmployeePSsnJsonData = JsonProjectManager[i]["PSsn"].ToString();
                string ProjectEmployeePCharacterJsonData = JsonProjectManager[i]["PCharacter"].ToString();
                Pnumber = int.Parse(ProjectEmployeePnumberJsonData);
                PSsn = ProjectEmployeePSsnJsonData;
                PCharacter = ProjectEmployeePCharacterJsonData;
                CanAddEmployee[i] = new ProjectEmployeeData(Pnumber, PSsn, PCharacter);
                Debug.Log("Manager PSsn:" + PSsn);
                Debug.Log("Manager Pnumber:" + Pnumber);
                i++;
            }
        
    }


	public void GetRoom(int pNum)
    {
        //string roomName = "default";
        //string PnumName;
        //GameObject game = thisGameObject.gameObject;
        //roomName = game.transform.parent.GetComponentInChildren<Text>().text;
        //重要
        //PnumName = roomName.Substring(3, roomName.IndexOf(" "));
		PlayerPrefs.SetString("Pnum", pNum.ToString());
		Debug.Log("Go to NO." + pNum + " project");
        RoomOptions option = new RoomOptions();
		PhotonNetwork.JoinOrCreateRoom(pNum.ToString(), option, TypedLobby.Default);
        SceneManager.LoadScene(2);
    }


    //載入員工列表
    private IEnumerator InstantPerson()
    {
        //WWW ItemsData = new WWW("http://10.22.28.42/scrumboard/ItemsData.php");

		WWW ItemsData = new WWW("http://140.134.26.71:12345/ItemsData.php");

        yield return ItemsData;
        string ItemsDataString = ItemsData.text;
        if (ItemsDataString.Equals("NULL"))
        {
            Debug.Log("No Employee");
        }
        else
        {
            JsonEmployee = JsonMapper.ToObject<JsonData>(ItemsDataString);
            jsonEmployeelength = JsonEmployee.Count;
            Debug.Log("jsonProjectlength:" + JsonEmployee.Count);
        }


            int i = 0;
            while (i < (jsonEmployeelength))
            {
                string EmployeeSSnJsonData = JsonEmployee[i]["SSn"].ToString();
                string EmployeeLnameJsonData = JsonEmployee[i]["Lname"].ToString();
                string EmployeeFnameJsonData = JsonEmployee[i]["Fname"].ToString();
                Ssn = EmployeeSSnJsonData;
                Lname = EmployeeLnameJsonData;
                Fname = EmployeeFnameJsonData;

                person = Ssn + " " + Lname + Fname;
                ;
                //print(person);
                personName = person;

                personGameObject = Instantiate(copyPerson);
                personGameObject.transform.SetParent(PersonList.transform);
                personGameObject.GetComponentInChildren<Text>().text = personName;
                i++;
            }

        
    }


    public void CheckToggle(Button thisGameObject)
    {
        int i = 1;
        int jumpToggleText = 1;//要跳過toggle裡的lable
        int thisPnum = 0;
        string PnumName = "default";

        LoginSsn = PlayerPrefs.GetString("Ssn");
        GameObject game = thisGameObject.gameObject;
        PnumName = game.transform.parent.GetComponentInChildren<Text>().text;
        print(PnumName);
        PnumName = PnumName.Substring(3, PnumName.IndexOf(" "));
        print(PnumName);
        thisPnum = int.Parse(PnumName);
        print(thisPnum);
        print(LoginSsn + "登錄了");
        while (i <= (jsonEmployeelength))
        {
            Text employeeText = employeeList.GetComponentsInChildren<Text>()[i + jumpToggleText];
            Toggle employeeToggle = employeeList.GetComponentsInChildren<Toggle>()[i];

            string employeelist = employeeText.text;

            employeelist = employeelist.Substring(0, 6);


            if (employeeToggle.isOn)
            {
                if (LoginSsn.Equals(employeelist))
                {
                    Debug.Log("Faild");
                }
                else
                {
                    InsertProjectCharacter(employeelist, thisPnum, "TeamMember");
                    Debug.Log(employeelist + "ok");
                }
            }
            i++;
            jumpToggleText++;
        }

    }
    //testJson

    /*private IEnumerator ProjectnumberJson(string projectName)
    {

        //WWW ItemsProject = new WWW("http://10.22.28.42/scrumboard/ItemsProject.php");

        WWW ItemsProjectJson = new WWW("http://localhost/scrumboard/ItemsProject.php");

        yield return ItemsProjectJson;
        string ItemsDataString = ItemsProjectJson.text;
        if (ItemsDataString.Equals("NULL"))
        {
            Debug.Log("No project");
        }
        else
        {
            Json = JsonMapper.ToObject<JsonData>(ItemsDataString);
            jsonProjectlength = Json.Count;
            Debug.Log("jsonProjectlength:" + Json.Count);
        }
            //Json.Count
            //string ProjectJsonData = Json[1]["Pnum"].ToString();

            //JsonData json2 = JsonMapper.ToObject<JsonData>(json);
            ProjectItems = ItemsDataString.Split('|');

            int ProjectLength = ProjectItems.Length;
            print(ProjectLength);
            int i = 0;
            LoginSsn = PlayerPrefs.GetString("Ssn");

            while (i < (jsonProjectlength))
            {

                //PnumString = ProjectItems[i].Substring(ProjectItems[i].IndexOf("id\":\"") + "id\":\"".Length);
                string ProjectJsonData = Json[i]["Pnum"].ToString();

                //PnumString = PnumString.Remove(PnumString.IndexOf("\""));
                ;
                Pnum = int.Parse(ProjectJsonData);
                Debug.Log("Pnum:" + Pnum);
                Pnum = Pnum + 1;

                i++;
                //}
            }

            if (i == 0)
            {
                Pnum = 1;
            }

            Pname = projectName;
            Super_SSn = LoginSsn;
            //JsonProjectDataSave();
            //StartCoroutine(InsertProjectJson());

            //print(project);
            StartCoroutine(CreateProject(Pname, Pnum, Super_SSn));
            string projecttostring = Convert.ToString(Pnum);
            projectName = "NO." + projecttostring + "   " + projectName + "         Project Manager:" + Super_SSn;
            InstantProject(projectName);
            //權限升級
            StartCoroutine(UpdateProjectAuthority(Super_SSn));
            //專案角色改變
            StartCoroutine(InsertProjectCharacter(Super_SSn, Pnum, "ScrumMaster"));
        }*/
    
}

class ProjectData
{
    public int Pnum;
    public string Pname;
    public string Super_SSn;
    public ProjectData(int Pnum, string Pname, string Super_SSn)
    {
        this.Pnum = Pnum;
        this.Pname = Pname;
        this.Super_SSn = Super_SSn;
    }
}

class EmployeeData
{
    public int Authority;
    public string Fname;
    public string Lname;
    public string Ssn;
    public string Password;

    public EmployeeData(int Authority, string Fname, string Lname, string Ssn,string Password)
    {
        this.Authority = Authority;
        this.Fname = Fname;
        this.Lname = Lname;
        this.Ssn = Ssn;
        this.Password = Password;
    }

}

class ProjectEmployeeData
{
    public int Pnumber;
    public string PSsn;
    public string PCharacter;

    public ProjectEmployeeData()
    {

    }

    public ProjectEmployeeData(int Pnumber, string PSsn, string PCharacter)
    {
        this.Pnumber = Pnumber;
        this.PSsn = PSsn;
        this.PCharacter = PCharacter;
    }
}

