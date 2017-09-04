using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : Photon.MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (PhotonNetwork.connected) {
			PhotonNetwork.automaticallySyncScene = true;
			PhotonNetwork.LeaveRoom ();
			PhotonNetwork.LeaveLobby ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
