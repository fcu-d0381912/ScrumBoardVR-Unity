using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
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


	}
}
