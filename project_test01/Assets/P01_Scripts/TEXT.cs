using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Photon;
using System;
using LitJson;
public class TEXT : PunBehaviour {
    public GameObject T;
	public Text cardtitle;
	public Text cardText;
	public Text cardaSsn;
	public Text cardeTime;
	public Text cardEstimate;
	//Color color;
	//public GameObject cardtext;
    //public string s;


	
    float ss;

    //
    public int Cnum;
    public string Ctitle;
	public string Ctext;


    public float xLocation;
    public float yLocation;
    public int Pnum;

    public string CSsn;
	public string ASsn;

	public int Estimate;
	public int Etime;

    public float Alpha;
    public float Red;
    public float Green;
    public float Blue;
	public JsonData JsonCardData;  
    public int JsonCardDatalength=0;

    public JsonData JsonCnumCardData;
    public int JsonCnumCardDatalength = 0;

	private GameObject cloneCard ;
    public string LoginSsn;
	public string LoginPnum;

    public JsonData JsonCard;
    public int jsonCardlength = 0;
    //
    // public string srt;
    // Use this for initialization
	private Card cardComponent ;

    public void Generate()
    {
       
		//PlayerPrefs.SetString("Ssn","ss");
		//PlayerPrefs.SetString("Pnum","1");
        LoginSsn = PlayerPrefs.GetString("Ssn");
        LoginPnum= PlayerPrefs.GetString("Pnum");
        
        Pnum = int.Parse(LoginPnum);

        //ss +=0.1f;

        
		cloneCard = PhotonNetwork.Instantiate ("card", new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler (0f, 90f, 0f),0) as GameObject;

		cloneCard.GetComponentInChildren<Renderer>().material.color = GameObject.Find ("ColorIndicator").GetComponent<Renderer>().material.color;

		cloneCard.GetComponentsInChildren<Text>()[0].text = cardtitle.text;

		Ctext= cardText.text;
		ASsn = cardaSsn.text;
		Etime =int.Parse(cardeTime.text);
		Estimate=int.Parse(cardEstimate.text);

		cloneCard.GetComponentsInChildren<Text>()[2].text = ASsn;
		cloneCard.GetComponentsInChildren<Text>()[3].text = Etime.ToString()+" h";


         Alpha = GameObject.Find("ColorIndicator").GetComponent<Renderer>().material.color.a;
         Red = GameObject.Find("ColorIndicator").GetComponent<Renderer>().material.color.r;
         Green = GameObject.Find("ColorIndicator").GetComponent<Renderer>().material.color.g;
         Blue = GameObject.Find("ColorIndicator").GetComponent<Renderer>().material.color.b;

		xLocation = cloneCard.transform.position.x;
		yLocation = cloneCard.transform.position.y;
		float zLocation = cloneCard.transform.position.z;

        StartCoroutine(CnumAutoCreate());
		//gobj.GetComponentsInChildren<Text>()[1].text =Cnum;
		//StartCoroutine(InsertCard(Cnum, caedtext.text, Pnum, LoginSsn, 4,xLocation, yLocation, Alpha, Red, Green, Blue));
    }

	public IEnumerator InsertCard(int Cnum, string Ctitle,string Ctext, int Pnum,string CSsn,string ASsn ,int Estimate,int Etime,float xLocation, float yLocation,float ColorAlpha, float ColorRed, float ColorGreen, float ColorBlue)
    {
        string InsertCardURL = ("http://localhost/scrumboard/card/CardAllInsert.php");
        WWWForm form = new WWWForm();
        //pname?
        form.AddField("CnumPost", Cnum);


		form.AddField("CtitlePost", Ctitle);
		form.AddField("CtextPost", Ctext);

		form.AddField("EtimePost", Etime);

        form.AddField("PnumPost", Pnum);

        form.AddField("CSsnPost", CSsn);
		form.AddField("ASsnPost", ASsn);

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
		cloneCard.name = "card"+Cnum;
		cloneCard.GetComponentsInChildren<Text>()[1].text =Cnum.ToString();
		cardComponent = cloneCard.GetComponent<Card> ();
		cardComponent.setCard (Cnum, Ctitle, Ctext, xLocation, yLocation, Pnum, LoginSsn, ASsn, Estimate, Etime, Alpha, Red, Green, Blue);
		StartCoroutine(InsertCard (Cnum, cardtitle.text, Ctext, Pnum, LoginSsn, ASsn, Estimate, Etime, xLocation, yLocation, Alpha, Red, Green, Blue));
		//InsertCard (Cnum, caedtext.text, Ctext, Pnum, LoginSsn, ASsn, Estimate, Etime, xLocation, yLocation, Alpha, Red, Green, Blue);

    }

