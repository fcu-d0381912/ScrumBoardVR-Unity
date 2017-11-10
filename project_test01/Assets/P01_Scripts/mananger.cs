using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
public class mananger : PunBehaviour {
    public bool havetext;
    public bool first;
    string TEMP;
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
                PhotonNetwork.Instantiate("Sphere", new Vector3(0, 1, 0), Quaternion.identity, 0);
            }
            Debug.Log(TEMP);
        }
    }

    public override void OnConnectedToMaster()
    {
        OnJoinedLobby();
    }

    public override void OnJoinedLobby()
    {
        RoomOptions roomoption = new RoomOptions();
        //roomoption.CustomRoomProperties=;
        PhotonNetwork.JoinOrCreateRoom("testroom", roomoption,TypedLobby.Default);
        
    }
    

   

    
     void OnApplicationQuit()
    {
        PhotonNetwork.Disconnect();
    }
}
