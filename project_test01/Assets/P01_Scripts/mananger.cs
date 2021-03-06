﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using UnityEngine.UI;
public class mananger : PunBehaviour {
    public bool havetext;
    public bool first;
    string TEMP;
	public TEXT cardCreate;
	public Text projectName;
    // Use this for initialization
    void Start () {
        havetext = false;
        first = false;
        PhotonNetwork.ConnectUsingSettings("1.0");

	    }
	
	// Update is called once per frame
	void Update () {
		
        if (TEMP != PhotonNetwork.connectionStateDetailed.ToString())
        {
            TEMP = PhotonNetwork.connectionStateDetailed.ToString();
            if (PhotonNetwork.inRoom)
            {
               // PhotonNetwork.Instantiate("Sphere", new Vector3(0, 1, 0), Quaternion.identity, 0);
				projectName.text = PlayerPrefs.GetString ("projectName");
				if (PhotonNetwork.isMasterClient) {
					StartCoroutine (cardCreate.ListPnumCard ());
				}
				Debug.Log ("1231231"+PhotonNetwork.room.Name);
				Debug.Log (PhotonNetwork.room.PlayerCount);
            }
            Debug.Log(TEMP);


        }
		#if UNITY_ANDROID
		PhotonNetwork.JoinRandomRoom ();
		if(PhotonNetwork.room.PlayerCount == 1){
			PhotonNetwork.LeaveRoom();
			PhotonNetwork.JoinRandomRoom ();
		}
		#endif
    }

    public override void OnConnectedToMaster()
    {
        OnJoinedLobby();
    }

    public override void OnJoinedLobby()
    {
        RoomOptions roomoption = new RoomOptions();
        //roomoption.CustomRoomProperties=;
		#if UNITY_ANDROID
		PhotonNetwork.JoinRandomRoom ();
		#endif
		PhotonNetwork.JoinOrCreateRoom(projectName.text, roomoption,TypedLobby.Default);

        
    }
    
	public void Leave(){
		PhotonNetwork.LeaveRoom ();
	}
   

    
     void OnApplicationQuit()
    {
        PhotonNetwork.Disconnect();
    }
}