	public IEnumerator UpdateLocation(string Cnum,float xLocation,float yLocation){
		string UpdateLocationURL = "http://localhost/scrumboard/card/UpdateCardLocation.php";
		WWWForm form = new WWWForm();
		form.AddField("CnumPost", Cnum);

		form.AddField("xLocationPost", xLocation.ToString());
		form.AddField("yLocationPost", yLocation.ToString());

		WWW www = new WWW(UpdateLocationURL, form);

		yield return www;

		string UpdateLocationboolean = www.text;
		Debug.Log("UpdateLocationboolean:" + UpdateLocationboolean);
	}

	public void AutoGenerate(int Cnum,string Ctitle,string Ctext,float xLocation,float yLocation,int Pnum,string CSsn,string ASsn,int Estimate,int Etime,float Alpha,float Red,float Green,float Blue)
	{
		//PlayerPrefs.SetString("Ssn","ss");
		//PlayerPrefs.SetString("Pnum","1");
		//LoginSsn = PlayerPrefs.GetString("Ssn");
		//LoginPnum= PlayerPrefs.GetString("Pnum");

		//Pnum = int.Parse(LoginPnum);

		//ss +=0.1f;
		Debug.Log ("autogenerate:1");
		GameObject cloneCard = PhotonNetwork.Instantiate ("card", new Vector3 (xLocation, yLocation, 2.7f), Quaternion.Euler (0f, 0f, 0f),0) as GameObject;
		cardComponent = cloneCard.GetComponent<Card> ();
		//cardComponent.setCard (Cnum, Ctitle, Ctext, xLocation, yLocation,Pnum,CSsn, ASsn, 0, Etime, Alpha, Red, Green, Blue);
		cardComponent.setCard (Cnum, Ctitle, Ctext, xLocation, yLocation, Pnum, CSsn, ASsn, Estimate, Etime, Alpha, Red, Green, Blue);
		 
		Debug.Log ("autogenerate:2");
		Color color = new Color (Red, Green, Blue, Alpha);
		cloneCard.GetComponentInChildren<MeshRenderer>().material.color = color;
		cloneCard.GetComponent<Rigidbody> ().useGravity = false;
		cloneCard.GetComponent<Rigidbody> ().isKinematic = true;
		cloneCard.GetComponentsInChildren<Text>()[0].text = Ctitle;
		cloneCard.GetComponentsInChildren<Text>()[1].text =Cnum.ToString();
		//cloneCard.GetComponentsInChildren<Text>()[2].text = Ctext;

		cloneCard.GetComponentsInChildren<Text>()[2].text = ASsn;
		cloneCard.GetComponentsInChildren<Text>()[3].text = Etime.ToString()+" h";

		cloneCard.name = "card"+Cnum;


		//StartCoroutine(InsertCard(Cnum, caedtext.text, Pnum, LoginSsn, 4,xLocation, yLocation, Alpha, Red, Green, Blue));
	}

