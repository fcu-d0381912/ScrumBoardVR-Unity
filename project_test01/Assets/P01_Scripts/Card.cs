using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using UnityEngine.UI;
public class Card : MonoBehaviour {
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
	public JsonData JsonCnumCardData;
	public int JsonCnumCardDatalength = 0;
	public void setCard(int Cnum,string Ctitle,string Ctext,float xLocation,float yLocation,int Pnum,string CSsn,string ASsn,int Estimate,int Etime,float Alpha,float Red,float Green,float Blue){
		this.Cnum = Cnum;
		this.Ctitle = Ctitle;
		this.Ctext = Ctext;
		this.xLocation = xLocation;
		this.yLocation = yLocation;
		this.Pnum = Pnum;
		this.CSsn = CSsn;
		this.ASsn = ASsn;
		this.Estimate = Estimate;
		this.Etime = Etime;
		this.Alpha = Alpha;
		this.Red = Red;
		this.Green = Green;
		this.Blue = Blue;

	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if(stream.isWriting){
			stream.SendNext(Cnum);
			StartCoroutine (ListCnumCard ());
			Color color = new Color (Red, Green, Blue, Alpha);
			GetComponentInChildren<MeshRenderer>().material.color = color;
			GetComponent<Rigidbody> ().useGravity = false;
			GetComponent<Rigidbody> ().isKinematic = true;
			GetComponentsInChildren<Text>()[0].text = Ctitle;
			GetComponentsInChildren<Text>()[1].text =Cnum.ToString();
			//cloneCard.GetComponentsInChildren<Text>()[2].text = Ctext;

			GetComponentsInChildren<Text>()[2].text = ASsn;
			GetComponentsInChildren<Text>()[3].text = Etime.ToString()+" h";

			name = "card"+Cnum;
		}else{
			Cnum = (int)stream.ReceiveNext();
			StartCoroutine (ListCnumCard ());
			Color color = new Color (Red, Green, Blue, Alpha);
			GetComponentInChildren<MeshRenderer>().material.color = color;
			GetComponent<Rigidbody> ().useGravity = false;
			GetComponent<Rigidbody> ().isKinematic = true;
			GetComponentsInChildren<Text>()[0].text = Ctitle;
			GetComponentsInChildren<Text>()[1].text =Cnum.ToString();
			//cloneCard.GetComponentsInChildren<Text>()[2].text = Ctext;

			GetComponentsInChildren<Text>()[2].text = ASsn;
			GetComponentsInChildren<Text>()[3].text = Etime.ToString()+" h";

			name = "card"+Cnum;

		} 
	}
	/*public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			// We own this player: send the others our data
			stream.SendNext(Ctitle);
			stream.SendNext(Ctext);
		}else{
			// Network player, receive data
			this.Ctitle = (string)stream.ReceiveNext();
			this.Ctext = (string)stream.ReceiveNext();
		}


	}*/

	public IEnumerator ListCnumCard( )
	{
		//PlayerPrefs.SetString("Pnum","0");

		string findCnumCard ="";

		string CnumCardURL = "http://140.134.26.71:12345/CnumCardData.php";
		WWWForm form = new WWWForm();
		form.AddField("CnumPost", Cnum);
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
