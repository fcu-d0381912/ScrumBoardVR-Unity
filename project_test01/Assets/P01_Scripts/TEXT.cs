using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Photon;
using System;
using LitJson;
public class TEXT : PunBehaviour {
    public GameObject T;
	public Text caedtext;
	//Color color;
	//public GameObject cardtext;
    //public string s;
	string[] CardCnumItems;
	int cout = 0;
	
    float ss;

    //
    public int Cnum;
    public string Cstory;
    public float xLocation;
    public float yLocation;
    public int Pnum;
    public string CSsn;
    public int Estimate;
    public float Alpha;
    public float Red;
    public float Green;
    public float Blue;

    public JsonData JsonCard;
    public int jsonCardlength = 0;
    //
    // public string srt;
    // Use this for initialization
    public void Generate()
    {
       
		PlayerPrefs.SetString("Ssn","ss");
		PlayerPrefs.SetString("Pnum","1313");
        string LoginSsn = PlayerPrefs.GetString("Ssn");
        string LoginPnum= PlayerPrefs.GetString("Pnum");
        int Pnum = 0;
        Pnum = int.Parse(LoginPnum);
        //Network.Instantiate
        ss +=0.1f;

        
		GameObject gobj = PhotonNetwork.Instantiate ("card", new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler (0f, 90f, 0f),0) as GameObject;
		//gobj.GetComponent<Renderer>  = GameObject.Find ("ColorIndicator").GetComponent<Color> ().a;

		gobj.GetComponentInChildren<Renderer>().material.color = GameObject.Find ("ColorIndicator").GetComponent<Renderer>().material.color;
		//caedtext= GameObject.Find("").GetComponent<Text> ();
		gobj.GetComponentInChildren<Text>().text = caedtext.text;
		//gobj.GetComponentInChildren<Text>().text = cardtext.GetComponent<Speech>().stringtemp;
        gobj.name = "ni"+ss;
	
        //資料庫測試
        StartCoroutine(CnumAutoCreate());
		    //Cnum = getCnum (Cnum);
		    //print("回傳卡片值:"+Cnum);
		//Debug.Log (Cnum);
        //InsertCard(Cnum, caedtext.text, Pnum, LoginSsn, 4);
        //顏色測試
        float ColorAlpha = GameObject.Find("ColorIndicator").GetComponent<Renderer>().material.color.a;
        float ColorRed = GameObject.Find("ColorIndicator").GetComponent<Renderer>().material.color.r;
        float ColorGreen = GameObject.Find("ColorIndicator").GetComponent<Renderer>().material.color.g;
        float ColorBlue = GameObject.Find("ColorIndicator").GetComponent<Renderer>().material.color.b;
        //Debug.Log("顏色測試Alpha:"+ ColorAlpha);
        //Debug.Log("顏色測試Red:" + ColorRed);
        //Debug.Log("顏色測試Green:" + ColorGreen);
        //Debug.Log("顏色測試Blue:" + ColorBlue);
        //資料庫載入測試
        //InsertCardColor(ColorAlpha, ColorRed, ColorGreen, ColorBlue);
        //座標測試
        float xLocation = gobj.transform.position.x;
        float yLocation = gobj.transform.position.y;
        float zLocation = gobj.transform.position.z;
        //Debug.Log("座標測試xLocation:" + xLocation);
        //Debug.Log("座標測試yLocation:" + yLocation);
        //Debug.Log("座標測試zLocation:" + zLocation);
        //資料庫載入測試
        //InsertCardLocation(xLocation, yLocation);
		InsertCard(Cnum, caedtext.text, Pnum, LoginSsn, 4,xLocation, yLocation,ColorAlpha, ColorRed, ColorGreen, ColorBlue);
    }

	public void InsertCard(int Cnum, string Cstory, int Pnum,string CSsn, int Estimate,float xLocation, float yLocation,float ColorAlpha, float ColorRed, float ColorGreen, float ColorBlue)
    {
        string InsertCardURL = ("http://localhost/scrumboard/card/CardAllInsert.php");
        WWWForm form = new WWWForm();
        //pname?
        form.AddField("CnumPost", Cnum);
        form.AddField("CstoryPost", Cstory);
        form.AddField("PnumPost", Pnum);
        form.AddField("CSsnPost", CSsn);
        form.AddField("EstimatePost", Estimate);

		form.AddField("xLocationPost", xLocation.ToString("0.0000"));
		form.AddField("yLocationPost", yLocation.ToString("0.0000"));

		form.AddField("AlphaPost", ColorAlpha.ToString("0.0000"));
		form.AddField("RedPost", ColorRed.ToString("0.0000"));
		form.AddField("GreenPost", ColorGreen.ToString("0.0000"));
		form.AddField("BluePost", ColorBlue.ToString("0.0000"));
        WWW www = new WWW(InsertCardURL, form);
		Debug.Log ("insertcard");
    }


    public IEnumerator CnumAutoCreate()
    {
        //int CardCnumber;       
        WWW CardCnum = new WWW("http://localhost/scrumboard/card/CardData.php");
        //CardCnumber = 0;
        yield return CardCnum;
		//Debug.Log ("test"+cout++);
        string ItemsDataString = CardCnum.text;
        CardCnumItems = ItemsDataString.Split(';');

        if (ItemsDataString.Equals("NULL"))
        {
            Debug.Log("No card");
        }
        else
        {
            JsonCard = JsonMapper.ToObject<JsonData>(ItemsDataString);
            jsonCardlength = JsonCard.Count;
            Debug.Log("jsonProjectlength:" + JsonCard.Count);
        }
        int i = 0;
        while (i < (jsonCardlength))
        {

            string CardJsonData = JsonCard[i]["Cnum"].ToString();


            Cnum = int.Parse(CardJsonData);
            Debug.Log("Cnum:" + Cnum);
            Cnum = Cnum + 1;
            i++;
        }
        if (i == 0)
        {
            Cnum = 1;
        }
    }


	public void InsertCardLocation(float xLocation, float yLocation)
	{
		string InsertCardLocationURL = ("http://localhost/scrumboard/card/CardLocationInsert.php");
		WWWForm form = new WWWForm();
		form.AddField("xLocationPost", xLocation.ToString("0.0000"));
		form.AddField("yLocationPost", yLocation.ToString("0.0000"));

		WWW www = new WWW(InsertCardLocationURL, form);
	}
	public void InsertCardColor(float ColorAlpha, float ColorRed, float ColorGreen, float ColorBlue)
	{
		string InsertCardColorURL = ("http://localhost/scrumboard/card/CardColorInsert.php");
		WWWForm form = new WWWForm();
		form.AddField("AlphaPost", ColorAlpha.ToString("0.0000"));
		form.AddField("RedPost", ColorRed.ToString("0.0000"));
		form.AddField("GreenPost", ColorGreen.ToString("0.0000"));
		form.AddField("BluePost", ColorBlue.ToString("0.0000"));

		WWW www = new WWW(InsertCardColorURL, form);
	}
}

class Card
{
    public int Cnum;
    public string Cstory;
    public float xLocation;
    public float yLocation;
    public int Pnum;
    public string CSsn;
    public int Estimate;
    public float Alpha;
    public float Red;
    public float Green;
    public float Blue;
}