	public IEnumerator ListPnumCard()
	{	
		//PlayerPrefs.SetString("Pnum","0");

		string LoginPnum= PlayerPrefs.GetString("Pnum");
		Debug.Log("LoginPnum:" + LoginPnum);
		string CardDataURL = "http://localhost/scrumboard/card/ListPnumCard.php";
		WWWForm form = new WWWForm();
		form.AddField("PnumPost", LoginPnum);
		WWW www = new WWW(CardDataURL, form);
		yield return www;

		string ItemsDataString = www.text;
		Debug.Log("ItemsDataString:" + ItemsDataString);
		//Debug.Log(ItemsDataString.Equals("\nNULL"));
		if (ItemsDataString.Equals("NULL"))
		{
			Debug.Log("NO have any card");
		}
		else
		{
			JsonCardData = JsonMapper.ToObject<JsonData>(ItemsDataString);
			JsonCardDatalength = JsonCardData.Count;
			Debug.Log("JsonCardDatalength:" + JsonCardData.Count);
		}


		int i = 0;
		while (i < (JsonCardDatalength))
		{
			string CardCnumJsonData = JsonCardData[i]["Cnum"].ToString();

			string CardCtitleJsonData = JsonCardData[i]["Ctitle"].ToString();
			string CardCtextJsonData = JsonCardData[i]["Ctext"].ToString();

			string CardxLocationJsonData = JsonCardData[i]["xLocation"].ToString();
			string CardyLocationJsonData = JsonCardData[i]["yLocation"].ToString();
			string CardPnumJsonData = JsonCardData[i]["Pnum"].ToString();

			string CardCSsnJsonData = JsonCardData[i]["CSsn"].ToString();
			string CardASsnJsonData = JsonCardData[i]["ASsn"].ToString();

			string CardEstimateJsonData = JsonCardData[i]["Estimate"].ToString();
			string CardEtimeJsonData = JsonCardData[i]["Etime"].ToString();


			string CardAlphaJsonData = JsonCardData[i]["Alpha"].ToString();
			string CardRedJsonData = JsonCardData[i]["Red"].ToString();
			string CardGreenJsonData = JsonCardData[i]["Green"].ToString();
			string CardBlueJsonData = JsonCardData[i]["Blue"].ToString();


			Cnum = int.Parse(CardCnumJsonData);
			Ctitle = CardCtitleJsonData;
			Ctext = CardCtextJsonData;
			xLocation = float.Parse(CardxLocationJsonData);
			yLocation =  float.Parse(CardyLocationJsonData);
			Pnum = int.Parse(CardPnumJsonData);
			CSsn = CardCSsnJsonData;
			ASsn = CardASsnJsonData;

			Estimate = int.Parse(CardEstimateJsonData);
			Etime=int.Parse(CardEtimeJsonData);
			Alpha =  float.Parse(CardAlphaJsonData);
			Red = float.Parse(CardRedJsonData);
			Green =  float.Parse(CardGreenJsonData);
			Blue = float.Parse(CardBlueJsonData);


			//CanAddEmployee[i] = new ProjectEmployeeData(Pnumber, PSsn, PCharacter);

			//Debug.Log("PnumCard Cnum:" + Cnum);
			//Debug.Log("PnumCard Ctitle:" + Ctitle);
			//Debug.Log("PnumCard Ctext:" + Ctext);

			//Debug.Log("PnumCard xLocation:" + xLocation);
			//Debug.Log("PnumCard yLocation:" + yLocation);
			//Debug.Log("PnumCard Pnum:" + Pnum);
			//Debug.Log("PnumCard CSsn:" + CSsn);

			//Debug.Log("PnumCard ASsn:" + ASsn);
			//Debug.Log("PnumCard Estimate:" + Estimate);
			//Debug.Log("PnumCard Etime:" + Etime);
			//Debug.Log("PnumCard Alpha:" + Alpha);
			//Debug.Log("PnumCard Red:" + Red);
			//Debug.Log("PnumCard Green:" + Green);
			//Debug.Log("PnumCard Blue:" + Blue);

			AutoGenerate (Cnum, Ctitle, Ctext, xLocation, yLocation, Pnum, CSsn, ASsn, Estimate, Etime, Alpha, Red, Green, Blue);

			i++;
		}

	}


