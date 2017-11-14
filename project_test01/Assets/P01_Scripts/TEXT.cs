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
	public JsonData JsonCardData;
	public int JsonCardDatalength=0;

    public string LoginSsn;
	public string LoginPnum;

    public JsonData JsonCard;
    public int jsonCardlength = 0;
    //
    // public string srt;
    // Use this for initialization
    

    public void Generate()
    {
       
		PlayerPrefs.SetString("Ssn","ss");
		PlayerPrefs.SetString("Pnum","1");
        LoginSsn = PlayerPrefs.GetString("Ssn");
        LoginPnum= PlayerPrefs.GetString("Pnum");
        
        Pnum = int.Parse(LoginPnum);

        //ss +=0.1f;

        
		GameObject gobj = PhotonNetwork.Instantiate ("card", new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler (0f, 90f, 0f),0) as GameObject;


		gobj.GetComponentInChildren<Renderer>().material.color = GameObject.Find ("ColorIndicator").GetComponent<Renderer>().material.color;

		gobj.GetComponentInChildren<Text>().text = caedtext.text;

        gobj.name = "ni"+ss;
	

         Alpha = GameObject.Find("ColorIndicator").GetComponent<Renderer>().material.color.a;
         Red = GameObject.Find("ColorIndicator").GetComponent<Renderer>().material.color.r;
         Green = GameObject.Find("ColorIndicator").GetComponent<Renderer>().material.color.g;
         Blue = GameObject.Find("ColorIndicator").GetComponent<Renderer>().material.color.b;

        xLocation = gobj.transform.position.x;
        yLocation = gobj.transform.position.y;
        float zLocation = gobj.transform.position.z;

        StartCoroutine(CnumAutoCreate());
		//StartCoroutine(InsertCard(Cnum, caedtext.text, Pnum, LoginSsn, 4,xLocation, yLocation, Alpha, Red, Green, Blue));
    }

	public IEnumerator InsertCard(int Cnum, string Cstory, int Pnum,string CSsn, int Estimate,float xLocation, float yLocation,float ColorAlpha, float ColorRed, float ColorGreen, float ColorBlue)
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
        yield return www;

        string Cardboolean = www.text;
        Debug.Log("Cardboolean:" + Cardboolean);

        
    }


    public IEnumerator CnumAutoCreate()
    {
        //int CardCnumber;       
		WWW CardDataURL = new WWW("http://localhost/scrumboard/card/CardData.php");
        //CardCnumber = 0;
		yield return CardDataURL;
		//Debug.Log ("test"+cout++);
		string ItemsDataString = CardDataURL.text;
        

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
            //Debug.Log("Cnum:" + Cnum);
            Cnum = Cnum + 1;
            i++;
        }
        if (i == 0)
        {
            Cnum = 1;
        }
        StartCoroutine(InsertCard(Cnum, caedtext.text, Pnum, LoginSsn, 4, xLocation, yLocation, Alpha, Red, Green, Blue));
    }



	public void AutoGenerate(string Cstory,float xLocation,float yLocation,float Alpha,float Red,float Green,float Blue)
	{

		PlayerPrefs.SetString("Ssn","ss");
		PlayerPrefs.SetString("Pnum","1");
		LoginSsn = PlayerPrefs.GetString("Ssn");
		LoginPnum= PlayerPrefs.GetString("Pnum");

		Pnum = int.Parse(LoginPnum);

		//ss +=0.1f;

		GameObject gobj2 = PhotonNetwork.Instantiate ("card", new Vector3 (xLocation, yLocation, 2.782f), Quaternion.Euler (0f, 90f, 0f),0) as GameObject;


		Color color = new Color (Red, Green, Blue, Alpha);
		gobj2.GetComponentInChildren<MeshRenderer>().material.color = color;

		gobj2.GetComponentInChildren<Text> ().text = Cstory;

		gobj2.name = "ni"+ss;


		//StartCoroutine(InsertCard(Cnum, caedtext.text, Pnum, LoginSsn, 4,xLocation, yLocation, Alpha, Red, Green, Blue));
	}

	public IEnumerator ListPnumCard()
	{	
		PlayerPrefs.SetString("Pnum","0");

		string LoginPnum= PlayerPrefs.GetString("Pnum");
		string CardDataURL = "http://localhost/scrumboard/card/CardData.php";
		WWWForm form = new WWWForm();
		form.AddField("PnumPost", LoginPnum);
		WWW www = new WWW(CardDataURL, form);
		yield return www;

		string ItemsDataString = www.text;
		Debug.Log("ItemsDataString:" + ItemsDataString);
		//Debug.Log(ItemsDataString.Equals("\nNULL"));
		if (ItemsDataString.Equals("\nNULL"))
		{
			Debug.Log("NO have any card");
		}
		else
		{
			JsonCardData = JsonMapper.ToObject<JsonData>(ItemsDataString);
			JsonCardDatalength = JsonCardData.Count;
			Debug.Log("JsonProjectEmployee:" + JsonCardData.Count);
		}


		int i = 0;
		while (i < (JsonCardDatalength))
		{
			string CardCnumJsonData = JsonCardData[i]["Cnum"].ToString();
			string CardCstoryJsonData = JsonCardData[i]["Cstory"].ToString();
			string CardxLocationJsonData = JsonCardData[i]["xLocation"].ToString();
			string CardyLocationJsonData = JsonCardData[i]["yLocation"].ToString();
			string CardPnumJsonData = JsonCardData[i]["Pnum"].ToString();
			string CardCSsnJsonData = JsonCardData[i]["CSsn"].ToString();
			string CardEstimateJsonData = JsonCardData[i]["Estimate"].ToString();
			string CardAlphaJsonData = JsonCardData[i]["Alpha"].ToString();
			string CardRedJsonData = JsonCardData[i]["Red"].ToString();
			string CardGreenJsonData = JsonCardData[i]["Green"].ToString();
			string CardBlueJsonData = JsonCardData[i]["Blue"].ToString();


			Cnum = int.Parse(CardCnumJsonData);
			Cstory = CardCstoryJsonData;
			xLocation = float.Parse(CardxLocationJsonData);
			yLocation =  float.Parse(CardyLocationJsonData);
			Pnum = int.Parse(CardPnumJsonData);
			CSsn = CardCSsnJsonData;
			Estimate = int.Parse(CardEstimateJsonData);
			Alpha =  float.Parse(CardAlphaJsonData);
			Red = float.Parse(CardRedJsonData);
			Green =  float.Parse(CardGreenJsonData);
			Blue = float.Parse(CardBlueJsonData);


			//CanAddEmployee[i] = new ProjectEmployeeData(Pnumber, PSsn, PCharacter);

			Debug.Log("PnumCard Cnum:" + Cnum);
			Debug.Log("PnumCard Cstory:" + Cstory);
			Debug.Log("PnumCard xLocation:" + xLocation);
			Debug.Log("PnumCard yLocation:" + yLocation);
			Debug.Log("PnumCard Pnum:" + Pnum);
			Debug.Log("PnumCard CSsn:" + CSsn);
			Debug.Log("PnumCard Estimate:" + Estimate);
			Debug.Log("PnumCard Alpha:" + Alpha);
			Debug.Log("PnumCard Red:" + Red);
			Debug.Log("PnumCard Green:" + Green);
			Debug.Log("PnumCard Blue:" + Blue);
			AutoGenerate (Cstory, xLocation, yLocation, Alpha, Red, Green, Blue);
			i++;
		}

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

	public Card(int Cnum,string Cstory,float xLocation,float yLocation,int Pnum,string CSsn,int Estimate,float Alpha,float Red,float Green,float Blue){
		this.Cnum = Cnum;
		this.Cstory = Cstory;
		this.xLocation = xLocation;
		this.yLocation = yLocation;
		this.Pnum = Pnum;
		this.CSsn = CSsn;
		this.Estimate = Estimate;
		this.Alpha = Alpha;
		this.Red = Red;
		this.Green = Green;
		this.Blue = Blue;
		
	}
}