    public IEnumerator ListCnumCard()
    {
        //PlayerPrefs.SetString("Pnum","0");

        string findCnumCard ="";
        
        string CnumCardURL = "http://localhost/scrumboard/card/CnumCardData.php";
        WWWForm form = new WWWForm();
        form.AddField("CnumPost", findCnumCard);
        WWW www = new WWW(CnumCardURL, form);
        yield return www;

        string ItemsDataString = www.text;
        Debug.Log("ItemsDataString:" + ItemsDataString);
        //Debug.Log(ItemsDataString.Equals("\nNULL"));
        if (ItemsDataString.Equals("NULL"))
        {
            Debug.Log("NO have any card");
        }
        else
        {
            JsonCnumCardData = JsonMapper.ToObject<JsonData>(ItemsDataString);
            JsonCnumCardDatalength = JsonCnumCardData.Count;
            Debug.Log("JsonCnumCardDatalength:" + JsonCnumCardData.Count);
        }


        int i = 0;
            string CardCnumJsonData = JsonCnumCardData[i]["Cnum"].ToString();

            string CardCtitleJsonData = JsonCnumCardData[i]["Ctitle"].ToString();
            string CardCtextJsonData = JsonCnumCardData[i]["Ctext"].ToString();

            string CardxLocationJsonData = JsonCnumCardData[i]["xLocation"].ToString();
            string CardyLocationJsonData = JsonCnumCardData[i]["yLocation"].ToString();
            string CardPnumJsonData = JsonCnumCardData[i]["Pnum"].ToString();

            string CardCSsnJsonData = JsonCnumCardData[i]["CSsn"].ToString();
            string CardASsnJsonData = JsonCnumCardData[i]["ASsn"].ToString();

            string CardEstimateJsonData = JsonCnumCardData[i]["Estimate"].ToString();
            string CardEtimeJsonData = JsonCnumCardData[i]["Etime"].ToString();


            string CardAlphaJsonData = JsonCnumCardData[i]["Alpha"].ToString();
            string CardRedJsonData = JsonCnumCardData[i]["Red"].ToString();
            string CardGreenJsonData = JsonCnumCardData[i]["Green"].ToString();
            string CardBlueJsonData = JsonCnumCardData[i]["Blue"].ToString();


            Cnum = int.Parse(CardCnumJsonData);
            Ctitle = CardCtitleJsonData;
            Ctext = CardCtextJsonData;
            xLocation = float.Parse(CardxLocationJsonData);
            yLocation = float.Parse(CardyLocationJsonData);
            Pnum = int.Parse(CardPnumJsonData);
            CSsn = CardCSsnJsonData;
            ASsn = CardASsnJsonData;

            Estimate = int.Parse(CardEstimateJsonData);
            Etime = int.Parse(CardEtimeJsonData);
            Alpha = float.Parse(CardAlphaJsonData);
            Red = float.Parse(CardRedJsonData);
            Green = float.Parse(CardGreenJsonData);
            Blue = float.Parse(CardBlueJsonData);


            //CanAddEmployee[i] = new ProjectEmployeeData(Pnumber, PSsn, PCharacter);

            Debug.Log("CnumCard Cnum:" + Cnum);
            Debug.Log("CnumCard Ctitle:" + Ctitle);
            Debug.Log("CnumCard Ctext:" + Ctext);

            Debug.Log("CnumCard xLocation:" + xLocation);
            Debug.Log("CnumCard yLocation:" + yLocation);
            Debug.Log("CnumCard Pnum:" + Pnum);
            Debug.Log("CnumCard CSsn:" + CSsn);

            Debug.Log("CnumCard ASsn:" + ASsn);
            Debug.Log("CnumCard Estimate:" + Estimate);
            Debug.Log("CnumCard Etime:" + Etime);
            Debug.Log("CnumCard Alpha:" + Alpha);
            Debug.Log("CnumCard Red:" + Red);
            Debug.Log("CnumCard Green:" + Green);
            Debug.Log("CnumCard Blue:" + Blue);


    }


}

